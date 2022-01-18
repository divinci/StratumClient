using Newtonsoft.Json;

namespace StratumClient.V1.Messages.Responses
{
    public class SubscribeResponse : JsonRpcResponse
    {
        private SubscribeResponse()
        { }

        public string Extranonce1 { get; private set; }

        public int Extranonce2Size { get; private set; }

        public static SubscribeResponse BuildFrom(string responseText)
        {
            var jsonRpcResponse = JsonConvert.DeserializeObject<JsonRpcResponse>(responseText);

            var instance = new SubscribeResponse();
            instance._id = jsonRpcResponse.Id;

            instance.Extranonce1 = jsonRpcResponse.Result[1].ToString();
            instance.Extranonce2Size = int.Parse(jsonRpcResponse.Result[2].ToString());

            return instance;
        }
    }
}