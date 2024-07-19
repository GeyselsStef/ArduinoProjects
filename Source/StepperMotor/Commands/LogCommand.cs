using SerialCommunication.Abstraction;

namespace StepperMotor
{
    public class LogCommand : ISerialCommand
    {
        private readonly string _value;

        public bool CanWrite => false;

        public LogCommand(string value)
        {
            _value = value;
        }

        public void Write(ISerialConnection port)
        {
            // Omit writing to the port
        }

        public override string ToString()
        {
            return $"Log: {_value}";
        }

        public static ISerialCommand Parse(string[] parts)
        {
            return new LogCommand(parts[1]);
        }
    }
}
