using Serilog.Events;

namespace StratumClient.Extensions.Logging
{
    public static class StratumClientExtensions
    {
        public static V1.StratumV1JsonRpcClient UseSerilog(
            this V1.StratumV1JsonRpcClient client,
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