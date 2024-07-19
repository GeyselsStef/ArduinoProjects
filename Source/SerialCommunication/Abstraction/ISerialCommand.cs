namespace SerialCommunication.Abstraction
{
    public interface ISerialCommand
    {
        bool CanWrite { get; }
        void Write(ISerialConnection port);
    }
}