using SystemWrapper.Interface;
using SystemWrapper.Interface.Factory;
using SystemWrapper.Wrapper;

namespace SystemWrapper.Factory
{
    /// <summary>
    /// ProcessWrapperFactory - used to generate new instances of <see cref="IProcessWrapper"/>.
    /// </summary>
    public class ProcessWrapperFactory : IProcessWrapperFactory
    {
        /// <inheritdoc />
        public IProcessWrapper GetNewProcessWrapper()
        {
            return new ProcessWrapper();
        }
    }
}
