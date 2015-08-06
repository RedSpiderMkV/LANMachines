using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

namespace LanPinger
{
    public class LanPingerAsync
    {
        #region Public Methods

        public LanPingerAsync(int pingerCount)
        {
            initialiseLanPingers(pingerCount);
            ipAddressBaseSet_m = initialiseIpBase();
        } // end method

        public void PingAllAsync()
        {
            int pingerCount = 1;
            foreach (Ping ping in lanPingers_m)
            {
                ping.SendAsync(ipAddressBase_m + pingerCount.ToString(), 500, null);
                pingerCount++;
            } // end foreach
        } // end method

        #endregion

        #region Private Methods

        private bool initialiseIpBase()
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
                    
                    return true;
                } // end if
            } // end if

            return false;
        } // end method

        private void initialiseLanPingers(int pingerCount)
        {
            lanPingers_m = new List<Ping>();
            for (int i = 0; i < pingerCount; i++)
            {
                Ping ping = new Ping();
                ping.PingCompleted += new PingCompletedEventHandler(ping_PingCompleted);
                
                lanPingers_m.Add(ping);
            } // end method
        } // end method

        private void ping_PingCompleted(object sender, PingCompletedEventArgs e)
        {
            if (e.Reply.Status == IPStatus.Success)
            {
                Console.WriteLine(e.Reply.Address.ToString());
            }

            Ping sendingPinger = (Ping)sender;
            sendingPinger.PingCompleted -= ping_PingCompleted;

            lanPingers_m.Remove(sendingPinger);
            sendingPinger.Dispose();
        } // end method

        #endregion

        #region Private Data

        private string ipAddressBase_m;
        private List<Ping> lanPingers_m;
        private bool ipAddressBaseSet_m;

        #endregion

    } // end class
} // end namespace
