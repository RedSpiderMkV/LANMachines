using System;
using System.Collections.Generic;
using System.Text;

namespace LanDiscovery
{
    public class LanDiscoveryManager
    {
        #region Public Methods

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

        private List<string> getLanPingResults()
        {
            using (lanPinger_m = new LanPingerAsync())
            {
                return lanPinger_m.GetActiveMachines();
            } // end using
        } // end method

        private List<string> getArpScanResults()
        {
            using (arpScanner_m = new ArpScanner())
            {
                return arpScanner_m.GetRespondingMachines();
            } // end using
        } // end method

        #endregion

        #region Private Data

        private LanPingerAsync lanPinger_m;
        private ArpScanner arpScanner_m;

        #endregion
    } // end class
} // end namespace
