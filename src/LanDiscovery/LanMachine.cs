using System.Net;
using RedSpider.LanDiscovery.Interface;

namespace RedSpider.LanDiscovery
{
    /// <summary>
    /// Lan machines class representing a detected machine.
    /// </summary>
    public class LanMachine : ILanMachine
    {
        #region Properties

        /// <inheritdoc />
        public IPAddress MachineIPAddress { get; private set; }

        /// <inheritdoc />
        public string MachineName { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Instantiate new lan machine object.
        /// </summary>
        /// <param name="address">Machine IP address.</param>
        /// <param name="name">Machine name.</param>
        public LanMachine(IPAddress address, string name)
        {
            MachineIPAddress = address;
            MachineName = name;
        } // end method

        #endregion
    } // end class
} // end namespace
