using System;
using System.Threading;
using System.IO.Ports;
using SerialCommunication.Abstraction;
using SerialCommunication.EventArguments;

namespace SerialCommunication
{

    public abstract class BaseSerialCommunicator :  ISerialCommunicator,IDisposable
    {
        public event EventHandler<BackgroundActionTerminatedEventArgs> BackgroundActionTerminated;

        protected Thread _bgThread;
        protected CancellationTokenSource _cts;
        protected bool _disposedValue;

        protected object _lock = new object();
        protected readonly ISerialConnection _connection;
        protected System.IO.Ports. SerialPort Connection => (System.IO.Ports. SerialPort)_connection;

        public BaseSerialCommunicator(ISerialConnection connection)
        {
            _connection = connection;
        }

        public virtual void Start()
        {
            _cts = new CancellationTokenSource();
            _bgThread = new Thread(BackgroundAction);
            _bgThread.IsBackground = true;
            _bgThread.Name = "Background Write Thread";
            _bgThread.Priority = ThreadPriority.Normal;
            _bgThread.Start();
        }

        public virtual void Stop()
        {
            try
            {
                _cts?.Cancel();
            }
            catch { }
        }

        protected virtual void BackgroundAction()
        {
            OnBackgroundActionTerminated(new BackgroundActionTerminatedEventArgs());
        }

        protected void OnBackgroundActionTerminated(BackgroundActionTerminatedEventArgs e)
        {
            BackgroundActionTerminated?.Invoke(this, e);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _cts?.Cancel();
                    _bgThread?.Abort();
                    _cts.Dispose();
                }
                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
