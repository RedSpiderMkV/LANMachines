using System.Collections.Generic;

namespace RedSpider.SystemWrapper.Interface.Proxy
{
    /// <summary>
    /// Interface to the NetworkInterface proxy - a proxy to the NetworkInterface static methods.
    /// </summary>
    public interface INetworkInterfaceProxy
    {
        /// <summary>
        /// Returns a collection of <see cref="INetworkInterfaceWrapper"/> representing the 
        /// network interfaces present on the computer.
        /// </summary>
        /// <returns>Collection of <see cref="INetworkInterfaceWrapper"/>.</returns>
        IEnumerable<INetworkInterfaceWrapper> GetAllNetworkInterfaces();
    }
}
