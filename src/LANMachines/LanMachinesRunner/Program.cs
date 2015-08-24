using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;

using LanDiscovery;

namespace LanMachines
{
    class Program
    {
        internal static void Main(string[] args)
        {
            ArpScanner scanner = new ArpScanner();
            List<string> activeAddresses = new List<string>();
            foreach (string s in scanner.GetRespondingMachines())
            {
                activeAddresses.Add(s);
            } // end foreach

            //return;

            using (LanPingerAsync asyncPinger = new LanPingerAsync())
            {
                List<string> lanPings = asyncPinger.GetActiveMachines();
                foreach (string pings in lanPings)
                {
                    if (!activeAddresses.Contains(pings))
                    {
                        activeAddresses.Add(pings);
                    }
                }

                foreach (string address in activeAddresses)
                {
                    Console.WriteLine(address);
                }
            } // end using

            Console.WriteLine("LAN ping complete");
            Console.ReadKey();
        } // end method
    } // end class
} // end namespace
