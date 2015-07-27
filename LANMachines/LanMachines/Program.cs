using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;

namespace LanMachines
{
    class Program
    {
        private static int pingerCount = 0;

        static void Main(string[] args)
        {
            Ping ping = new Ping();
            List<Ping> pingers = new List<Ping>();

            for (int i = 1; i < 255; i++)
            {
                pingers.Add(new Ping());
            }

            string baseIp = "192.168.2.";

            foreach(Ping pinger in pingers)
            {
                pinger.PingCompleted += new PingCompletedEventHandler(pinger_PingCompleted);
                pinger.SendAsync(baseIp + pingerCount.ToString(), 100, null);

                pingerCount++;
            }

            while (pingerCount > 0)
            {
                System.Threading.Thread.Sleep(500);
            }

            foreach (Ping pinger in pingers)
            {
                pinger.PingCompleted -= pinger_PingCompleted;
                pinger.Dispose();
            } // end foreach
        }

        private static void pinger_PingCompleted(object sender, PingCompletedEventArgs e)
        {
            if (e.Reply.Status == IPStatus.Success)
            {
                Console.WriteLine(e.Reply.Address.ToString());
            }

            pingerCount--;
        }
    }
}
