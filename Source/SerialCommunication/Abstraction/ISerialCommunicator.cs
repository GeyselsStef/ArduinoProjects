using SerialCommunication.EventArguments;
using System;

namespace SerialCommunication.Abstraction
{
    public interface ISerialCommunicator:IDisposable
    {
        event EventHandler<BackgroundActionTerminatedEventArgs> BackgroundActionTerminated;

        void Start();
        void Stop();
    }
}