using SerialCommunication.Abstraction;

namespace StepperMotor
{
    public class RotateClockwise : ISerialCommand
    {
        private readonly decimal _rotations;

        public bool CanWrite => true;

        public RotateClockwise(decimal rotations)
        {
            _rotations = rotations;
        }

        public void Write(ISerialConnection port)
        {
            port.Write($"[cw|{_rotations:0.000}]".Replace(",", "."));
        }
    }
}
