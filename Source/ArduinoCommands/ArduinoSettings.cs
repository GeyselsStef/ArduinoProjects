using SerialCommunication.Enums;

namespace ArduinoCommands
{
    public class ArduinoSettings
    {
        public string PortName { get; set; }
        public int BaudRate { get; set; }
        public Parity Parity { get; set; } = Parity.None;
        public int DataBits { get; set; } = 8;
        public StopBits StopBits { get; set; } = StopBits.One;
        public char StartChar { get; set; } = '[';
        public char EndChar { get; set; } = ']';
    }
}
