using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace StratumClient.V1.Messages
{
    public class JsonRpcRequest : JsonRpcBase
    {
        [JsonProperty("method")]
        internal string Method { get; set; }

        [JsonProperty("params")]
        internal JArray Params { get; set; } = new JArray();
    }
}