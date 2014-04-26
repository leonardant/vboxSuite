using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using Vbox_Home_GeneralInterfaceLibrary;

namespace UnitTests_GeneralInterfaceLibraryAsync
{
    [TestClass]
    public class UnitTests_for_XMLTVInterfaceLibraryAsync
    {
        const string serverProtAndIp_success = "http://10.100.107.204";
        const string serverProtAndIp_failure = "http://10.10.10.45";

        #region // Get System Time
        #region // Get System Time for Format Date And Time
        [TestMethod]
        public void Test_canGetSystemTimeResponseAsNotEmptyWithWorkingURIAsync_forFormatDateAndTime()
        {
            Uri serverIp = new Uri(serverProtAndIp_success, UriKind.Absolute);

            PortableGeneralInterfaceLibraryAsync alpha = new PortableGeneralInterfaceLibraryAsync();

            PortableGeneralInterfaceLibraryAsync.Enum_TimeFormat tf = PortableGeneralInterfaceLibraryAsync.Enum_TimeFormat.DateAndTime;

            var response = alpha.GetSystemTimeAsync(serverIp, tf);

            string result = response.Result.ToString();

            Assert.AreNotEqual("", result);
        }

        [TestMethod]
        public void Test_canGetSystemTimeResponseAsNotEmptyWithFailingURIAsync_forFormatDateAndTime()
        {
            Uri serverIp = new Uri(serverProtAndIp_failure, UriKind.Absolute);

            PortableGeneralInterfaceLibraryAsync alpha = new PortableGeneralInterfaceLibraryAsync();

            PortableGeneralInterfaceLibraryAsync.Enum_TimeFormat tf = PortableGeneralInterfaceLibraryAsync.Enum_TimeFormat.DateAndTime;

            var response = alpha.GetSystemTimeAsync(serverIp, tf);

            string result = response.Result.ToString();

            Assert.AreNotEqual("", result);
        }

        [TestMethod]
        public void Test_canGetSystemTimeResponseSuccessCodeWithWorkingURIAsync_forFormatDateAndTime()
        {
            Uri serverIp = new Uri(serverProtAndIp_success, UriKind.Absolute);

            PortableGeneralInterfaceLibraryAsync alpha = new PortableGeneralInterfaceLibraryAsync();

            PortableGeneralInterfaceLibraryAsync.Enum_TimeFormat tf = PortableGeneralInterfaceLibraryAsync.Enum_TimeFormat.DateAndTime;

            var response = alpha.GetSystemTimeAsync(serverIp, tf);

            var successCode = (from node in response.Result.Descendants("Status")
                               select new
                               {
                                   ErrorCode = node.Element("ErrorCode").Value.ToString()
                               }).Single();

            Assert.AreEqual("0", successCode.ErrorCode);
        }

        [TestMethod]
        public void Test_canGetSystemTimeResponseFailureCodeWithFailingURIAsync_forFormatDateAndTime()
        {
            Uri serverIp = new Uri(serverProtAndIp_failure, UriKind.Absolute);

            PortableGeneralInterfaceLibraryAsync alpha = new PortableGeneralInterfaceLibraryAsync();

            PortableGeneralInterfaceLibraryAsync.Enum_TimeFormat tf = PortableGeneralInterfaceLibraryAsync.Enum_TimeFormat.DateAndTime;

            var response = alpha.GetSystemTimeAsync(serverIp, tf);

            var successCode = (from node in response.Result.Descendants("Status")
                               select new
                               {
                                   ErrorCode = node.Element("ErrorCode").Value.ToString()
                               }).Single();

            Assert.AreEqual("10000", successCode.ErrorCode);
        }

        [TestMethod]
        public void Test_canGetSystemTimeResponseAndTimeCloseToRealTimeWithWorkingURIAsync_forFormatDateAndTime()
        {
            Uri serverIp = new Uri(serverProtAndIp_success, UriKind.Absolute);

            PortableGeneralInterfaceLibraryAsync alpha = new PortableGeneralInterfaceLibraryAsync();

            PortableGeneralInterfaceLibraryAsync.Enum_TimeFormat tf = PortableGeneralInterfaceLibraryAsync.Enum_TimeFormat.DateAndTime;

            var response = alpha.GetSystemTimeAsync(serverIp, tf);

            var replyTime = (from node in response.Result.Descendants("Reply")
                               select new
                               {
                                   Time = node.Element("Time").Value.ToString()
                               }).Single();

            DateTime dateTimeOnDevice;
            if (DateTime.TryParseExact(replyTime.Time, "ddd MMM dd,yyyy-hh:mm:ss tt", null, DateTimeStyles.None, out dateTimeOnDevice))
            {
                TimeSpan timeDifference = DateTime.Now - dateTimeOnDevice;

                Assert.IsTrue(timeDifference.Seconds < 5); 
            }
            else
            {
                Assert.Inconclusive("could not parse the datetime from the device, so cannot deterime if the times are close");
            }          
        }
        #endregion

