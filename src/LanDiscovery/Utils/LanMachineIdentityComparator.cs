using System;
using System.Collections.Generic;
using System.Net;

namespace RedSpider.LanDiscovery
{
    internal class LanMachineIdentityComparator : IComparer<LanMachine>
    {
        public int Compare(LanMachine m1, LanMachine m2)
        {
            IPAddress x = m1.MachineIPAddress;
            IPAddress y = m2.MachineIPAddress;

            byte[] xBytes = x.GetAddressBytes();
            byte[] yBytes = y.GetAddressBytes();

            if (xBytes.Length != 4 || yBytes.Length != 4)
            {
                return 0;
            } // end if

            for (int i = 0; i < 4; i++)
            {
                if (xBytes[i] > yBytes[i])
                {
                    return 1;
                }
                else if (xBytes[i] < yBytes[i])
                {
                    return -1;
                } // end if
            } // end for

            return 0;
        } // end method
    } // end class
} // end namespace
