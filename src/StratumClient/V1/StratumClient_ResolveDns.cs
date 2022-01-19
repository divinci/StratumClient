using Stratum.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Stratum.V1
{
    public partial class StratumClient
    {
        private IEnumerable<IPEndPoint> _iPEndPoints;

        private void _SetHostAddress(IPEndPoint iPEndPoint)
        {
            _iPEndPoints = new List<IPEndPoint>()
            {
                iPEndPoint
            };

            _logger.Verbose($"IPEndPoints set {string.Join(", ", _iPEndPoints.AsEnumerable())}");
        }

        private async Task CheckOrResolveIPEndPoints(string hostNameOrAddress, int port)
        {
            _logger.EnteringMethod();

            if (_iPEndPoints == null)
            {
                _logger.Verbose($"No IPEndPoint set in constructor. DNS resolution required.");

                _logger.Verbose($"Resolving hostname {hostNameOrAddress}");

                var ipAddresses = await Dns.GetHostAddressesAsync(hostNameOrAddress);

                _logger.Verbose($"IPAddresses returned {string.Join(", ", ipAddresses.AsEnumerable())}");

                _iPEndPoints = ipAddresses.Select(ipAddress => new IPEndPoint(ipAddress, port));

                _logger.Verbose($"IPEndPoints set {string.Join(", ", _iPEndPoints.AsEnumerable())}");
            }
        }
    }
}