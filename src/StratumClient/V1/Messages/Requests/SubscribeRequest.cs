using Newtonsoft.Json.Linq;

namespace StratumClient.V1.Messages.Requests
{
    public class SubscribeRequest : JsonRpcRequest
    {
        public const string METHOD = "mining.subscribe";

        public SubscribeRequest()
        {
            this.Method = METHOD;
            this.Params = new JArray();
        }
    }
}