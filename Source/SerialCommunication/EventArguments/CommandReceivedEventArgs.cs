using System;
using SerialCommunication.Abstraction;

namespace SerialCommunication.EventArguments
{
    public class CommandReceivedEventArgs : EventArgs
    {
        public ISerialCommand Command { get; }
        public string OriginalText { get; }

        public CommandReceivedEventArgs(ISerialCommand command, string originalText)
        {
            Command = command;
            OriginalText = originalText;
        }
    }
}
