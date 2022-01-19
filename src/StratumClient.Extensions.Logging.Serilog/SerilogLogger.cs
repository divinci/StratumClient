using Serilog;
using Serilog.Events;
using Stratum.Logging;
using System;

namespace Stratum.Extensions.Logging.Serilog
{
    internal class StratumClientSerilogLogger : IStratumClientLogger
    {
        private ILogger _serilogLogger;

        private LogEventLevel _jsonMessageReceivedLogLevel;

        private LogEventLevel _jsonMessageSentLogLevel;

        public StratumClientSerilogLogger(
            ILogger serilogLogger,
            LogEventLevel jsonMessageReceivedLogLevel = LogEventLevel.Verbose,
            LogEventLevel jsonMessageSentLogLevel = LogEventLevel.Verbose)
        {
            if (serilogLogger == null)
            {
                throw new ArgumentNullException(nameof(serilogLogger));
            }

            _jsonMessageReceivedLogLevel = jsonMessageReceivedLogLevel;
            _jsonMessageSentLogLevel = jsonMessageSentLogLevel;

            _serilogLogger = serilogLogger;
        }

        public void Debug(string message)
        {
            _serilogLogger.Debug(message);
        }

        public void Error(Exception e, string message)
        {
            _serilogLogger.Error(e, message);
        }

        public void Error(string message)
        {
            _serilogLogger.Error(message);
        }

        public void Information(string message)
        {
            _serilogLogger.Information(message);
        }

        public void JsonMessageReceived(byte[] json)
        {
            _serilogLogger.Write(_jsonMessageReceivedLogLevel, $"Message received: {System.Text.Encoding.UTF8.GetString(json)}");
        }

        public void JsonMessageSent(byte[] json)
        {
            _serilogLogger.Write(_jsonMessageSentLogLevel, $"Message sent: {System.Text.Encoding.UTF8.GetString(json)}");
        }

        public void Verbose(string message)
        {
            _serilogLogger.Verbose(message);
        }
    }
}