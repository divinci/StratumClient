using Newtonsoft.Json;

namespace Stratum.V1.Messages.Responses
{
    public class AuthorizeResponse : JsonRpcResponse
    {
        private AuthorizeResponse()
        { }

        public bool Success { get; set; }

        public static AuthorizeResponse BuildFrom(string responseText)
        {
            var jsonRpcResponse = JsonConvert.DeserializeObject<JsonRpcResponse>(responseText);

            var instance = new AuthorizeResponse();
            instance._id = jsonRpcResponse.Id;

            instance.Error = jsonRpcResponse.Error;
            instance.Success = jsonRpcResponse.Result[0].ToObject<bool>();

            return instance;
        }
    }
}