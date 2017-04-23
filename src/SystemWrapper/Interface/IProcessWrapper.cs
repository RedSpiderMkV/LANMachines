using System;
using System.Diagnostics;
using System.IO;

namespace SystemWrapper.Interface
{
    /// <summary>
    /// Interface to the wrapper around <see cref="Process"/>.
    /// </summary>
    public interface IProcessWrapper : IDisposable
    {
        /// <summary>
        /// Get or set <see cref="ProcessStartInfo"/>.
        /// </summary>
        ProcessStartInfo StartInfo { get; set; }

        /// <summary>
        /// Get the standard output from the process.
        /// </summary>
        StreamReader StandardOuput { get; }

        /// <summary>
        /// Start the underlying process.
        /// </summary>
        bool Start();

        /// <summary>
        /// Wait indefinitely until the process exits.
        /// </summary>
        void WaitForExit();
    }
} 
