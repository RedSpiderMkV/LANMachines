using System.IO;
using RedSpider.SystemWrapper.Interface;

namespace RedSpider.SystemWrapper.Wrapper
{
    /// <summary>
    /// StreamReaderWrapper - a wrapper around <see cref="StreamReader"/>. 
    /// </summary>
    public class StreamReaderWrapper : IStreamReaderWrapper
    {
        #region Public Methods

        /// <summary>
        /// Instantiate a new wrapper around the stream reader.
        /// </summary>
        /// <param name="streamReader"></param>
        public StreamReaderWrapper(StreamReader streamReader)
        {
            streamReader_m = streamReader;
        }

        /// <inheritdoc />
        public string ReadLine()
        {
            return streamReader_m.ReadLine();
        }

        #endregion

        #region Private Data

        private readonly StreamReader streamReader_m;

        #endregion
    }
}
