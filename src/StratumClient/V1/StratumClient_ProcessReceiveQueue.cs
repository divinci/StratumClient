using Newtonsoft.Json;
using Stratum.Extensions;
using Stratum.V1.Messages;
using Stratum.V1.Messages.Requests;
using Stratum.V1.Messages.Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Stratum.V1
{
    public partial class StratumClient
    {
        private readonly object _processReceiveLock = new object();

        private MemoryStream _incomingDataStream = new MemoryStream();

        private void ProcessReceiveQueue()
        {
            if (Monitor.TryEnter(_processReceiveLock))
            {
                try
                {
                    // Write all bytes to the _incomingData MemoryStream.

                    _incomingDataStream.Seek(0, SeekOrigin.End);

                    while (_recieveQueue.TryDequeue(out byte[] data))
                    {
                        _incomingDataStream.Write(data, 0, data.Length);
                    }

                    // We are going to check each character of the stream
                    // and record where we have \n characters.

                    var newLineBreakIndexes = new List<int>();

                    _incomingDataStream.Seek(0, SeekOrigin.Begin);

                    for (int i = 0; i < _incomingDataStream.Length; i++)
                    {
                        var currentCharacter = (char)_incomingDataStream.ReadByte();
                        if (currentCharacter == '\n')
                        {
                            newLineBreakIndexes.Add(i);
                        }
                    }

                    // Create byte[] for each peice of data
                    // between the \n characters.

                    _incomingDataStream.Seek(0, SeekOrigin.Begin);

                    if (newLineBreakIndexes.Count > 0)
                    {
                        int previousBreak = 0;
                        for (int i = 0; i < newLineBreakIndexes.Count; i++)
                        {
                            int currentBreak = newLineBreakIndexes[i];
                            try
                            {
                                var jsonMessageBytes = new byte[currentBreak - previousBreak];
                                _incomingDataStream.Read(jsonMessageBytes, 0, jsonMessageBytes.Length);

                                var jsonMessageString = Encoding.UTF8.GetString(jsonMessageBytes);

                                if(jsonMessageString.Trim().Length > 0)
                                {
                                    _logger.JsonMessageReceived(jsonMessageBytes);

                                    if (jsonMessageString.Contains("\"params\""))
                                    {
                                        ProcessJsonRpcRequest(jsonMessageString);
                                    }
                                    else
                                    {
                                        try
                                        {
                                            ProcessJsonRpcResponse(jsonMessageString);
                                        }
                                        catch (Exception ex)
                                        {
                                            _logger.Error(ex, "Error deserializing JsonRpcResponse");
                                        }
                                    }
                                }

                                previousBreak = currentBreak;
                            }
                            catch (Exception e)
                            {
                                _logger.Error(e, $"Error encountered while invoking event.");
                            }
                        }
                    }

                    var newMemoryStream = new MemoryStream();
                    _incomingDataStream.CopyTo(newMemoryStream);
                    _incomingDataStream = newMemoryStream;
                }
                finally
                {
                    Monitor.Exit(_processReceiveLock);
                }
            }
        }

        private void ProcessJsonRpcResponse(string jsonMessageString)
        {
            var jsonRpcResponse = JsonConvert.DeserializeObject<JsonRpcResponse>(jsonMessageString);

            if (jsonRpcResponse == null)
            {
                _logger.Error($"Unrecognised JsonRpcResponse message received: {jsonMessageString}");
                return;
            }

            if (!jsonRpcResponse.Id.HasValue)
            {
                _logger.Error($"JsonRpcResponse message does not have an Id: {jsonMessageString}");
                return;
            }

            if (_acks.TryGetValue(jsonRpcResponse.Id.Value, out JsonRpcRequest value))
            {
                switch (value.Method)
                {
                    case AuthorizeRequest.METHOD:
                        OnAuthorizeResponse?.Invoke(AuthorizeResponse.BuildFrom(jsonMessageString), value as AuthorizeRequest);
                        break;

                    case SubscribeRequest.METHOD:
                        OnSubscribeResponse?.Invoke(SubscribeResponse.BuildFrom(jsonMessageString), value as SubscribeRequest);
                        break;

                    case ShareRequest.METHOD:
                        OnShareResponse?.Invoke(ShareResponse.BuildFrom(jsonMessageString), value as ShareRequest);
                        break;

                    default:
                        _logger.LogAndThrow(new NotImplementedException($"JsonRpcResponse not implemented: {jsonMessageString}"));
                        break;
                }
            }
            else
            {
                _logger.Error($"Unable to match JsonRpcResponse message Id with corresponding JsonRpcRequest message Id: {jsonMessageString}");
                return;
            }
        }

        private void ProcessJsonRpcRequest(string jsonMessageString)
        {
            var jsonRpcRequest = JsonConvert.DeserializeObject<JsonRpcRequest>(jsonMessageString);

            if (jsonRpcRequest == null)
            {
                _logger.Error($"Unrecognised JsonRpcRequest message received: {jsonMessageString}");
            }

            if (jsonMessageString.Contains($"\"{NotifyRequest.METHOD}\""))
            {
                var notifyRequest = NotifyRequest.BuildFrom(jsonMessageString);
                _logger.Information($"NotifyRequest received JobId:{notifyRequest.JobId} CleanJobs:{notifyRequest.CleanJobs}");
                OnNotifyRequest?.Invoke(notifyRequest);
            }
            else if (jsonMessageString.Contains($"\"{SetDifficultyRequest.METHOD}\""))
            {
                var setDifficultyRequest = SetDifficultyRequest.BuildFrom(jsonMessageString);
                _logger.Information($"SetDifficultyRequest received Difficulty:{setDifficultyRequest.Difficulty}");
                OnSetDifficultyRequest?.Invoke(setDifficultyRequest);
            }
            else
            {
                _logger.Error($"Unrecognised JsonRpcRequest message received: {jsonMessageString}");
            }
        }
    }
}