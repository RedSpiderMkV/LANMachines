using System.Collections.Generic;
using System.Net;

namespace RedSpider.LanDiscovery.Interface
{
    /// <summary>
    /// Interface to the ARP scanner - used to scan the network using the ARP command.
    /// </summary>
    public interface IArpScanner
    {
        /// <summary>
        /// Get all machines which respond to the arp request.
        /// </summary>
        /// <returns>List of responding machines.</returns>
        IEnumerable<IPAddress> GetRespondingMachines();
    }
}
