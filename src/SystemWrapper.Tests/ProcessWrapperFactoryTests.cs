using Microsoft.VisualStudio.TestTools.UnitTesting;
using RedSpider.SystemWrapper.Factory;
using RedSpider.SystemWrapper.Interface;
using RedSpider.SystemWrapper.Wrapper;

namespace RedSpider.SystemWrapper.Tests
{
    /// <summary>
    /// Test class to the ProcessWrapperFactory.
    /// </summary>
    [TestClass]
    public class ProcessWrapperFactoryTests
    {
        /// <summary>
        /// Test that the factory returns a new instance of ProcessWrapper.
        /// </summary>
        [TestMethod]
        public void ProcessWrapperFactory_GetNewProcessWrapper_Returns_New_Instance_Of_ProcessWrapper()
        {
            // Arrange
            var sut = new ProcessWrapperFactory();

            // Act
            IProcessWrapper processWrapper = sut.GetNewProcessWrapper();

            // Assert
            Assert.IsNotNull(processWrapper);
            Assert.IsInstanceOfType(processWrapper, typeof(ProcessWrapper));
        }

        /// <summary>
        /// Test that each call to GetNewProcessWrapper returns a unique instance of ProcessWrapper.
        /// </summary>
        [TestMethod]
        public void ProcessWrapperFactory_GetNewProcessWrapper_Returns_Unique_Instance_Of_ProcessWrapper()
        {
            // Arrange
            var sut = new ProcessWrapperFactory();

            // Act
            IProcessWrapper processWrapper1 = sut.GetNewProcessWrapper();
            IProcessWrapper processWrapper2 = sut.GetNewProcessWrapper();

            // Assert
            Assert.IsNotNull(processWrapper1);
            Assert.IsNotNull(processWrapper2);
            Assert.AreNotSame(processWrapper1, processWrapper2);
        }
    }
}
