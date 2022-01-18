using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace StratumClient.V1.Messages.Requests
{
    public class AuthorizeRequest : JsonRpcRequest
    {
        public const string METHOD = "mining.authorize";

        public AuthorizeRequest(string workerName, string password)
        {
            this.Method = METHOD;
            this.Params = new JArray()
            {
                workerName,
                password
            };
            this.WorkerName = workerName;
            this.Password = password;
        }

        [JsonIgnore]
        public string WorkerName { get; }

        [JsonIgnore]
        public string Password { get; }
    }
}