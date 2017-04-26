using System;
using System.Net;
using System.Net.NetworkInformation;
using RedSpider.SystemWrapper.Interface;

namespace RedSpider.SystemWrapper.Wrapper
{
    /// <summary>
    /// Wrapper around <see cref="NetworkInterface"/>. 
    /// </summary>
    public class NetworkInterfaceWrapper : INetworkInterfaceWrapper
    {
        #region Properties

        /// <inheritdoc />
        public NetworkInterfaceType NetworkInterfaceType { get; }

        /// <inheritdoc />
        public OperationalStatus OperationalStatus { get; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Instantiate a new wrapper around <see cref="NetworkInterface"/>. 
        /// </summary>
        /// <param name="networkInterface">NetworkInterface being wrapped.</param>
        public NetworkInterfaceWrapper(NetworkInterface networkInterface)
        {
            networkInterface_m = networkInterface;
        }

        /// <inheritdoc />
        public IPAddress GatewayIPAddressOfFirstInterface()
        {
            IPAddress gatewayAddress = null;
            GatewayIPAddressInformationCollection gateWayAddresses = networkInterface_m.GetIPProperties().GatewayAddresses;

            if (gateWayAddresses != null && gateWayAddresses.Count > 0)
            {
                string[] parts = gateWayAddresses[0].Address.ToString().Split('.');

                if(parts.Length == 3)
                {
                    string address = String.Format("{0}.{1}.{2}.", parts[0], parts[1], parts[2]);
                    IPAddress.TryParse(address, out gatewayAddress);
                }
            }

            return gatewayAddress;
        }

        #endregion

        #region Private Data

        private readonly NetworkInterface networkInterface_m;

        #endregion
    }
}
