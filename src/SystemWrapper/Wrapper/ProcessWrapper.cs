using System.Diagnostics;
using System.IO;
using RedSpider.SystemWrapper.Interface;

namespace RedSpider.SystemWrapper.Wrapper
{
    public class ProcessWrapper : IProcessWrapper
    {
        #region Properties

        /// <inheritdoc />
        public StreamReader StandardOuput { get { return process_m.StandardOutput; } }

        /// <inheritdoc />
        public ProcessStartInfo StartInfo
        {
            get { return process_m.StartInfo; }
            set { process_m.StartInfo = value; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Instantiate a new ProcessWrapper object.
        /// </summary>
        public ProcessWrapper()
        {
            process_m = new Process();
        }

        /// <inheritdoc />
        public bool Start()
        {
            return process_m.Start();
        }

        /// <inheritdoc />
        public void WaitForExit()
        {
            process_m.WaitForExit();
        }

        /// <summary>
        /// IDisposable implementation - release resources and clean up dependencies.
        /// </summary>
        public void Dispose()
        {
            process_m?.Dispose();
        }

        #endregion

        #region Private Data

        // Internal process being wrapped.
        private readonly Process process_m;

        #endregion
    }
}
