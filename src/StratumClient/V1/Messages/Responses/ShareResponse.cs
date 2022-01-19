using Newtonsoft.Json;

namespace Stratum.V1.Messages.Responses
{
    public class ShareResponse : JsonRpcResponse
    {
        private ShareResponse()
        { }

        public bool Valid { get; private set; }

        public static ShareResponse BuildFrom(string responseText)
        {
            var jsonRpcResponse = JsonConvert.DeserializeObject<JsonRpcResponse>(responseText);

            var instance = new ShareResponse();
            instance._id = jsonRpcResponse.Id;

            instance.Error = jsonRpcResponse.Error;
            instance.Valid = bool.Parse(jsonRpcResponse.Result[0].ToString());

            return instance;
        }
    }
}