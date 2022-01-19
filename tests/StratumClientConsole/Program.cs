using Serilog;
using Stratum.V1;
using Stratum.V1.Messages.Requests;

namespace StratumClientConsole
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var client = new StratumClient()
                .UseSerilog(
                    new LoggerConfiguration()
                        .MinimumLevel.Verbose()
                        .WriteTo.Console()
                        .CreateLogger());

            await client.ConnectAsync(new Uri("tcp://eu.stratum.slushpool.com:3333"));

            client.OnSubscribeResponse += (subscribeResponse, subscribeRequest) =>
            {
                Console.WriteLine($"Subscribed | Extranonce1 : {subscribeResponse.Extranonce1}");

                client.Authorize(
                    new AuthorizeRequest(
                        "94be816cc10c7705dc706dfc19c2f3b8.StratumClient",
                        "myPassword"));
            };

            client.OnAuthorizeResponse += (authorizeResponse, authorizeRequest) =>
            {
                Console.WriteLine($"Authorized | Success : {authorizeResponse.Success}");
            };

            client.OnSetDifficultyRequest += (setDifficultyRequest) =>
            {
                Console.WriteLine($"SetDifficulty | Difficulty : {setDifficultyRequest.Difficulty}");
            };

            client.OnNotifyRequest += (notifyRequest) =>
            {
                Console.WriteLine($"Notify | JobId : {notifyRequest.JobId}");
            };

            client.Subscribe(new SubscribeRequest());

            Console.ReadLine();
        }
    }
}