using Stratum.Logging;
using Stratum.V1.Messages.Requests;
using Stratum.V1.Messages.Responses;
using System.Net.Sockets;

namespace Stratum.V1
{
    public delegate void DOnConnect(StratumClient jsonRpcClient);

    public delegate void DOnDisconnect(StratumClient jsonRpcClient);

    public delegate void DOnAuthorizeResponse(AuthorizeResponse authorizeResponse, AuthorizeRequest authorizeRequest);

    public delegate void DOnSubscribeResponse(SubscribeResponse subscribeResponse, SubscribeRequest subscribeRequest);

    public delegate void DOnShareResponse(ShareResponse shareResponse, ShareRequest shareRequest);

    public delegate void DOnNotifyRequest(NotifyRequest notifyRequest);

    public delegate void DOnSetDifficultyRequest(SetDifficultyRequest setDifficultyRequest);

    public partial class StratumClient
    {
        private IStratumClientLogger _logger;

        private Socket _socket;

        public event DOnAuthorizeResponse OnAuthorizeResponse;

        public event DOnSubscribeResponse OnSubscribeResponse;

        public event DOnShareResponse OnShareResponse;

        public event DOnNotifyRequest OnNotifyRequest;

        public event DOnSetDifficultyRequest OnSetDifficultyRequest;

        public StratumClient()
        {
            _logger = new NullIStratumClientLogger();
        }

        internal void SetLogger(IStratumClientLogger logger)
        {
            _logger = logger;
        }

        public void Subscribe(SubscribeRequest subscribeRequest) => Send(subscribeRequest);

        public void Authorize(AuthorizeRequest authorizeRequest) => Send(authorizeRequest);

        public void Share(ShareRequest shareRequest) => Send(shareRequest);
    }
}