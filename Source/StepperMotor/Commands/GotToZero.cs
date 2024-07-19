using SerialCommunication.Abstraction;

namespace StepperMotor
{
    public class GotToZero : ISerialCommand
    {
        public bool CanWrite => true;

        public void Write(ISerialConnection port)
        {
            port.Write($"[0]");
        }
    }
}
