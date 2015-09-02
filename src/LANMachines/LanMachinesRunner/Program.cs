using System;
using System.Collections.Generic;

using LanDiscovery;

namespace LanMachines
{
    internal class Program
    {
        internal static void Main(string[] args)
        {
            LanDiscoveryManager lanDiscovery = new LanDiscoveryManager();
            List<string> lanMachines = lanDiscovery.GetNetworkMachines();

            printStringList(lanMachines);

            Console.WriteLine("LAN ping complete");
            Console.ReadKey();
        } // end method

        private static void printStringList(List<string> stringList)
        {
            foreach (string s in stringList)
            {
                Console.WriteLine(s);
            } // end foreach
        } // end method

    } // end class
} // end namespace
