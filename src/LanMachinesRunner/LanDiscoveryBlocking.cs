using System;
using System.Collections.Generic;
using RedSpider.LanDiscovery;

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

            printDiscoveredAddresses(lanMachines);

            Console.WriteLine("\nLAN discovery complete.\n");
        } // end method

        private static void printDiscoveredAddresses(List<LanMachine> lanMachines)
        {
            Console.WriteLine("Following machines found on network:\n");
            Console.WriteLine("Address\t\t| Name");
            Console.WriteLine("------------------------------------");

            foreach (LanMachine machines in lanMachines)
            {
                Console.WriteLine(machines.MachineIPAddress + "\t| " + machines.MachineName);
            } // end foreach
        } // end method
    } // end class
} // end namespace
