using SerialCommunication.Abstraction;

namespace StepperMotor
{
    public class UnKnownCommand : ISerialCommand
    {
        private readonly string _value;

        public bool CanWrite => false;

        public UnKnownCommand() { }

        public UnKnownCommand(string value)
        {
            _value = value;
        }

        public void Write(ISerialConnection port)
        {
            // Omit writing to the port
        }

        public override string ToString()
        {
            return $"Command: {_value}";
        }
    }
}
