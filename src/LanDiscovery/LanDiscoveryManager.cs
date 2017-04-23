using System;
using System.Collections.Generic;
using System.Net;
using SystemWrapper.Factory;
using SystemWrapper.Interface.Factory;

namespace LanDiscovery
{
    /// <summary>
    /// Lan discovery manager - identifies lan machines
    /// using multiple techniques.
    /// </summary>
    public class LanDiscoveryManager
    {
        #region Public Methods

        public LanDiscoveryManager()
        {
            comparator_m = new LanMachineIdentityComparator();
            processWrapperFactory_m = new ProcessWrapperFactory();
        } // end method

        /// <summary>
        /// Retrieve a list of network machines.
        /// </summary>
        /// <returns>List of network IP addresses.</returns>
        public List<LanMachine> GetNetworkMachines()
        {
            List<IPAddress> uniqueMachineAddresses = getUniqueIpAddresses();
            List<LanMachine> lanMachines = getLanMachinesFromIpAddresses(uniqueMachineAddresses);

            lanMachines.Sort(comparator_m);

            return lanMachines;
        } // end method

        #endregion

        #region Private Methods

        /// <summary>
        /// Retrieve a list of unique IP addresses detected on the LAN.
        /// </summary>
        /// <returns>List of unique IP addresses.</returns>
        private List<IPAddress> getUniqueIpAddresses()
        {
            List<IPAddress> pingResponders = getLanPingResults();
            List<IPAddress> arpResponders = getArpScanResults();

            List<IPAddress> uniqueMachineAddresses = pingResponders;
            foreach (IPAddress lanIp in arpResponders)
            {
                if (!uniqueMachineAddresses.Contains(lanIp))
                {
                    uniqueMachineAddresses.Add(lanIp);
                } // end if
            } // end foreach

            return uniqueMachineAddresses;
        } // end method

        /// <summary>
        /// Retrieve a list of LanMachine objects (IP address and machine name)
        /// from a list of IP addresses.
        /// </summary>
        /// <param name="ipAddresses">Machine IP addresses.</param>
        /// <returns>Lan machines.</returns>
        private List<LanMachine> getLanMachinesFromIpAddresses(List<IPAddress> ipAddresses)
        {
            List<LanMachine> lanMachines = new List<LanMachine>();

            string machineName;
            IPHostEntry entry;

            foreach (IPAddress address in ipAddresses)
            {
                try
                {
                    entry = Dns.GetHostEntry(address);
                    machineName = entry.HostName;
                }
                catch (Exception)
                {
                    // Console.WriteLine("Unable to find host name: " + address.ToString());
                    machineName = String.Empty;
                } // end try-catch

                lanMachines.Add(new LanMachine(address, machineName));
            } // end foreach

            return lanMachines;
        } // end method

        /// <summary>
        /// Get the list of machines which responded to the lan ping test.
        /// </summary>
        /// <returns>List of lan ping responding.</returns>
        private List<IPAddress> getLanPingResults()
        {
            using (lanPinger_m = new LanPingerAsync())
            {
                List<IPAddress> respondingIpAddresses = lanPinger_m.GetActiveMachineAddresses();
                if (respondingIpAddresses == null)
                {
                    return new List<IPAddress>();
                } // end if

                return respondingIpAddresses;
            } // end using
        } // end method

        /// <summary>
        /// Get the list of machines which responded to the arp request.
        /// </summary>
        /// <returns>List of arp responders.</returns>
        private List<IPAddress> getArpScanResults()
        {
            using (arpScanner_m = new ArpScanner(processWrapperFactory_m))
            {
                return arpScanner_m.GetRespondingMachines();
            } // end using
        } // end method

        #endregion

        #region Private Data

        // Lan ping handler.
        private LanPingerAsync lanPinger_m;
        // Arp request handler.
        private ArpScanner arpScanner_m;

        private readonly IComparer<LanMachine> comparator_m;
        private readonly IProcessWrapperFactory processWrapperFactory_m;

        #endregion

    } // end class
} // end namespace
