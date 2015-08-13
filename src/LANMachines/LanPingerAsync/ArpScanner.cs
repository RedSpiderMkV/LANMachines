using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace LanDiscovery
{
    public class ArpScanner
    {
        #region Public Methods

        public ArpScanner()
        {
            // default constructor..
        } // end method

        public List<string> GetRespondingMachines()
        {
            List<string> reachableMachines = new List<string>();

            List<string> arpRes = getArpScan();

            foreach (string res in arpRes)
            {
                Console.WriteLine(res);
            } // end foreach

            return reachableMachines;
        } // end method

        #endregion

        #region Private Methods

        private List<string> getArpScan()
        {
            Process arpProcess = new Process();
            ProcessStartInfo procInfo = new ProcessStartInfo()
            {
                FileName = "arp",
                Arguments = "-a",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            arpProcess.StartInfo = procInfo;
            arpProcess.Start();

            string procOut = "";
            List<string> arpScanResults = new List<string>();
            while ((procOut = arpProcess.StandardOutput.ReadLine()) != null)
            {
                string[] parts = procOut.Trim().Split(' ');

                if (parts.Length == 0)
                {
                    continue;
                } // end if

                string address = parts[0];
                if (!String.IsNullOrEmpty(address) && char.IsDigit(address[0]))
                {
                    arpScanResults.Add(address);
                } // end if
            } // end while

            arpProcess.WaitForExit();

            return arpScanResults;
        } // end method

        #endregion

    } // end class
} // end namespace
