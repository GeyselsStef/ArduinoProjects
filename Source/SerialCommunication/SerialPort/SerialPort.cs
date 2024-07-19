using System;
using System.IO;
using SerialCommunication.Enums;
using SerialCommunication.Abstraction;

namespace SerialCommunication.SerialPort
{

    public class SafeSerialPort : System.IO.Ports.SerialPort, ISerialConnection, IDisposable
    {
        private Stream _baseStream;

        public SafeSerialPort(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits)
            : base(portName, baudRate, (System.IO.Ports.Parity)parity, dataBits, (System.IO.Ports.StopBits)stopBits)
        {
        }

        public SafeSerialPort(string portName, int baudRate) : base(portName, baudRate)
        {
        }

        bool ISerialConnection.Write(string message)
        {
            if (IsOpen)
            {
                try
                {
                    Write(message);
                    return true;
                }
                catch (Exception ex)
                {
                }
            }
            return false;
        }

        public new void Open()
        {
            try
            {
                base.Open();
                _baseStream = BaseStream;
                GC.SuppressFinalize(BaseStream);
            }
            catch
            {

            }
        }

        public new void Dispose()
        {
            Dispose(true);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && base.Container != null)
            {
                base.Container.Dispose();
            }
            try
            {
                if (_baseStream.CanRead)
                {
                    _baseStream.Close();
                    GC.ReRegisterForFinalize(_baseStream);
                }
            }
            catch
            {
                // ignore exception - bug with USB - serial adapters.
            }
            base.Dispose(disposing);
        }

    }
}
