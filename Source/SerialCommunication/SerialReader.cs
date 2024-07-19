using System;
using System.IO.Ports;
using SerialCommunication.Abstraction;
using SerialCommunication.EventArguments;

namespace SerialCommunication
{
    public class SerialReader : BaseSerialCommunicator, ISerialReader
    {
        public event EventHandler<CommandReceivedEventArgs> CommandReceived;

        private readonly ICommandFactory _commandFactory;
        private readonly Action<ISerialCommand> _commandProcessor;
        private readonly char _startChar;
        private readonly char _endChar;

        public SerialReader(ISerialConnection serialConnection, ICommandFactory commandFactory, char startChar, char endChar)
            : this(serialConnection, commandFactory, null, startChar, endChar)
        {
        }

        public SerialReader(ISerialConnection serialConnection, ICommandFactory commandFactory) : this(serialConnection, commandFactory, null)
        {
        }

        public SerialReader(ISerialConnection serialConnection, ICommandFactory commandFactory, Action<ISerialCommand> commandProcessor)
            : this(serialConnection, commandFactory, commandProcessor, '[', ']')
        {
        }

        public SerialReader(ISerialConnection serialConnection, ICommandFactory commandFactory, Action<ISerialCommand> commandProcessor, char startChar, char endChar)
            : base(serialConnection)
        {
            _commandFactory = commandFactory;
            _commandProcessor = commandProcessor;
            _startChar = startChar;
            _endChar = endChar;
        }

        protected override void BackgroundAction()
        {
            string buffer = "";

            while (!_cts.Token.IsCancellationRequested)
            {
                try
                {
                    if (Connection.BytesToRead > 0)
                    {
                        char c = (char)Connection.ReadChar();

                        if (c == _startChar)
                        {
                            buffer = "";
                        }
                        else if (c == _endChar)
                        {
                            var command = _commandFactory.Create(buffer);
                            if (command != null)
                            {
                                _commandProcessor?.Invoke(command);
                                CommandReceived?.DynamicInvoke(this, new CommandReceivedEventArgs(command, buffer));
                            }
                        }
                        else
                        {
                            buffer += c;
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
            base.BackgroundAction();
        }
    }
}
