using System.Net;
using System.Net.NetworkInformation;

namespace RedSpider.SystemWrapper.Interface
{
    /// <summary>
    /// Interface to the wrapper around NetworkInterface.
    /// </summary>
    public interface INetworkInterfaceWrapper
    {
        /// <summary>
        /// Get the network interface type.
        /// </summary>
        NetworkInterfaceType NetworkInterfaceType { get; }

        /// <summary>
        /// Get the network operational state.
        /// </summary>
        OperationalStatus OperationalStatus { get; }

        /// <summary>
        /// Gets the gateway IP address of the first network interface.
        /// </summary>
        /// <returns>First gateway IP address.</returns>
        IPAddress GatewayIPAddressOfFirstInterface();
    }
}
