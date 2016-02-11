using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;

namespace LanDiscovery
{
    /// <summary>
    /// Identify all network machines which respond to an arp request.
    /// </summary>
    internal class ArpScanner : IDisposable
    {
        #region Public Methods

        /// <summary>
        /// Get all machines which respond to the arp request.
        /// </summary>
        /// <returns>List of responding machines.</returns>
        public List<IPAddress> GetRespondingMachines()
        {
            return getArpScan();
        } // end method

        /// <summary>
        /// Dispose of the arp runner process.
        /// </summary>
        public void Dispose()
        {
            if (arpProcess_m != null)
            {
                arpProcess_m.Dispose();
            } // end if
        } // end method

        #endregion

        #region Private Methods

        /// <summary>
        /// Initialise the arp scanner process.
        /// </summary>
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

        /// <summary>
        /// Get the arp scan responding machines.
        /// </summary>
        /// <returns>List of IP addresses which respond to the ARP command.</returns>
        private List<IPAddress> getArpScan()
        {
            initialiseArpScanProc();

            string procOut = "";
            List<IPAddress> arpScanResults = new List<IPAddress>();
            while ((procOut = arpProcess_m.StandardOutput.ReadLine()) != null)
            {
                string[] parts = procOut.Trim().Split(' ');

                if (parts.Length < 2 || parts[parts.Length - 1] == "invalid")
                {
                    continue;
                } // end if

                string address = parts[0];
                IPAddress ipAddress;
                if (!String.IsNullOrEmpty(address) && char.IsDigit(address[0]) && IPAddress.TryParse(address, out ipAddress))
                {
                    arpScanResults.Add(ipAddress);
                } // end if
            } // end while

            arpProcess_m.WaitForExit();

            return arpScanResults;
        } // end method

        #endregion

        #region Private Data

        // Arp command runner process.
        private Process arpProcess_m;

        #endregion

    } // end class
} // end namespace
