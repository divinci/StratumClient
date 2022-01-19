using Newtonsoft.Json;

namespace Stratum.V1.Messages.Requests
{
    public class SetDifficultyRequest : JsonRpcRequest
    {
        public const string METHOD = "mining.set_difficulty";
        public int Difficulty { get; private set; }

        private SetDifficultyRequest()
        {
            this.Method = METHOD;
        }

        internal static SetDifficultyRequest BuildFrom(string responseText)
        {
            var jsonRpcRequest = JsonConvert.DeserializeObject<JsonRpcRequest>(responseText);

            var instance = new SetDifficultyRequest();
            instance._id = jsonRpcRequest.Id;

            instance.Difficulty = int.Parse(jsonRpcRequest.Params[0].ToString());

            return instance;
        }
    }
}