using System;

namespace Stratum.Logging
{
    internal class NullIStratumClientLogger : IStratumClientLogger
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