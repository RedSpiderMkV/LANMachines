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

            string arpRes = getArpScan();
            Console.WriteLine(arpRes);

            return reachableMachines;
        } // end method

        #endregion

        #region Private Methods

        private string getArpScan()
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

            string output = arpProcess.StandardOutput.ReadToEnd();

            arpProcess.WaitForExit();

            return output;
        } // end method

        #endregion

    } // end class
} // end namespace
