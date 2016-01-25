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
            Console.WriteLine("LAN Discovery Test Console v0.3");
            Console.WriteLine();

            LanDiscoveryManager lanDiscovery = new LanDiscoveryManager();
            List<LanMachine> lanMachines = lanDiscovery.GetNetworkMachines();

            Console.WriteLine("Following IP addresses found on network:");
            printDiscoveredAddresses(lanMachines);

            Console.WriteLine();
            Console.WriteLine("LAN discovery complete.");
        } // end method

        private static void printDiscoveredAddresses(List<LanDiscovery.LanMachine> lanMachines)
        {
            foreach (LanMachine machines in lanMachines)
            {
                Console.WriteLine(machines.MachineIPAddress + " " + machines.MachineName);
            } // end foreach
        } // end method
    } // end class
} // end namespace
