using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RedSpider.LanDiscovery.Interface;
using RedSpider.SystemWrapper.Interface;
using RedSpider.SystemWrapper.Interface.Factory;
using Rhino.Mocks;

namespace RedSpider.LanDiscovery.Tests
{
    /// <summary>
    /// Test class for <see cref="ArpScanner"/>. 
    /// </summary>
    [TestClass]
    public class ArpScannerUnitTests
    {
        private IProcessWrapperFactory _processWrapperFactory;

        /// <summary>
        /// Initialise test class prior to each test.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            _processWrapperFactory = MockRepository.GenerateMock<IProcessWrapperFactory>();
        }

        /// <summary>
        /// Test that an exception is thrown if the ProcessWrapperFactory is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ArpScanner_Constructor_Throws_Exception_If_ProcessWrapperFactory_Is_Null()
        {
            try
            {
                // Act
                new ArpScanner(null);
            }
            catch(NullReferenceException exception)
            {
                // Assert
                Assert.AreEqual("ArpScanner: ProcessWrapper cannot be null.", exception.Message);
                throw;
            }
        }

        /// <summary>
        /// Test that the constructor instantiates an ArpScanner object correctly.
        /// </summary>
        [TestMethod]
        public void ArpScanner_Constructor_Succeeds()
        {
            // Act
            var sut = new ArpScanner(_processWrapperFactory);

            // Assert
            Assert.IsNotNull(sut);
            Assert.IsInstanceOfType(sut, typeof(IArpScanner));
        }

        /// <summary>
        /// Test that GetRespondingMachines makes a call to retrieve a ProcessWrapper from the factory.
        /// </summary>
        [TestMethod]
        public void ArpScanner_GetRespondingMachines_Retrieves_New_ProcessWrapper_From_Factory()
        {
            // Arrange
            IProcessWrapper mockProcessWrapper = GetMockProcessWrapper(new string[] { null });

            _processWrapperFactory.Stub(x => x.GetNewProcessWrapper()).Return(mockProcessWrapper);
            var sut = new ArpScanner(_processWrapperFactory);

            // Act
            sut.GetRespondingMachines();

            // Assert
            _processWrapperFactory.AssertWasCalled(x => x.GetNewProcessWrapper(), options => options.Repeat.Once());
        }

        /// <summary>
        /// Ensure the scan process is started when GetRespondingMachines is called.
        /// </summary>
        [TestMethod]
        public void ArpScanner_GetRespondingMachines_Starts_ArpScan_Process()
        {
            // Arrange
            IProcessWrapper mockProcessWrapper = GetMockProcessWrapper(new string[] { null });

            _processWrapperFactory.Stub(x => x.GetNewProcessWrapper()).Return(mockProcessWrapper);
            var sut = new ArpScanner(_processWrapperFactory);

            // Act
            sut.GetRespondingMachines();

            // Assert
            mockProcessWrapper.AssertWasCalled(x => x.Start(), options => options.Repeat.Once());
        }

        /// <summary>
        /// Ensure GetRespondingMachines waits for the scan process to exit.
        /// </summary>
        [TestMethod]
        public void ArpScanner_GetRespondingMachines_Waits_For_Process_To_Exit()
        {
            // Arrange
            IProcessWrapper mockProcessWrapper = GetMockProcessWrapper(new string[] { null });

            _processWrapperFactory.Stub(x => x.GetNewProcessWrapper()).Return(mockProcessWrapper);
            var sut = new ArpScanner(_processWrapperFactory);

            // Act
            sut.GetRespondingMachines();

            // Assert
            mockProcessWrapper.AssertWasCalled(x => x.WaitForExit(), options => options.Repeat.Once());
        }

        /// <summary>
        /// GetRespondingMachines should continue if the output value from ReadLine is invalid.
        /// </summary>
        [TestMethod]
        public void ArpScanner_GetRespondingMachines_Continues_If_ReadLine_Parts_Are_Less_Than_Two()
        {
            GetRespondingMachinesShouldContinueIfReadLineContentInvalid_TestRunner("Test");
        }

