using Serilog.Events;

namespace StratumClient.Extensions.Logging
{
    public static class StratumClientExtensions
    {
        public static V1.StratumClient UseSerilog(
            this V1.StratumClient client,
            Serilog.ILogger logger,
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