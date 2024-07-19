using SerialCommunication.Abstraction;

namespace StepperMotor.SerialTalkers
{
    public class LocationCommand : ISerialCommand
    {
        private readonly int _step;
        public int Step => _step;

        public bool CanWrite => true;

        public LocationCommand(int step)
        {
            _step = step;
        }

        public void Write(ISerialConnection port)
        {
        }

        public override string ToString()
        {
            return $"Current location: {_step}";
        }

        public static ISerialCommand Parse(string command)
        {
            command = command.Split('|')[1];
            return new LocationCommand(int.Parse(command));
        }
    }
}
