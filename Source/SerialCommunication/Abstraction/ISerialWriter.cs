namespace SerialCommunication.Abstraction
{
    public interface ISerialWriter: ISerialCommunicator
    {
        bool Write(ISerialCommand message);
    }
}