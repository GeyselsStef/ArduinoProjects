using SerialCommunication.Abstraction;

namespace StepperMotor
{
    public class ResetCommand : ISerialCommand
    {
        public bool CanWrite => true;

        public void Write(ISerialConnection port)
        {
            port.Write("[reset]");
        }
    }
}
