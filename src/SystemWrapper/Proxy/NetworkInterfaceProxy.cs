using System.Collections.Generic;
using System.Net.NetworkInformation;
using RedSpider.SystemWrapper.Interface;
using RedSpider.SystemWrapper.Interface.Proxy;
using RedSpider.SystemWrapper.Wrapper;

namespace RedSpider.SystemWrapper.Proxy
{
    public class NetworkInterfaceProxy : INetworkInterfaceProxy
    {
        /// <summary>
        /// Returns a collection of <see cref="INetworkInterfaceWrapper"/> representing the 
        /// network interfaces present on the computer.
        /// </summary>
        /// <returns>Collection of <see cref="INetworkInterfaceWrapper"/>.</returns>
        public IEnumerable<INetworkInterfaceWrapper> GetAllNetworkInterfaces()
        {
            var networkInterfaces = new List<INetworkInterfaceWrapper>();
            NetworkInterface[] interfacesArray = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface networkInterface in interfacesArray)
            {
                networkInterfaces.Add(new NetworkInterfaceWrapper(networkInterface));
            }

            return networkInterfaces;
        }
    }
}
