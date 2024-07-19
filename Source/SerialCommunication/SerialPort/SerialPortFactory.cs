using System;
using SerialCommunication.Abstraction;
using SerialCommunication.Enums;

namespace SerialCommunication.SerialPort
{
    public class SerialPortFactory : ISerialPortFactory
    {
        public ISerialConnection Create(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits)
        {
            return new SafeSerialPort(portName, baudRate, parity, dataBits, stopBits);
        }

        public ISerialConnection Create(string portName, int baudRate)
        {
            return new SafeSerialPort(portName, baudRate);
        }

        public ISerialReader CreateReader(ISerialConnection connection, ICommandFactory commandFactory, Action<ISerialCommand> processCommand)
        {
            return new SerialReader(connection, commandFactory, processCommand);
        }

        public ISerialReader CreateReader(ISerialConnection serialPort, ICommandFactory commandFactory, Action<ISerialCommand> processCommand, char startChar, char stopChar)
        {
            return new SerialReader(serialPort, commandFactory, processCommand, startChar, stopChar);
        }

        public ISerialWriter CreateWriter(ISerialConnection serialPort)
        {
            return new SerialWriter(serialPort);
        }

        public ISerialWriter CreateWriter(ISerialConnection serialPort, char startChar, char stopChar)
        {
            return new SerialWriter(serialPort);
        }
    }
}
