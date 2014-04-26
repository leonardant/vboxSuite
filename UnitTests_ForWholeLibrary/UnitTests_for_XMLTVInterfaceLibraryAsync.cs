using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vbox_Home_GeneralInterfaceLibrary;
using System.Xml.Linq;
using Vbox_Home_XMLTVInterfaceLibrary;

namespace UnitTests_GeneralInterfaceLibraryAsync
{
    [TestClass]
    public class UnitTests_for_GeneralInterfaceLibraryAsync
    {
        const string serverProtAndIp_success = "http://10.100.107.204";
        const string serverProtAndIp_failure = "http://10.100.107.45";

        [TestMethod]
        public void Test_canGetNumOfChannelsAsync()
        {
            Uri serverIp = new Uri(serverProtAndIp_success, UriKind.Absolute);

            PortableXMLTVInterfaceLibraryAsync alpha = new PortableXMLTVInterfaceLibraryAsync();

            var response = alpha.GetNumOfChannelsAsync(serverIp);

            XDocument xdoc = response.Result;

            Assert.IsNotNull(xdoc);
        }

        [TestMethod]
        public void Test_canGetXmltvEntireFileAsync()
        {
            Uri serverIp = new Uri(serverProtAndIp_success, UriKind.Absolute);

            PortableXMLTVInterfaceLibraryAsync alpha = new PortableXMLTVInterfaceLibraryAsync();

            var response = alpha.GetXmltvEntireFileAsync(serverIp);

            string result = response.Result.ToString();

            Assert.AreNotEqual("", result);
        }
    }
}
