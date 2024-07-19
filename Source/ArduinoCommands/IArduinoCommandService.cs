using SerialCommunication.Abstraction;
using SerialCommunication.EventArguments;
using System;

namespace ArduinoCommands
{
    public interface IArduinoCommandService : IDisposable
    {
        event EventHandler<CommandReceivedEventArgs> CommandReceived;

        void Start(string portName, int baudRate, ICommandFactory commandFactory, Action<ISerialCommand> commandExecuter);
        void Start(string portName, Action<ISerialCommand> commandExecuter);
        void Start();
    }
}
