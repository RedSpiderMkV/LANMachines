using System;
using System.Collections.Generic;
using System.Net;

using LanDiscovery;

namespace LanMachines
{
    internal class LanDiscoveryBlocking
    {
        public static void DiscoverLanMachines()
        {
            Console.Title = "LAN Discovery Runner";
            Console.WriteLine("LAN Discovery Test Console v0.2");
            Console.WriteLine();

            LanDiscoveryManager lanDiscovery = new LanDiscoveryManager();
            List<IPAddress> lanMachines = lanDiscovery.GetNetworkMachines();

            Console.WriteLine("Following IP addresses found on network:");
            printStringList(lanMachines);

            Console.WriteLine();
            Console.WriteLine("LAN discovery complete.");
        } // end method

        private static void printStringList(List<IPAddress> ipAddressList)
        {
            foreach (IPAddress address in ipAddressList)
            {
                Console.WriteLine(address.ToString());
            } // end foreach
        } // end method
    } // end class
} // end namespace