        #region // Get System Time for Format Time
        [TestMethod]
        public void Test_canGetSystemTimeResponseAsNotEmptyWithWorkingURIAsync_forFormatTime()
        {
            Uri serverIp = new Uri(serverProtAndIp_success, UriKind.Absolute);

            PortableGeneralInterfaceLibraryAsync alpha = new PortableGeneralInterfaceLibraryAsync();

            PortableGeneralInterfaceLibraryAsync.Enum_TimeFormat tf = PortableGeneralInterfaceLibraryAsync.Enum_TimeFormat.Time;

            var response = alpha.GetSystemTimeAsync(serverIp, tf);

            string result = response.Result.ToString();

            Assert.AreNotEqual("", result);
        }

        [TestMethod]
        public void Test_canGetSystemTimeResponseAsNotEmptyWithFailingURIAsync_forFormatTime()
        {
            Uri serverIp = new Uri(serverProtAndIp_failure, UriKind.Absolute);

            PortableGeneralInterfaceLibraryAsync alpha = new PortableGeneralInterfaceLibraryAsync();

            PortableGeneralInterfaceLibraryAsync.Enum_TimeFormat tf = PortableGeneralInterfaceLibraryAsync.Enum_TimeFormat.Time;

            var response = alpha.GetSystemTimeAsync(serverIp, tf);

            string result = response.Result.ToString();

            Assert.AreNotEqual("", result);
        }

        [TestMethod]
        public void Test_canGetSystemTimeResponseSuccessCodeWithWorkingURIAsync_forFormatTime()
        {
            Uri serverIp = new Uri(serverProtAndIp_success, UriKind.Absolute);

            PortableGeneralInterfaceLibraryAsync alpha = new PortableGeneralInterfaceLibraryAsync();

            PortableGeneralInterfaceLibraryAsync.Enum_TimeFormat tf = PortableGeneralInterfaceLibraryAsync.Enum_TimeFormat.Time;

            var response = alpha.GetSystemTimeAsync(serverIp, tf);

            var successCode = (from node in response.Result.Descendants("Status")
                               select new
                               {
                                   ErrorCode = node.Element("ErrorCode").Value.ToString()
                               }).Single();

            Assert.AreEqual("0", successCode.ErrorCode);
        }

        [TestMethod]
        public void Test_canGetSystemTimeResponseFailureCodeWithFailingURIAsync_forFormatTime()
        {
            Uri serverIp = new Uri(serverProtAndIp_failure, UriKind.Absolute);

            PortableGeneralInterfaceLibraryAsync alpha = new PortableGeneralInterfaceLibraryAsync();

            PortableGeneralInterfaceLibraryAsync.Enum_TimeFormat tf = PortableGeneralInterfaceLibraryAsync.Enum_TimeFormat.Time;

            var response = alpha.GetSystemTimeAsync(serverIp, tf);

            var successCode = (from node in response.Result.Descendants("Status")
                               select new
                               {
                                   ErrorCode = node.Element("ErrorCode").Value.ToString()
                               }).Single();

            Assert.AreEqual("10000", successCode.ErrorCode);
        }

        [TestMethod]
        public void Test_canGetSystemTimeResponseAndTimeCloseToRealTimeWithWorkingURIAsync_forFormatTime()
        {
            Uri serverIp = new Uri(serverProtAndIp_success, UriKind.Absolute);

            PortableGeneralInterfaceLibraryAsync alpha = new PortableGeneralInterfaceLibraryAsync();

            PortableGeneralInterfaceLibraryAsync.Enum_TimeFormat tf = PortableGeneralInterfaceLibraryAsync.Enum_TimeFormat.Time;

            var response = alpha.GetSystemTimeAsync(serverIp, tf);

            var replyTime = (from node in response.Result.Descendants("Reply")
                             select new
                             {
                                 Time = node.Element("Time").Value.ToString()
                             }).Single();

            DateTime dateTimeOnDevice;
            if (DateTime.TryParseExact(replyTime.Time, "hh:mm:ss tt", null, DateTimeStyles.None, out dateTimeOnDevice))
            {
                TimeSpan timeDifference = DateTime.Now - dateTimeOnDevice;

                Assert.IsTrue(timeDifference.Seconds < 5);
            }
            else
            {
                Assert.Inconclusive("could not parse the datetime from the device, so cannot deterime if the times are close");
            }
        }
        #endregion

