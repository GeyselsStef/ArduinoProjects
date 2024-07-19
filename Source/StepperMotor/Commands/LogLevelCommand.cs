using SerialCommunication.Abstraction;

namespace StepperMotor
{
    public class LogLevelCommand : ISerialCommand
    {
        private readonly int _level;
        public int Level => _level;

        public bool CanWrite => true;

        public LogLevelCommand(int level)
        {
            _level = level;
        }

        public void Write(ISerialConnection port)
        {
            port.Write($"[loglevel|{_level}]");
        }

        public override string ToString()
        {
            return $"Log level: {_level}";
        }

        public static ISerialCommand Parse(string[] parts)
        {
            return new LogLevelCommand(int.Parse(parts[1]));
        }

    }
}
