using System;

namespace StratumClient
{
    public interface ILogger
    {
        void Verbose(string message);

        void Debug(string message);

        void Information(string message);

        void JsonMessageSent(byte[] json);

        void JsonMessageReceived(byte[] json);

        void Error(Exception e, string message);

        void Error(string message);
    }
}