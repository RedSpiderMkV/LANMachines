using System;
using System.Collections.Generic;
using System.Net;

namespace LanDiscovery
{
    private class IPAddressComparer : IComparer<IPAddress>
    {
        public int Compare(IPAddress x, IPAddress y)
        {
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
