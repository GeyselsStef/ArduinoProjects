using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Net.Http;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SerialCommunication;
using SerialCommunication.Abstraction;
using StepperMotor.SerialTalkers;

namespace StepperMotor
{
    public partial class Form1 : Form
    {

        private string _selectedPort;
        private ISerialConnection _serialPort;
        private ISerialReader _serialReader;
        private ISerialWriter _serialWriter;
        private bool _sendCommandOnTrackBarChanged = true;
        private readonly ISerialPortFactory _serialPortFactory;

        public Form1(ISerialPortFactory serialPortFactory)
        {
            _serialPortFactory = serialPortFactory;

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.DisplayMember = "Description";
            comboBox1.Items.AddRange(GetComPorts().ToArray());

            string[] loglevels = new string[] { "0", "1", "2", "3", "4" };
            comboBox2.Items.AddRange(loglevels);
        }

        private void ProcessCommand(ISerialCommand str)
        {
            // Visitor pattern
            Visit(str as dynamic);
        }

        private void Visit(ISerialCommand command) { WriteText($"Unknown Command: {command}{Environment.NewLine}"); }

        private void Visit(LocationCommand command) => UpdateTrackBar(command.Step);

        private void Visit(ManualCommand command) { }

        private void Visit(UnKnownCommand command) => WriteText($"{command}{Environment.NewLine}");

        private void Visit(ResetCommand command) => UpdateTrackBar(0);

        private void Visit(LogCommand command) => WriteText($"{command}{Environment.NewLine}");

        private void Visit(LogLevelCommand command) => UpdateLogLevel(command);

        private void UpdateTrackBar(int step)
        {
            if (trackBar1.InvokeRequired)
            {
                trackBar1.Invoke(new Action(() => UpdateTrackBar(step)));
            }
            else
            {
                decimal rotation = Math.Truncate((step / 4096m) * 1000) / 1000;
                _sendCommandOnTrackBarChanged = false;
                trackBar1.Value = Math.Max((int)Math.Round((rotation * 12) + 60),0);
                _sendCommandOnTrackBarChanged = true;
                trackBar1.Enabled = true;
            }
        }

        private void UpdateLogLevel(LogLevelCommand logLevelCommand)
        {
            if (comboBox2.InvokeRequired)
            {
                comboBox2.Invoke(new Action(() => UpdateLogLevel(logLevelCommand)));
            }
            else
            {
                comboBox2.SelectedItem = logLevelCommand.Level.ToString();
            }
        }

        private void WriteText(string str)
        {
            if (textBox1.InvokeRequired)
            {
                textBox1.Invoke(new Action(() => WriteText(str)));
            }
            else
            {
                textBox1.Text += str;
                textBox1.SelectionStart = textBox1.Text.Length;
                textBox1.ScrollToCaret();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _serialReader?.Stop();
            _serialWriter?.Stop();
            e.Cancel = _serialPort?.IsOpen ?? false;
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _selectedPort = (comboBox1.SelectedItem as ComPort)?.Name;
        }

        private void ButtonConnect_Click(object sender, EventArgs e)
        {
            SetEnabeling();

            try
            {
                _serialPort = _serialPortFactory.Create(_selectedPort, 115200);
                _serialPort.Open();

                while (!_serialPort.IsOpen)
                {
                    WriteText(Text = $"Waiting for serial port \"{_selectedPort}\" to open..." + Environment.NewLine);
                    Thread.Sleep(500);
                }

                _serialReader = _serialPortFactory.CreateReader(_serialPort, new CommandFactory(), ProcessCommand);
                _serialReader.Start();

                _serialWriter = _serialPortFactory.CreateWriter(_serialPort);
                _serialWriter.Start();

                WriteCommand(new GetStatus());

            }
            catch (Exception)
            {
                SetEnabeling(true);
            }
        }

        private void SetEnabeling(bool val = false)
        {
            comboBox1.Enabled = val;
            ButtonConnect.Enabled = val;
            ButtonReloadComPorts.Enabled = val;
            ButtonDisconnect.Enabled = !val;
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            decimal r = (trackBar1.Value - 60) / 12m;
            if (_sendCommandOnTrackBarChanged) WriteCommand(GetCommand(r));
        }

        private ISerialCommand GetCommand(decimal value) => value switch
        {
            var v when v < 0 => new RotateCounterClockwise(value),
            var v when v > 0 => new RotateClockwise(value),
            0 => new GotToZero(),
            _ => new UnKnownCommand()
        };

        private void ButtonReload_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(GetComPorts().ToArray());
        }

