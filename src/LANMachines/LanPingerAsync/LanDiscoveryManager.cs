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

        #endregion

        #region Private Data

        private LanPingerAsync lanPinger_m;
        private ArpScanner arpScanner_m;

        #endregion
    }
}
