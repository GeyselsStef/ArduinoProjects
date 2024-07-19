using SerialCommunication.Abstraction;
using System;

namespace StepperMotor
{
    public class RotateCounterClockwise : ISerialCommand
    {
        private readonly decimal _rotations;

        public bool CanWrite => true;

        public RotateCounterClockwise(decimal rotations)
        {
            _rotations = Math.Abs(rotations);
        }

        public void Write(ISerialConnection port)
        {
            port.Write($"[ccw|{_rotations:0.000}]".Replace(",", "."));
        }
    }
}