        private void ButtonToOrigin_Click(object sender, EventArgs e)
        {
            trackBar1.Enabled = false;
            WriteCommand(new GotToZero());
        }

        private void ButtonRefreshArduinoData_Click(object sender, EventArgs e)
        {
            WriteCommand(new GetStatus());
        }

        private void ButtonSendLogLevel_Click(object sender, EventArgs e)
        {
            WriteCommand(() => new LogLevelCommand(int.Parse(comboBox2.Text)));
        }

        private void ButtonDisconnect_Click(object sender, EventArgs e)
        {
            _serialReader?.Stop();
            _serialWriter?.Stop();
            _serialPort?.Close();
            _serialPort?.Dispose();
            _serialReader.Dispose();
            _serialWriter.Dispose();

            SetEnabeling(true);
        }

        private void ButtonResetOrigin_Click(object sender, EventArgs e)
        {
            WriteCommand(new ResetCommand());
        }

        private void WriteCommand(ISerialCommand command)
        {
            try
            {
                _serialWriter?.Write(command);
            }
            catch (Exception ex)
            {
                WriteText($"Error: {ex.Message}{Environment.NewLine}");
            }
        }

        private void WriteCommand(Func<ISerialCommand> command)
        {
            try
            {
                _serialWriter?.Write(command.Invoke());
            }
            catch (Exception ex)
            {
                WriteText($"Error: {ex.Message}{Environment.NewLine}");
            }
        }

        private void ButtonManualLeftFast_Click(object sender, EventArgs e)
        {
            WriteCommand(new ManualCommand(-10 * (ModifierKeys.HasFlag(Keys.Shift) ? 2 : 1) * (ModifierKeys.HasFlag(Keys.Control) ? 5 : 1)));
        }

        private void ButtonManualLeft_Click(object sender, EventArgs e)
        {
            WriteCommand(new ManualCommand(-1 * (ModifierKeys.HasFlag(Keys.Shift) ? 2 : 1) * (ModifierKeys.HasFlag(Keys.Control) ? 5 : 1)));
        }

        private void ButtonManualRight_Click(object sender, EventArgs e)
        {
            WriteCommand(new ManualCommand(1 * (ModifierKeys.HasFlag(Keys.Shift) ? 2 : 1) * (ModifierKeys.HasFlag(Keys.Control) ? 5 : 1)));
        }

        private void ButtonManualRightFast_Click(object sender, EventArgs e)
        {
            WriteCommand(new ManualCommand(10 * (ModifierKeys.HasFlag(Keys.Shift) ? 2 : 1) * (ModifierKeys.HasFlag(Keys.Control) ? 5 : 1)));
        }

        private void ButtonStop_Click(object sender, EventArgs e)
        {
            WriteCommand(new StopCommand());
        }

        private void trackBar1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void trackBar1_MouseUp(object sender, MouseEventArgs e)
        {

        }

        protected virtual IEnumerable<ComPort> GetComPorts()
        {
            using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE Caption like '%(Com%'"))
            {
                var portNames = searcher.Get().OfType<ManagementBaseObject>().Select(p => p["Caption"].ToString()).ToList();
                string[] ports = SerialPort.GetPortNames();
                return ports.Select(n => new ComPort(n, portNames.FirstOrDefault(s => s.Contains(n))));
            }
        }

        protected class ComPort
        {
            public string Name { get; set; }
            public string Description { get; set; }

            public ComPort(string name, string description)
            {
                Name = name;
                Description = description;
            }

            public override string ToString()
            {
                return Description;
            }
        }

        private TcpClient _tcpClient;
        private Thread _tcpThread;

        private void ButtonConnectWifi_Click(object sender, EventArgs e)
        {
            if (_tcpThread != null && _tcpThread.IsAlive)
            {
                _tcpThread.Abort();
            }

            _tcpThread = new Thread(ConnectToWifi);
            _tcpThread.IsBackground = true;
            _tcpThread.Start();

        }

