using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;

using LanPinger;

namespace LanMachines
{
    class Program
    {
        internal static void Main(string[] args)
        {
            LanPingerAsync asyncPinger = new LanPingerAsync(255);
            asyncPinger.PingAllAsync();

            Console.ReadKey();
        }
    }
}
