using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

namespace LanMachines
{
    public class LanPingerAsync
    {
        #region Public Methods

        public LanPingerAsync(int pingerCount)
        {
            initialiseLanPingerList(pingerCount);
            initialiseIpBase();
        } // end method

        #endregion

        #region Private Methods

        private void initialiseIpBase()
        {
            NetworkInterface networkInterface = NetworkInterface.GetAllNetworkInterfaces().FirstOrDefault();
            if (networkInterface == null)
            {
                ipAddressBase_m = null;
            }
            else
            {
                string gateWayAddress = networkInterface.GetIPProperties().GatewayAddresses.FirstOrDefault().Address.ToString();
                string[] parts = gateWayAddress.Split('.');

                if (parts.Length < 3)
                {
                    ipAddressBase_m = null;
                }
                else
                {
                    ipAddressBase_m = String.Format("{0}.{1}.{2}", parts[0], parts[1], parts[2]);
                } // end if
            } // end if
        } // end method

        private void initialiseLanPingerList(int pingerCount)
        {
            lanPingers_m = new List<Ping>();
            for (int i = 0; i < pingerCount; i++)
            {
                lanPingers_m.Add(new Ping());
            } // end method
        } // end method

        #endregion

        #region Private Data

        private string ipAddressBase_m;
        private List<Ping> lanPingers_m;

        #endregion

    } // end class
} // end namespace