        #region // Get System Time for Format Date
        [TestMethod]
        public void Test_canGetSystemTimeResponseAsNotEmptyWithWorkingURIAsync_forFormatDate()
        {
            Uri serverIp = new Uri(serverProtAndIp_success, UriKind.Absolute);

            PortableGeneralInterfaceLibraryAsync alpha = new PortableGeneralInterfaceLibraryAsync();

            PortableGeneralInterfaceLibraryAsync.Enum_TimeFormat tf = PortableGeneralInterfaceLibraryAsync.Enum_TimeFormat.Date;

            var response = alpha.GetSystemTimeAsync(serverIp, tf);

            string result = response.Result.ToString();

            Assert.AreNotEqual("", result);
        }

        [TestMethod]
        public void Test_canGetSystemTimeResponseAsNotEmptyWithFailingURIAsync_forFormatDate()
        {
            Uri serverIp = new Uri(serverProtAndIp_failure, UriKind.Absolute);

            PortableGeneralInterfaceLibraryAsync alpha = new PortableGeneralInterfaceLibraryAsync();

            PortableGeneralInterfaceLibraryAsync.Enum_TimeFormat tf = PortableGeneralInterfaceLibraryAsync.Enum_TimeFormat.Date;

            var response = alpha.GetSystemTimeAsync(serverIp, tf);

            string result = response.Result.ToString();

            Assert.AreNotEqual("", result);
        }

        [TestMethod]
        public void Test_canGetSystemTimeResponseSuccessCodeWithWorkingURIAsync_forFormatDate()
        {
            Uri serverIp = new Uri(serverProtAndIp_success, UriKind.Absolute);

            PortableGeneralInterfaceLibraryAsync alpha = new PortableGeneralInterfaceLibraryAsync();

            PortableGeneralInterfaceLibraryAsync.Enum_TimeFormat tf = PortableGeneralInterfaceLibraryAsync.Enum_TimeFormat.Date;

            var response = alpha.GetSystemTimeAsync(serverIp, tf);

            var successCode = (from node in response.Result.Descendants("Status")
                               select new
                               {
                                   ErrorCode = node.Element("ErrorCode").Value.ToString()
                               }).Single();

            Assert.AreEqual("0", successCode.ErrorCode);
        }

        [TestMethod]
        public void Test_canGetSystemTimeResponseFailureCodeWithFailingURIAsync_forFormatDate()
        {
            Uri serverIp = new Uri(serverProtAndIp_failure, UriKind.Absolute);

            PortableGeneralInterfaceLibraryAsync alpha = new PortableGeneralInterfaceLibraryAsync();

            PortableGeneralInterfaceLibraryAsync.Enum_TimeFormat tf = PortableGeneralInterfaceLibraryAsync.Enum_TimeFormat.Date;

            var response = alpha.GetSystemTimeAsync(serverIp, tf);

            var successCode = (from node in response.Result.Descendants("Status")
                               select new
                               {
                                   ErrorCode = node.Element("ErrorCode").Value.ToString()
                               }).Single();

            Assert.AreEqual("10000", successCode.ErrorCode);
        }

        [TestMethod]
        public void Test_canGetSystemTimeResponseAndTimeCloseToRealTimeWithWorkingURIAsync_forFormatDate()
        {
            Uri serverIp = new Uri(serverProtAndIp_success, UriKind.Absolute);

            PortableGeneralInterfaceLibraryAsync alpha = new PortableGeneralInterfaceLibraryAsync();

            PortableGeneralInterfaceLibraryAsync.Enum_TimeFormat tf = PortableGeneralInterfaceLibraryAsync.Enum_TimeFormat.Date;

            var response = alpha.GetSystemTimeAsync(serverIp, tf);

            var replyTime = (from node in response.Result.Descendants("Reply")
                             select new
                             {
                                 Time = node.Element("Time").Value.ToString()
                             }).Single();

            DateTime dateTimeOnDevice;
            if (DateTime.TryParseExact(replyTime.Time, "ddd MMM dd,yyyy", null, DateTimeStyles.None, out dateTimeOnDevice))
            {
                TimeSpan timeDifference = DateTime.Now - dateTimeOnDevice;

                Assert.IsTrue(timeDifference.Hours < 24);
            }
            else
            {
                Assert.Inconclusive("could not parse the datetime from the device, so cannot deterime if the times are close");
            }
        }
        #endregion

