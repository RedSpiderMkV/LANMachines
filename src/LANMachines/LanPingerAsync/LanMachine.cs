using System;
using System.Net;

namespace LanDiscovery
{
    public class LanMachine
    {
        #region Properties

        public IPAddress MachineIPAddress { get; private set; }
        public string MachineName { get; private set; }

        #endregion

        #region Public Methods

        public LanMachine(IPAddress address, string name)
        {
            MachineIPAddress = address;
            MachineName = name;
        } // end method

        #endregion
    } // end class
} // end namespace
