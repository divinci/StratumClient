using Newtonsoft.Json;

namespace Stratum.V1.Messages
{
    public class JsonRpcBase
    {
        [JsonProperty("id")]
        internal int? _id { get; set; }

        [JsonIgnore]
        public int? Id
        { get { return _id; } }
    }
}