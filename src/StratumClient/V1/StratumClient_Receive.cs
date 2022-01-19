using Stratum.Extensions;
using System;
using System.Collections.Concurrent;
using System.Net.Sockets;

namespace Stratum.V1
{
    public partial class StratumClient
    {
        private byte[] _recieveBuffer = new byte[1024 * 4];
        private void _BeginRecieve()
        {
            _logger.EnteringMethod();

            _socket.BeginReceive(_recieveBuffer, 0, _recieveBuffer.Length, SocketFlags.None, new AsyncCallback(_EndRecieve), null);
        }

        private ConcurrentQueue<byte[]> _recieveQueue = new ConcurrentQueue<byte[]>();
        private void _EndRecieve(IAsyncResult iAsyncResult)
        {
            _logger.EnteringMethod();

            try
            {
                var bytesReceived = _socket.EndReceive(iAsyncResult);
                if (bytesReceived > 0)
                {
                    var data = new byte[bytesReceived];
                    Array.Copy(_recieveBuffer, data, bytesReceived);
                    _recieveQueue.Enqueue(data);

                    _BeginRecieve();

                    ProcessReceiveQueue();
                }
            }
            catch (Exception ex)
            {
                _logger.LogAndThrow(new SystemException($"Error receiving", ex));
            }
        }
    }
}