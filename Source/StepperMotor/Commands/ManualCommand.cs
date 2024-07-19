using SerialCommunication.Abstraction;

namespace StepperMotor
{
    public class ManualCommand : ISerialCommand
    {
        private readonly int _steps;
        public int Steps => _steps;

        public bool CanWrite => true;

        public ManualCommand(int steps)
        {
            _steps = steps;
        }

        public void Write(ISerialConnection port)
        {
            port.Write($"[steps|{_steps}]");
        }

        public static ManualCommand Parse(string command)
        {
            command = command.Split('|')[1];
            return new ManualCommand(int.Parse(command));
        }
    }
    public class StopCommand : ISerialCommand
    {
        public bool CanWrite => true;

        public void Write(ISerialConnection port)
        {
            port.Write($"[stop]");
        }
    }
}
