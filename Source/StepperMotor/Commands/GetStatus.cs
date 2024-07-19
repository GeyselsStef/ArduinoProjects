using SerialCommunication.Abstraction;

namespace StepperMotor
{
    public class GetStatus : ISerialCommand
    {
        public bool CanWrite => true;

        public void Write(ISerialConnection port)
        {
            port.Write($"[status]");
        }
    }
}
