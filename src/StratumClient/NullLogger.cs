using System;

namespace StratumClient
{
    internal class NullLogger : ILogger
    {
        public void Debug(string message)
        {
        }

        public void Error(Exception e, string message)
        {
        }

        public void Error(string message)
        {
        }

        public void Information(string message)
        {
        }

        public void JsonMessageReceived(byte[] json)
        {
        }

        public void JsonMessageSent(byte[] json)
        {
        }

        public void Verbose(string message)
        {
        }
    }
}