        int _counter = 0;
        private void ConnectToWifi()
        {
            while (true)
            {
                try
                {
                    WriteText("Connecting to server..." + Environment.NewLine);
                    _tcpClient = new TcpClient(textBoxIp.Text, 80);
                    //_tcpClient.ReceiveTimeout = (int)TimeSpan.FromSeconds(5).TotalMilliseconds;
                    _tcpClient.SendTimeout = (int)TimeSpan.FromSeconds(5).TotalMilliseconds;
                    byte[] bytes = new byte[1024];
                    string message = "";
                    using (NetworkStream stream = _tcpClient.GetStream())
                    {
                        WriteText("Connected to server" + Environment.NewLine);
                        //byte[] content = System.Text.Encoding.ASCII.GetBytes("|");
                        //stream.Write(content, 0, content.Length);
                        //stream.ReadTimeout= (int)TimeSpan.FromSeconds(7).TotalMilliseconds;
                        while (_tcpClient.Connected)
                        {
                            if (!stream.CanRead || !stream.CanWrite)
                            {
                                //WriteText("Can't read or write" + Environment.NewLine);
                                break;
                            }

                            if (!string.IsNullOrEmpty(_tcpCommand))
                            {
                                var parts=_tcpCommand.Split('|');

                                byte[] cmd=new byte[3];
                                for (int i = 0; i < cmd.Length; i++)
                                {
                                    cmd[i] = byte.Parse(parts[0][i].ToString());
                                }

                                string[] strings = parts[1].Split(';');
                                byte[] cmdParts = new byte[strings.Length];
                                for (int i = 0; i < strings.Length; i++)
                                {
                                    cmdParts[i] = byte.Parse(strings[i]);
                                }

                                byte cmdLength = (byte)(cmd.Length +cmdParts.Length);

                                stream.WriteByte(254);
                                stream.WriteByte((byte)cmdLength);
                                stream.Write(cmd,0,cmd.Length);
                                stream.Write(cmdParts, 0, cmdParts.Length);
                                stream.WriteByte(255);
                                stream.Flush();

                                //content = System.Text.Encoding.ASCII.GetBytes(_tcpCommand);
                                //stream.Write(content, 0, content.Length);
                                _tcpCommand = "";
                            }

                            int bit=GetNextByte(stream).Result;
                                                         
                            if (bit >= 0)
                            {
                                char g = (char)bit;
                                message += g;
                                if (g == '\n')
                                {

                                    WriteText(message);
                                    if (long.TryParse(message, out long l))
                                    {
                                       byte[] content = System.Text.Encoding.ASCII.GetBytes($"[ackn|{l}]");
                                        stream.Write(content, 0, content.Length);
                                    }
                                    message = "";
                                }
                                else if (g == '.')
                                {
                                    WriteText(g.ToString());
                                }
                            }
                            else
                            {
                                // content = System.Text.Encoding.ASCII.GetBytes("[]");
                                // stream.Write(content, 0, content.Length);
                            }


                            //int bytesRead = 0;
                            //while (bytesRead < bytes.Length)
                            //{
                            //    bytesRead += stream.Read(bytes, bytesRead, bytes.Length - bytesRead);
                            //}
                            //string response = System.Text.Encoding.ASCII.GetString(bytes, 0, bytesRead);
                            //WriteText(response);
                        }
                    }
                    WriteText("Disconnected from server");
                }
                catch (Exception ex)
                {
                    WriteText($"Error: {ex.Message}{Environment.NewLine}");
                    _tcpCommand = "";
                }
            }
        }

        private string _tcpCommand = "";

        private void ButtonSendViaTCP_Click(object sender, EventArgs e)
        {
            _tcpCommand = textBoxTxpCommand.Text;
        }

        private async Task<int> GetNextByte(NetworkStream stream)
        {
            byte[] bytes = new byte[1];
            CancellationTokenSource cancellationToken = new CancellationTokenSource();

            int result = -1;
            Task readByteTask = new Task(() => result = stream.ReadByte(), cancellationToken.Token);
            Task delayTask = Task.Delay(3000, cancellationToken.Token);

            Task completedTask = await Task.WhenAny(readByteTask, delayTask);
            cancellationToken.Cancel();
            if (completedTask == delayTask)
            {
                return -1;
            }else
                return result;

        }
    }
}
