using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;

namespace LanPinger
{
    public class LanPingerAsync : IDisposable
    {
        #region Public Methods

        /// <summary>
        /// Instantiate a new object to ping all IP addresses on a network
        /// asynchronously.
        /// </summary>
        public LanPingerAsync()
        {
            activePingers_m = 0;
            activeMachines_m = new List<string>();

            initialiseLanPingers();
            ipAddressBaseSet_m = initialiseIpBase();
        } // end method

        /// <summary>
        /// Get a list of IP addresses which responded to the ping.
        /// </summary>
        /// <returns>List of IP reachable addresses.</returns>
        public List<string> GetActiveMachines()
        {
            pingAllAsync();

            while (activePingers_m > 0)
            {
                Thread.Sleep(500);
            } // end while

            return activeMachines_m;
        } // end method

        /// <summary>
        /// Dispose of object, dispose of all Ping objects.
        /// </summary>
        public void Dispose()
        {
            while (activePingers_m > 0)
            {
                Thread.Sleep(500);
            } // end while

            foreach (Ping pinger in lanPingers_m)
            {
                pinger.PingCompleted -= ping_PingCompleted;
                pinger.Dispose();
            } // end foreach
        } // end method

        #endregion

        #region Private Methods

        /// <summary>
        /// Send ping to all machines asynchronously.
        /// </summary>
        private void pingAllAsync()
        {
            foreach (Ping ping in lanPingers_m)
            {
                ping.SendAsync(ipAddressBase_m + activePingers_m.ToString(), 500, null);
                activePingers_m++;
            } // end foreach
        } // end method

        /// <summary>
        /// Initialise the base IP address by determining what the gateway address is.
        /// </summary>
        /// <returns>True if address was initialised.</returns>
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
                    ipAddressBase_m = String.Format("{0}.{1}.{2}.", parts[0], parts[1], parts[2]);
                    
                    return true;
                } // end if
            } // end if

            return false;
        } // end method

        /// <summary>
        /// Initialise the Ping objects.
        /// </summary>
        private void initialiseLanPingers()
        {
            lanPingers_m = new List<Ping>();
            for (int i = 0; i < 255; i++)
            {
                Ping ping = new Ping();
                ping.PingCompleted += new PingCompletedEventHandler(ping_PingCompleted);
                
                lanPingers_m.Add(ping);
            } // end method
        } // end method

        /// <summary>
        /// Ping complete event handler.
        /// </summary>
        /// <param name="sender">Ping object which has completed the ping.</param>
        /// <param name="e">Event arg.</param>
        private void ping_PingCompleted(object sender, PingCompletedEventArgs e)
        {
            if (e.Reply.Status == IPStatus.Success)
            {
                //Console.WriteLine(e.Reply.Address.ToString());
                activeMachines_m.Add(e.Reply.Address.ToString());
            } // end if

            activePingers_m--;
        } // end method

        #endregion

        #region Private Data

        private string ipAddressBase_m;
        private List<Ping> lanPingers_m;
        private List<string> activeMachines_m;
        private bool ipAddressBaseSet_m;
        private int activePingers_m;

        #endregion

    } // end class
} // end namespace
