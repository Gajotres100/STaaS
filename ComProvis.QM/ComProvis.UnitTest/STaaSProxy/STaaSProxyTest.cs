using ComProvis.Actions.Actions.STaaSProxy;
using ComProvis.Actions.Proxy.STaaSPlatformApi;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComProvis.UnitTest.STaaSProxy
{
    [TestClass]
    public class STaaSProxyTest
    {
        ISTaaSPlatformApi _sTaaSPlatformApi;
        ISTaaSSoap sTaaSSoap;

        [TestInitialize]
        public void Setup()
        {            
            var sTaaSSoapMock = new Mock<ISTaaSSoap>();
            var sTaaSPlatformApiMock = new Mock<ISTaaSPlatformApi>();
            sTaaSPlatformApiMock.Setup(r => r.DiskSpaceCreate(It.IsAny<string>()));
            _sTaaSPlatformApi = sTaaSPlatformApiMock.Object;
            sTaaSSoap = sTaaSSoapMock.Object;
        }

        [TestMethod]
        public void FolderNameNotNull_DiskSpaceCreate_SaveOk()
        {
            //Arrange
            sTaaSSoap.STaaSPlatformApi = _sTaaSPlatformApi;

            //Act
            sTaaSSoap.DiskSpaceCreate("klocna");

            //Assert
            Assert.IsInstanceOfType(sTaaSSoap, typeof(ISTaaSSoap));
        }
    }
}
