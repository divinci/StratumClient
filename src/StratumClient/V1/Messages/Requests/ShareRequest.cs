using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace StratumClient.V1.Messages.Requests
{
    public class ShareRequest : JsonRpcRequest
    {
        public ShareRequest(string workerName, string jobId, string extraOnce2, string ntime, string nonce)
        {
            this.Method = "mining.submit";
            this.Params = new JArray();
            Params.Add(workerName);
            Params.Add(jobId);
            Params.Add(extraOnce2);
            Params.Add(ntime);
            Params.Add(nonce);

            WorkerName = workerName;
            JobId = jobId;
            ExtraOnce2 = extraOnce2;
            Ntime = ntime;
            Nonce = nonce;
        }

        [JsonIgnore]
        public string WorkerName { get; }

        [JsonIgnore]
        public string JobId { get; }

        [JsonIgnore]
        public string ExtraOnce2 { get; }

        [JsonIgnore]
        public string Ntime { get; }

        [JsonIgnore]
        public string Nonce { get; }
    }
}