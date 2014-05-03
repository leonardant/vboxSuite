using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using Vbox_Home_GeneralInterfaceLibrary;

namespace UnitTests_RecordingsInterfaceLibraryAsync
{
    [TestClass]
    public class UnitTests_forRecordingsInterfaceLibraryAsync
    {
        const string serverProtAndIp_success = "http://10.100.107.204";
        const string serverProtAndIp_failure = "http://10.10.10.45";
        const string serverProtAndIp_MockDamagedResponse = "http://localhost:8080";

    }
}
