using System;

namespace LanMachines
{
    internal class Program
    {
        internal static void Main(string[] args)
        {
            LanDiscoveryBlocking.DiscoverLanMachines();

            Console.WriteLine("Press any key to EXIT...");
            Console.ReadKey();
        } // end method
    } // end class
} // end namespace
