using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace LanDiscovery
{
    internal class ArpScanner : IDisposable
    {
        #region Public Methods

        public List<string> GetRespondingMachines()
        {
            return getArpScan();
        } // end method

        public void Dispose()
        {
            if (arpProcess_m != null)
            {
                arpProcess_m.Dispose();
            } // end if
        } // end method

        #endregion

        #region Private Methods

        private void initialiseArpScanProc()
        {
            arpProcess_m = new Process();
            ProcessStartInfo procInfo = new ProcessStartInfo()
            {
                FileName = "arp",
                Arguments = "-a",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            arpProcess_m.StartInfo = procInfo;
            arpProcess_m.Start();
        } // end method

        private List<string> getArpScan()
        {
            initialiseArpScanProc();

            string procOut = "";
            List<string> arpScanResults = new List<string>();
            while ((procOut = arpProcess_m.StandardOutput.ReadLine()) != null)
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

            arpProcess_m.WaitForExit();

            return arpScanResults;
        } // end method

        #endregion

        #region Private Data

        private Process arpProcess_m;

        #endregion

    } // end class
} // end namespace
