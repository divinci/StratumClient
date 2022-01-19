using Newtonsoft.Json;

namespace Stratum.V1.Messages.Requests
{
    public class NotifyRequest : JsonRpcRequest
    {
        public const string METHOD = "mining.notify";
        public string JobId { get; private set; }
        public string PreviousHash { get; private set; }
        public string Coinbase1 { get; private set; }
        public string Coinbase2 { get; private set; }
        public string[] MerkleBranch { get; private set; }
        public string Version { get; private set; }
        public string NBits { get; private set; }
        public string NTime { get; private set; }
        public bool CleanJobs { get; private set; }

        private NotifyRequest()
        {
            this.Method = METHOD;
        }

        internal static NotifyRequest BuildFrom(string responseText)
        {
            var jsonRpcRequest = JsonConvert.DeserializeObject<JsonRpcRequest>(responseText);

            var instance = new NotifyRequest();
            instance._id = jsonRpcRequest.Id;

            instance.JobId = jsonRpcRequest.Params[0].ToString();
            instance.PreviousHash = jsonRpcRequest.Params[1].ToString();
            instance.Coinbase1 = jsonRpcRequest.Params[2].ToString();
            instance.Coinbase2 = jsonRpcRequest.Params[3].ToString();
            instance.MerkleBranch = jsonRpcRequest.Params[4].ToObject<string[]>();
            instance.Version = jsonRpcRequest.Params[5].ToString();
            instance.NBits = jsonRpcRequest.Params[6].ToString();
            instance.NTime = jsonRpcRequest.Params[7].ToString();
            instance.CleanJobs = jsonRpcRequest.Params[8].ToObject<bool>();

            return instance;
        }
    }
}