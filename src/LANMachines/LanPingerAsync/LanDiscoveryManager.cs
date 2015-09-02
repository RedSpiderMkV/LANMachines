using System;
using System.Collections.Generic;
using System.Text;

namespace LanDiscovery
{
    /// <summary>
    /// Lan discovery manager - identifies lan machines
    /// using multiple techniques.
    /// </summary>
    public class LanDiscoveryManager
    {
        #region Public Methods

        /// <summary>
        /// Retrieve a list of network machines.
        /// </summary>
        /// <returns>List of network IP addresses.</returns>
        public List<string> GetNetworkMachines()
        {
            List<string> lanMachinesList = getLanPingResults();
            List<string> activeMachines = getArpScanResults();

            foreach (string lanIp in lanMachinesList)
            {
                if (!activeMachines.Contains(lanIp))
                {
                    activeMachines.Add(lanIp);
                } // end if
            } // end foreach

            return activeMachines;
        } // end method

        #endregion

        #region Private Methods

        /// <summary>
        /// Get the list of machines which responded to the lan ping test.
        /// </summary>
        /// <returns>List of lan ping responding.</returns>
        private List<string> getLanPingResults()
        {
            using (lanPinger_m = new LanPingerAsync())
            {
                return lanPinger_m.GetActiveMachines();
            } // end using
        } // end method

        /// <summary>
        /// Get the list of machines which responded to the arp request.
        /// </summary>
        /// <returns>List of arp responders.</returns>
        private List<string> getArpScanResults()
        {
            using (arpScanner_m = new ArpScanner())
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

        #endregion

    } // end class
} // end namespace
