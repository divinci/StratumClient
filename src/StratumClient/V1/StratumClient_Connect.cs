using Stratum.Extensions;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Stratum.V1
{
    public partial class StratumClient
    {
        private SemaphoreSlim _socketConnectingSemaphore = new SemaphoreSlim(1);

        /// <summary>
        /// Connects to a Stratum version 1 server.
        /// </summary>
        /// <returns>
        /// The IPEndPoint information of the server it connected to
        /// </returns>
        public Task<IPEndPoint> ConnectAsync(IPEndPoint ipEndPoint)
        {
            _SetHostAddress(ipEndPoint);
            return ConnectAsync(string.Empty, 0);
        }

        /// <summary>
        /// Connects to a Stratum version 1 server.
        /// </summary>
        /// <returns>
        /// The IPEndPoint information of the server it connected to
        /// </returns>
        public Task<IPEndPoint> ConnectAsync(Uri uri)
        {
            return ConnectAsync(uri.DnsSafeHost, uri.Port);
        }

        /// <summary>
        /// Connects to a Stratum version 1 server.
        /// </summary>
        /// <returns>
        /// The IPEndPoint information of the server it connected to
        /// </returns>
        public async Task<IPEndPoint> ConnectAsync(string hostNameOrAddress, int port)
        {
            _logger.EnteringMethod();

            _socketConnectingSemaphore.Wait();

            if (_socket != null)
            {
                _logger.LogAndThrow(new SystemException($"Already connected. Do not call {nameof(ConnectAsync)} more than once."));
            }

            await CheckOrResolveIPEndPoints(hostNameOrAddress, port);

            var ipEndPoint = _iPEndPoints.Random();

            _logger.Verbose($"Connecting to IPEndPoint {ipEndPoint}");

            _socket = new Socket(ipEndPoint.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            _socket.Connect(ipEndPoint);

            _socketConnectingSemaphore.Release();

            _BeginRecieve();

            return ipEndPoint;
        }
    }
}