using System.Net;

namespace RedSpider.LanDiscovery.Interface
{
    public interface ILanMachine
    {
        /// <summary>
        /// IP address of machine.
        /// </summary>
        IPAddress MachineIPAddress { get; }

        /// <summary>
        /// Name of machine from IP address.
        /// </summary>
        string MachineName { get; }
    }
}
