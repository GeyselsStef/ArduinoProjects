using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SerialCommunication.Abstraction;
using SerialCommunication.EventArguments;
using System;

namespace ArduinoCommands
{
    public class ArduinoCommandService : IArduinoCommandService
    {
        public event EventHandler<CommandReceivedEventArgs> CommandReceived;

        private readonly ISerialPortFactory _serialPortFactory;
        private readonly IOptions<ArduinoSettings> _options;
        private readonly IServiceProvider _serviceProvider;
        private ISerialWriter _writer;
        private ISerialReader _reader;
        private bool _disposedValue;
        private ISerialConnection _serialPort;

        public ArduinoCommandService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _serialPortFactory = _serviceProvider.GetService<ISerialPortFactory>();
            _options = _serviceProvider.GetService<IOptions<ArduinoSettings>>();

            if (_serialPortFactory == null)
                throw new ArgumentNullException("ISerialPortFactory");
        }


        public void Start()
        {
            if (_options?.Value == null)
                throw new ArgumentNullException("ArduinoSettings");

            Start(_options.Value.PortName, null);
        }

        public void Start(string portName, Action<ISerialCommand> commandExecuter)
        {
            if (_options?.Value == null)
                throw new ArgumentNullException("ArduinoSettings");

            ICommandFactory commandFactory = _serviceProvider.GetService<ICommandFactory>();
            if (commandFactory == null)
                throw new ArgumentNullException("ICommandFactory");

            Start(portName, _options.Value.BaudRate, commandFactory, commandExecuter);
        }

        public void Start(string portName, int baudRate, ICommandFactory commandFactory, Action<ISerialCommand> commandExecuter)
        {
            _serialPort = _serialPortFactory.Create(portName, baudRate);
            _serialPort.Open();
            _writer = _serialPortFactory.CreateWriter(_serialPort);
            _writer.Start();
            _reader = _serialPortFactory.CreateReader(_serialPort, commandFactory, commandExecuter);
            _reader.CommandReceived += CommandReceivedHandler;
            _reader.Start();
        }

        protected virtual void CommandReceivedHandler(object sender, CommandReceivedEventArgs e)
        {
            CommandReceived?.Invoke(this, e);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    if (_writer != null)
                    {
                        _writer.Stop();
                    }
                    _writer?.Dispose();

                    if (_reader != null)
                    {
                        _reader.CommandReceived -= CommandReceivedHandler;
                        _reader.Stop();
                    }
                    _reader?.Dispose();

                    if (_serialPort != null)
                    {
                        _serialPort.Close();
                        _serialPort.Dispose();
                    }
                    _serialPort?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                _disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~ArduinoCommandService()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
