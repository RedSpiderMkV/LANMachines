using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using RedSpider.LanDiscovery.Interface;
using RedSpider.SystemWrapper.Interface;
using RedSpider.SystemWrapper.Interface.Factory;

namespace RedSpider.LanDiscovery
{
    /// <summary>
    /// Identify all network machines which respond to an arp request.
    /// </summary>
    public class ArpScanner : IArpScanner
    {
        #region Public Methods

        /// <summary>
        /// Instantiate a new ArpScanner object with the provided ProcessWrapperFactory.
        /// </summary>
        /// <param name="processWrapperFactory">ProcessWrapperFactory - used to generate the process
        /// to run the ARP scan.</param>
        public ArpScanner(IProcessWrapperFactory processWrapperFactory)
        {
            if(processWrapperFactory == null)
            {
                throw new NullReferenceException("ArpScanner: ProcessWrapper cannot be null.");
            }

            processWrapperFactory_m = processWrapperFactory;
        }

        /// <inheritdoc />
        public IEnumerable<IPAddress> GetRespondingMachines()
        {
            var arpScanResults = new List<IPAddress>();
            using (IProcessWrapper arpScanProcess = getArpScanProc())
            {
                arpScanProcess.Start();

                string procOut = "";
                while ((procOut = arpScanProcess.StandardOuput.ReadLine()) != null)
                {
                    string[] parts = procOut.Trim().Split(' ');

                    if (parts.Length < 2 || parts[parts.Length - 1] == "invalid")
                    {
                        continue;
                    } // end if

                    string address = parts[0];
                    IPAddress ipAddress;
                    if (IPAddress.TryParse(address, out ipAddress))
                    {
                        arpScanResults.Add(ipAddress);
                    } // end if
                } // end while

                arpScanProcess.WaitForExit();
            } // end using

            return arpScanResults;
        } // end method

        #endregion

        #region Private Methods

        /// <summary>
        /// Retrieve an initialised ARP scanning process.
        /// </summary>
        /// <returns>ARP scanning process.</returns>
        private IProcessWrapper getArpScanProc()
        {
            var arpProcess = processWrapperFactory_m.GetNewProcessWrapper();
            var procInfo = new ProcessStartInfo()
            {
                FileName = "arp",
                Arguments = "-a",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            arpProcess.StartInfo = procInfo;

            return arpProcess;
        } // end method

        #endregion

        #region Private Data

        private readonly IProcessWrapperFactory processWrapperFactory_m;

        #endregion

    } // end class
} // end namespace
