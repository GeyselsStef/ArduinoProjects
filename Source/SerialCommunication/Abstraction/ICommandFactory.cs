namespace SerialCommunication.Abstraction
{
    public interface ICommandFactory
    {
        ISerialCommand Create(string command);
    }
}
