using Serilog;
using Serilog.Events;
using Stratum.Extensions.Logging.Serilog;

namespace Stratum.V1
{
    public static class StratumClientExtensions
    {
        public static StratumClient UseSerilog(
            this StratumClient client,
            ILogger logger,
            LogEventLevel jsonMessageReceivedLogLevel = LogEventLevel.Verbose,
            LogEventLevel jsonMessageSentLogLevel = LogEventLevel.Verbose)
        {
            var stratumClientSerilogLogger = new StratumClientSerilogLogger(
                logger,
                jsonMessageReceivedLogLevel,
                jsonMessageSentLogLevel);

            client.SetLogger(stratumClientSerilogLogger);

            return client;
        }
    }
}