using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RedSpider.LanDiscovery.Interface;

namespace RedSpider.LanDiscovery.Tests
{
    /// <summary>
    /// Test class to LanMachine.
    /// </summary>
    [TestClass]
    public class LanMachineTests
    {
        /// <summary>
        /// LanMachine constructor should return an instantiated object.
        /// </summary>
        [TestMethod]
        public void LanMachine_Constructor_Should_Succeed()
        {
            // Arrange
            IPAddress address = IPAddress.Parse("127.0.0.1");
            const string MACHINE_NAME = "TestMachine";

            // Act
            var sut = new LanMachine(address, MACHINE_NAME);

            // Assert
            Assert.IsNotNull(sut);
            Assert.IsInstanceOfType(sut, typeof(ILanMachine));
            Assert.AreEqual(address, sut.MachineIPAddress);
            Assert.AreEqual(MACHINE_NAME, sut.MachineName);
        }
    }
}
