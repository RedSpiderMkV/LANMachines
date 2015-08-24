using System;
using System.Collections.Generic;

using LanDiscovery;

namespace LanMachines
{
    class Program
    {
        internal static void Main(string[] args)
        {
            LanDiscoveryManager lanDiscovery = new LanDiscoveryManager();
            List<string> lanMachines = lanDiscovery.GetNetworkMachines();

            foreach (string lanIp in lanMachines)
            {
                Console.WriteLine(lanIp);
            }

            Console.WriteLine("LAN ping complete");
            Console.ReadKey();
        } // end method
    } // end class
} // end namespace
