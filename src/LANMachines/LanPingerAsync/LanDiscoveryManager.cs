using System;
using System.Collections.Generic;
using System.Text;

namespace LanDiscovery
{
    public class LanDiscoveryManager
    {
        #region Public Methods

        public LanDiscoveryManager()
        {
            
        }

        public void DiscoverNetworkMachines()
        {
            List<string> lanMachinesList = new List<string>();

            using (lanPinger_m = new LanPingerAsync())
            {
                foreach (string lanIp in lanPinger_m.GetActiveMachines())
                {
                    lanMachinesList.Add(lanIp);
                }
            }

            arpScanner_m = new ArpScanner();
            foreach (string lanIp in arpScanner_m.GetRespondingMachines())
            {
                if (!lanMachinesList.Contains(lanIp))
                {
                    lanMachinesList.Add(lanIp);
                }
            }
        }

        #endregion

        #region Private Data

        private LanPingerAsync lanPinger_m;
        private ArpScanner arpScanner_m;

        #endregion
    }
}
