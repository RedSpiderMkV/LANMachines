using RedSpider.SystemWrapper.Interface;
using RedSpider.SystemWrapper.Interface.Factory;
using RedSpider.SystemWrapper.Wrapper;

namespace RedSpider.SystemWrapper.Factory
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
