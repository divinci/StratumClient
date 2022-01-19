using Newtonsoft.Json;
using StratumClient.Extensions;
using StratumClient.V1.Messages;
using System;
using System.Collections.Concurrent;
using System.Text;
using System.Threading;

namespace StratumClient.V1
{
    public partial class StratumClient
    {
        private int _messageId = 0;

        private ConcurrentDictionary<int, JsonRpcRequest> _acks
            = new ConcurrentDictionary<int, JsonRpcRequest>();

        private void Send(JsonRpcRequest jsonRpcRequest)
        {
            var thisMessageId = Interlocked.Increment(ref _messageId);
            jsonRpcRequest._id = thisMessageId;

            if (!_acks.TryAdd(thisMessageId, jsonRpcRequest))
            {
                _logger.LogAndThrow(new SystemException("Unknown exception, outgoing JsonRpcRequest.Id already in use."));
            }

            var jsonString = JsonConvert.SerializeObject(jsonRpcRequest) + '\n';

            var jsonStringBytes = Encoding.UTF8.GetBytes(jsonString);

            _sendQueue.Enqueue(jsonStringBytes);

            if (!_socket.Connected)
            {
                _logger.LogAndThrow(new SystemException("Send failed.  Client not connected.  Please call ConnectAsync before attempting to send."));
            }

            ProcessSendQueue();
        }

        private ConcurrentQueue<byte[]> _sendQueue = new ConcurrentQueue<byte[]>();

        private readonly object _processSendLock = new object();

        private ManualResetEventSlim _socketSendLock = new ManualResetEventSlim(false);
        private void ProcessSendQueue()
        {
            if (Monitor.TryEnter(_processSendLock))
            {
                while (_sendQueue.TryDequeue(out byte[] data))
                {
                    try
                    {
                        _socket.Send(data);
                    }
                    catch (Exception e)
                    {
                        _logger.LogAndThrow(new Exception($"Error encountered when sending data", e));
                    }
                }

                Monitor.Exit(_processSendLock);
            }
        }
    }
}