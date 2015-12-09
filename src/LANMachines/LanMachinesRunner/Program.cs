using System;
using System.Collections.Generic;
using System.Net;

using LanDiscovery;

namespace LanMachines
{
    internal class Program
    {
        internal static void Main(string[] args)
        {
            LanDiscoveryManager lanDiscovery = new LanDiscoveryManager();
            List<IPAddress> lanMachines = lanDiscovery.GetNetworkMachines();

            printStringList(lanMachines);

            Console.WriteLine("LAN discovery complete.");
            Console.WriteLine("Press any key to EXIT...");
            Console.ReadKey();
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
