using StratumClient.V1.Messages.Requests;
using StratumClient.V1.Messages.Responses;
using System.Net.Sockets;

namespace StratumClient.V1
{
    public delegate void DOnConnect(StratumV1JsonRpcClient jsonRpcClient);

    public delegate void DOnDisconnect(StratumV1JsonRpcClient jsonRpcClient);

    public delegate void DOnAuthorizeResponse(AuthorizeResponse authorizeResponse, AuthorizeRequest authorizeRequest);

    public delegate void DOnSubscribeResponse(SubscribeResponse subscribeResponse, SubscribeRequest subscribeRequest);

    public delegate void DOnShareResponse(ShareResponse shareResponse, ShareRequest shareRequest);

    public delegate void DOnNotifyRequest(NotifyRequest notifyRequest);

    public delegate void DOnSetDifficultyRequest(SetDifficultyRequest setDifficultyRequest);

    public partial class StratumV1JsonRpcClient
    {
        private ILogger _logger;

        private Socket _socket;

        public event DOnAuthorizeResponse OnAuthorizeResponse;

        public event DOnSubscribeResponse OnSubscribeResponse;

        public event DOnShareResponse OnShareResponse;

        public event DOnNotifyRequest OnNotifyRequest;

        public event DOnSetDifficultyRequest OnSetDifficultyRequest;

        public StratumV1JsonRpcClient()
        {
        }

        public void SetLogger(ILogger logger)
        {
            _logger = logger;
        }

        public StratumV1JsonRpcClient(ILogger logger)
        {
            _logger = logger;
        }

        public void Subscribe(SubscribeRequest subscribeRequest) => Send(subscribeRequest);

        public void Authorize(AuthorizeRequest authorizeRequest) => Send(authorizeRequest);

        public void Share(ShareRequest shareRequest) => Send(shareRequest);
    }
}