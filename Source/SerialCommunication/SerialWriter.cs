using Microsoft.Extensions.DependencyInjection;
using SerialCommunication.Abstraction;
using SerialCommunication.SerialPort;
using System;
using System.Collections.Concurrent;

namespace SerialCommunication
{

    public class SerialWriter : BaseSerialCommunicator, ISerialWriter
    {
        private BlockingCollection<ISerialCommand> _commands;

        public SerialWriter(ISerialConnection serialPort) : base(serialPort)
        {
        }

        public override void Start()
        {
            _commands = new BlockingCollection<ISerialCommand>();
            base.Start();
        }

        public bool Write(ISerialCommand message)
        {
            return _commands.TryAdd(message);
        }

        protected override void BackgroundAction()
        {
            try
            {
                foreach (var command in _commands.GetConsumingEnumerable(_cts.Token))
                {
                    try
                    {
                        if (command.CanWrite)
                        {
                            command.Write(_connection);
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception)
            {
            }

            base.BackgroundAction();
        }
    }

    public static class SerialPortExtensions
    {
        public static IServiceCollection AddSerialPortFactory(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ISerialPortFactory,SerialPortFactory>();
            return serviceCollection;
        }
    }
}
