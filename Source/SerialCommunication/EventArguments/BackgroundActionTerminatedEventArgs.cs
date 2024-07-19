using System;

namespace SerialCommunication.EventArguments
{
    public class BackgroundActionTerminatedEventArgs : EventArgs
    {
        public Exception Exception { get; set; }
    }
}
