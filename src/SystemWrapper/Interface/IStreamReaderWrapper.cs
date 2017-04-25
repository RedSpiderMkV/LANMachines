
namespace RedSpider.SystemWrapper.Interface
{
    /// <summary>
    /// Interface to the wrapper around a stream reader.
    /// </summary>
    public interface IStreamReaderWrapper
    {
        /// <summary>
        /// Read a line of characters from the stream.
        /// </summary>
        /// <returns>Line of characters.</returns>
        string ReadLine();
    }
}
