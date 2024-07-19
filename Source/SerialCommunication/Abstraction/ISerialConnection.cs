using System;
using System.IO;

namespace SerialCommunication.Abstraction
{
    public interface ISerialConnection : IDisposable
    {
        bool IsOpen { get; }
        Stream BaseStream { get; }

        void Close();
        void Open();
        bool Write(string message);
        bool Write(byte[] bytes);
    }
}