using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using RedSpider.SystemWrapper.Interface;
using RedSpider.SystemWrapper.Interface.Proxy;

namespace RedSpider.LanDiscovery
{
    internal class LanPingerAsync : IDisposable
    {
        #region Public Methods

        /// <summary>
        /// Instantiate a new object to ping all IP addresses on a network
        /// asynchronously.
        /// </summary>
        /// <param name="timeout">Ping timeout in milliseconds.</param>
        /// <param name="networkInterfaceProxy">Network interface proxy.</param>
        public LanPingerAsync(int timeout, INetworkInterfaceProxy networkInterfaceProxy)
        {
            if(networkInterfaceProxy == null)
            {
                throw new NullReferenceException("LanPingerAsync: NetworkInterfaceProxy cannot be null.");
            }

            networkInterfaceProxy_m = networkInterfaceProxy;

            activePingers_m = 0;
            activeMachines_m = new List<IPAddress>();

            initialiseLanPingers();
            ipAddressBaseSet_m = initialiseIpBase();

            timeout_m = timeout;
        } // end method

        /// <summary>
        /// Get a list of IP addresses which responded to the ping.
        /// </summary>
        /// <returns>List of IP reachable addresses.</returns>
        public IEnumerable<IPAddress> GetActiveMachineAddresses()
        {
            if (!ipAddressBaseSet_m)
            {
                return null;
            } // end if

            pingAllAsync();

            while (activePingers_m > 0)
            {
                Thread.Sleep(timeout_m);
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
                Thread.Sleep(timeout_m);
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
                ping.SendAsync(ipAddressBase_m + activePingers_m.ToString(), timeout_m, null);
                activePingers_m++;
            } // end foreach
        } // end method

        /// <summary>
        /// Get the active ethernet network interface.
        /// </summary>
        /// <returns>Active ethernet network interface.</returns>
        private INetworkInterfaceWrapper getActiveEthernetInterface()
        {
            foreach (INetworkInterfaceWrapper networkInterface in networkInterfaceProxy_m.GetAllNetworkInterfaces())
            {
                // TODO: Bluetooth dongle appears to match this if active...
                if (networkInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet
                    && networkInterface.OperationalStatus == OperationalStatus.Up)
                {
                    return networkInterface;
                } // end if
            } // end foreach

            return null;
        } // end method

        /// <summary>
        /// Initialise the base IP address by determining what the gateway address is.
        /// </summary>
        /// <returns>True if address was initialised.</returns>
        private bool initialiseIpBase()
        {
            INetworkInterfaceWrapper networkInterface = getActiveEthernetInterface();
            if(networkInterface == null)
            {
                return false;
            }

            IPAddress gatewayAddress = networkInterface.GatewayIPAddressOfFirstInterface();
            if (gatewayAddress == null)
            {
                return false;
            }

            ipAddressBase_m = gatewayAddress.ToString();

            return true;
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
                activeMachines_m.Add(e.Reply.Address);
            } // end if

            activePingers_m--;
        } // end method

        #endregion

        #region Private Data

        private string ipAddressBase_m;
        private List<Ping> lanPingers_m;
        private bool ipAddressBaseSet_m;
        private int activePingers_m;

        private readonly IList<IPAddress> activeMachines_m;
        private readonly int timeout_m;
        private readonly INetworkInterfaceProxy networkInterfaceProxy_m;

        #endregion

    } // end class
} // end namespace
