using System;
using SerialCommunication.EventArguments;

namespace SerialCommunication.Abstraction
{
    public interface ISerialReader : ISerialCommunicator
    {
        event EventHandler<CommandReceivedEventArgs> CommandReceived;
    }
}