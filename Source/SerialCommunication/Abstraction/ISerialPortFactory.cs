using System;
using SerialCommunication.Enums;

namespace SerialCommunication.Abstraction
{
    public interface ISerialPortFactory
    {
        ISerialConnection Create(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits);
        ISerialConnection Create(string portName, int baudRate);
        
        ISerialReader CreateReader(ISerialConnection serialPort, ICommandFactory commandFactory, Action<ISerialCommand> processCommand, char startChar, char stopChar);
        ISerialReader CreateReader(ISerialConnection serialPort, ICommandFactory commandFactory, Action<ISerialCommand> processCommand);
       
        ISerialWriter CreateWriter(ISerialConnection serialPort, char startChar, char stopChar);
        ISerialWriter CreateWriter(ISerialConnection serialPort);
    }
}