        #region // Get System Time for Format XMLTV
        [TestMethod]
        public void Test_canGetSystemTimeResponseAsNotEmptyWithWorkingURIAsync_forFormatXMLTV()
        {
            Uri serverIp = new Uri(serverProtAndIp_success, UriKind.Absolute);

            PortableGeneralInterfaceLibraryAsync alpha = new PortableGeneralInterfaceLibraryAsync();

            PortableGeneralInterfaceLibraryAsync.Enum_TimeFormat tf = PortableGeneralInterfaceLibraryAsync.Enum_TimeFormat.XMLTV;

            var response = alpha.GetSystemTimeAsync(serverIp, tf);

            string result = response.Result.ToString();

            Assert.AreNotEqual("", result);
        }

        [TestMethod]
        public void Test_canGetSystemTimeResponseAsNotEmptyWithFailingURIAsync_forFormatXMLTV()
        {
            Uri serverIp = new Uri(serverProtAndIp_failure, UriKind.Absolute);

            PortableGeneralInterfaceLibraryAsync alpha = new PortableGeneralInterfaceLibraryAsync();

            PortableGeneralInterfaceLibraryAsync.Enum_TimeFormat tf = PortableGeneralInterfaceLibraryAsync.Enum_TimeFormat.XMLTV;

            var response = alpha.GetSystemTimeAsync(serverIp, tf);

            string result = response.Result.ToString();

            Assert.AreNotEqual("", result);
        }

        [TestMethod]
        public void Test_canGetSystemTimeResponseSuccessCodeWithWorkingURIAsync_forFormatXMLTV()
        {
            Uri serverIp = new Uri(serverProtAndIp_success, UriKind.Absolute);

            PortableGeneralInterfaceLibraryAsync alpha = new PortableGeneralInterfaceLibraryAsync();

            PortableGeneralInterfaceLibraryAsync.Enum_TimeFormat tf = PortableGeneralInterfaceLibraryAsync.Enum_TimeFormat.XMLTV;

            var response = alpha.GetSystemTimeAsync(serverIp, tf);

            var successCode = (from node in response.Result.Descendants("Status")
                               select new
                               {
                                   ErrorCode = node.Element("ErrorCode").Value.ToString()
                               }).Single();

            Assert.AreEqual("0", successCode.ErrorCode);
        }

        [TestMethod]
        public void Test_canGetSystemTimeResponseFailureCodeWithFailingURIAsync_forFormatXMLTV()
        {
            Uri serverIp = new Uri(serverProtAndIp_failure, UriKind.Absolute);

            PortableGeneralInterfaceLibraryAsync alpha = new PortableGeneralInterfaceLibraryAsync();

            PortableGeneralInterfaceLibraryAsync.Enum_TimeFormat tf = PortableGeneralInterfaceLibraryAsync.Enum_TimeFormat.XMLTV;

            var response = alpha.GetSystemTimeAsync(serverIp, tf);

            var successCode = (from node in response.Result.Descendants("Status")
                               select new
                               {
                                   ErrorCode = node.Element("ErrorCode").Value.ToString()
                               }).Single();

            Assert.AreEqual("10000", successCode.ErrorCode);
        }

        [TestMethod]
        public void Test_canGetSystemTimeResponseAndTimeCloseToRealTimeWithWorkingURIAsync_forFormatXMLTV()
        {
            Uri serverIp = new Uri(serverProtAndIp_success, UriKind.Absolute);

            PortableGeneralInterfaceLibraryAsync alpha = new PortableGeneralInterfaceLibraryAsync();

            PortableGeneralInterfaceLibraryAsync.Enum_TimeFormat tf = PortableGeneralInterfaceLibraryAsync.Enum_TimeFormat.XMLTV;

            var response = alpha.GetSystemTimeAsync(serverIp, tf);

            var replyTime = (from node in response.Result.Descendants("Reply")
                             select new
                             {
                                 Time = node.Element("Time").Value.ToString()
                             }).Single();

            DateTime dateTimeOnDevice;
            if (DateTime.TryParseExact(replyTime.Time, "yyyyMMddHHmmsszzz", null, DateTimeStyles.None, out dateTimeOnDevice))
            {
                TimeSpan timeDifference = DateTime.Now - dateTimeOnDevice;

                Assert.IsTrue(timeDifference.Seconds < 5);
            }
            else
            {
                Assert.Inconclusive("could not parse the datetime from the device, so cannot deterime if the times are close");
            }
        }
        #endregion
        #endregion