        /// <summary>
        /// GetRespondingMachines should continue if the output value from ReadLine is invalid.
        /// </summary>
        [TestMethod]
        public void ArpScanner_GetRespondingMachines_Continues_If_ReadLine_Ends_With_Invalid()
        {
            GetRespondingMachinesShouldContinueIfReadLineContentInvalid_TestRunner("127.0.0.1 invalid");
        }

        /// <summary>
        /// Test that the responding IP adddresses are returned.
        /// </summary>
        [TestMethod]
        public void ArpScanner_GetRespondingMachines_Should_Return_Responding_IP_Address_List()
        {
            // Arrange
            const int IP_ADDRESS_COUNT = 2;
            const string IP_ADDRESS1 = "127.0.0.1";
            const string IP_ADDRESS2 = "192.168.1.1";
            IProcessWrapper mockProcessWrapper = GetMockProcessWrapper(new string[] { IP_ADDRESS1 + " OK", IP_ADDRESS2 + " OK", null });

            _processWrapperFactory.Stub(x => x.GetNewProcessWrapper()).Return(mockProcessWrapper);
            var sut = new ArpScanner(_processWrapperFactory);

            // Act
            IEnumerable<IPAddress> scanResults = sut.GetRespondingMachines();

            // Assert
            Assert.AreEqual(IP_ADDRESS_COUNT, scanResults.Count());
            Assert.AreEqual(IP_ADDRESS1, scanResults.ElementAt(0).ToString());
            Assert.AreEqual(IP_ADDRESS2, scanResults.ElementAt(1).ToString());
        }

        /// <summary>
        /// Test that only valid IP addresses are returned.
        /// </summary>
        [TestMethod]
        public void ArpScanner_GetResponding_Machines_Only_Returns_Valid_IP_Addresses()
        {
            // Arrange
            const int IP_ADDRESS_COUNT = 1;
            const string INVALID_IP_ADDRESS = "300.0.0.1";
            const string VALID_IP_ADDRESS = "127.0.0.1";

            IProcessWrapper mockProcessWrapper = GetMockProcessWrapper(new string[] { INVALID_IP_ADDRESS + " OK", VALID_IP_ADDRESS + " OK", null });

            _processWrapperFactory.Stub(x => x.GetNewProcessWrapper()).Return(mockProcessWrapper);
            var sut = new ArpScanner(_processWrapperFactory);

            // Act
            IEnumerable<IPAddress> scanResults = sut.GetRespondingMachines();

            // Assert
            Assert.AreEqual(IP_ADDRESS_COUNT, scanResults.Count());
            Assert.AreEqual(VALID_IP_ADDRESS, scanResults.First().ToString());
        }

        #region Private Methods

        /// <summary>
        /// Test runner - Test that GetRespondingMachines continues if ReadLine content is invalid.
        /// </summary>
        /// <param name="content">ReadLine content.</param>
        private void GetRespondingMachinesShouldContinueIfReadLineContentInvalid_TestRunner(string content)
        {
            // Arrange
            IProcessWrapper mockProcessWrapper = GetMockProcessWrapper(new string[] { content, null });

            _processWrapperFactory.Stub(x => x.GetNewProcessWrapper()).Return(mockProcessWrapper);
            var sut = new ArpScanner(_processWrapperFactory);

            // Act
            IEnumerable<IPAddress> scanResults = sut.GetRespondingMachines();

            // Assert
            Assert.IsFalse(scanResults.Any());
        }

        /// <summary>
        /// Get a mocked process wrapper which will return the provided string when ReadLine is called on it.
        /// </summary>
        /// <param name="readLineContents">Read line content values.</param>
        /// <returns>Mocked process wrapper.</returns>
        private static IProcessWrapper GetMockProcessWrapper(string[] readLineContents)
        {
            IStreamReaderWrapper streamReaderWrapper = MockRepository.GenerateMock<IStreamReaderWrapper>();
            foreach(string content in readLineContents)
            {
                streamReaderWrapper.Stub(x => x.ReadLine()).Return(content).Repeat.Once();
            }

            IProcessWrapper mockProcessWrapper = MockRepository.GenerateMock<IProcessWrapper>();
            mockProcessWrapper.Stub(x => x.StandardOuput).Return(streamReaderWrapper);

            return mockProcessWrapper;
        }

        #endregion
    }
}
