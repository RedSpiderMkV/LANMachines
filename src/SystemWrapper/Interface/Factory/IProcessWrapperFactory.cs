
namespace SystemWrapper.Interface.Factory
{
    /// <summary>
    /// Interface to the ProcessWrapperFactory - used to generate new instances of <see cref="IProcessWrapper"/>.
    /// </summary>
    public interface IProcessWrapperFactory
    {
        /// <summary>
        /// Get a new instance of <see cref="IProcessWrapper"/>. 
        /// </summary>
        /// <returns>New instance of <see cref="IProcessWrapper"/>.</returns>
        IProcessWrapper GetNewProcessWrapper();
    }
}