        #region // Get External Media Status Tests
        [TestMethod]
        public void Test_canGetExternalMediaStatusResponseAsNotEmptyWithWorkingURIAsync()
        {
            Uri serverIp = new Uri(serverProtAndIp_success, UriKind.Absolute);

            PortableGeneralInterfaceLibraryAsync alpha = new PortableGeneralInterfaceLibraryAsync();

            var response = alpha.GetExternalMediaStatusAsync(serverIp);

            string result = response.Result.ToString();

            Assert.AreNotEqual("", result);
        }

        [TestMethod]
        public void Test_canGetExternalMediaStatusResponseAsNotEmptyWithFailingURIAsync()
        {
            Uri serverIp = new Uri(serverProtAndIp_failure, UriKind.Absolute);

            PortableGeneralInterfaceLibraryAsync alpha = new PortableGeneralInterfaceLibraryAsync();

            var response = alpha.GetExternalMediaStatusAsync(serverIp);

            string result = response.Result.ToString();

            Assert.AreNotEqual("", result);
        }

        [TestMethod]
        public void Test_canGetExternalMediaStatusResponseSuccessCodeWithWorkingURIAsync()
        {
            Uri serverIp = new Uri(serverProtAndIp_success, UriKind.Absolute);

            PortableGeneralInterfaceLibraryAsync alpha = new PortableGeneralInterfaceLibraryAsync();

            var response = alpha.GetExternalMediaStatusAsync(serverIp);

            var successCode = (from node in response.Result.Descendants("Status")
                               select new
                               {
                                   ErrorCode = node.Element("ErrorCode").Value.ToString()
                               }).Single();

            Assert.AreEqual("0", successCode.ErrorCode);
        }

        [TestMethod]
        public void Test_canGetExternalMediaStatusResponseFailureCodeWithFailingURIAsync()
        {
            Uri serverIp = new Uri(serverProtAndIp_failure, UriKind.Absolute);

            PortableGeneralInterfaceLibraryAsync alpha = new PortableGeneralInterfaceLibraryAsync();

            var response = alpha.GetExternalMediaStatusAsync(serverIp);

            var successCode = (from node in response.Result.Descendants("Status")
                               select new
                               {
                                   ErrorCode = node.Element("ErrorCode").Value.ToString()
                               }).Single();

            Assert.AreEqual("10001", successCode.ErrorCode);
        }
        #endregion

        #region // Get Board Info Tests
        [TestMethod]
        public void Test_canGetBoardInfoResponseAsNotEmptyWithWorkingURIAsync()
        {
            Uri serverIp = new Uri(serverProtAndIp_success, UriKind.Absolute);

            PortableGeneralInterfaceLibraryAsync alpha = new PortableGeneralInterfaceLibraryAsync();

            var response = alpha.GetBoardInfoAsync(serverIp);

            string result = response.Result.ToString();

            Assert.AreNotEqual("", result);
        }

        [TestMethod]
        public void Test_canGetBoardInfoResponseAsNotEmptyWithFailingURIAsync()
        {
            Uri serverIp = new Uri(serverProtAndIp_failure, UriKind.Absolute);

            PortableGeneralInterfaceLibraryAsync alpha = new PortableGeneralInterfaceLibraryAsync();

            var response = alpha.GetBoardInfoAsync(serverIp);

            string result = response.Result.ToString();

            Assert.AreNotEqual("", result);
        }

        [TestMethod]
        public void Test_canGetBoardInfoResponseSuccessCodeWithWorkingURIAsync()
        {
            Uri serverIp = new Uri(serverProtAndIp_success, UriKind.Absolute);

            PortableGeneralInterfaceLibraryAsync alpha = new PortableGeneralInterfaceLibraryAsync();

            var response = alpha.GetBoardInfoAsync(serverIp);

            var successCode = (from node in response.Result.Descendants("Status")
                            select new
                            {
                                ErrorCode = node.Element("ErrorCode").Value.ToString()
                            }).Single();

            Assert.AreEqual("0", successCode.ErrorCode);
        }

        [TestMethod]
        public void Test_canGetBoardInfoResponseFailureCodeWithFailingURIAsync()
        {
            Uri serverIp = new Uri(serverProtAndIp_failure, UriKind.Absolute);

            PortableGeneralInterfaceLibraryAsync alpha = new PortableGeneralInterfaceLibraryAsync();

            var response = alpha.GetBoardInfoAsync(serverIp);

            var successCode = (from node in response.Result.Descendants("Status")
                               select new
                               {
                                   ErrorCode = node.Element("ErrorCode").Value.ToString()
                               }).Single();

            Assert.AreEqual("10002", successCode.ErrorCode);
        }
        #endregion
    }
}
