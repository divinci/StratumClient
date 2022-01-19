using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Stratum.V1.Messages
{
    public class JsonRpcResponse : JsonRpcBase
    {
        [JsonProperty("result")]
        [JsonConverter(typeof(JsonRpcResponseResultConvertor))]
        public JArray Result { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }
    }
}