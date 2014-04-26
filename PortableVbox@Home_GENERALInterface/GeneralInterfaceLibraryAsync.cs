//-----------------------------------------------------------------------
// <copyright file="GeneralInterfaceLibraryAsync.cs" company="Anthony Leonard">
// Copyright (c) Anthony Leonard. All rights reserved.
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with this library. If not, see http://www.gnu.org/licenses/.
// </copyright>
// <author>Anthony Leonard</author>
// <date>26/03/2014 22:49:56</date>
//-----------------------------------------------------------------------

namespace Vbox_Home_GeneralInterfaceLibrary
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using Vbox_Home_CommonLibrary;

    /// <summary>
    /// Async Portable GENERAL Interface Library to the device.
    /// </summary>
    public class PortableGeneralInterfaceLibraryAsync : PortableInterfaceLibraryCommon
    {
        /// <summary>
        /// Async Get the Time from the device in the requested format
        /// </summary>
        /// <param name="u">the uri of the device</param>
        /// <param name="t">an enumeration from ENUM_TIMEFORMAT</param>
        /// <returns>returns an XDocument object representing success OR failure</returns>
        /// <remarks>error codes "10000 - server error" and "11000 - xml parse error"</remarks>
        public async Task<XDocument> GetSystemTimeAsync(Uri u, Enum_TimeFormat t)
        {
            string urlContents = string.Empty;

            try
            {
                HttpClient client = new HttpClient();
                string requestString = @u.ToString() + CommonRequestStringPortion + Enum_MethodName_GeneralInterface.QuerySystemTime.ToString() + "&TimeFormat=" + t.ToString();
                Task<string> getStringTask = client.GetStringAsync(requestString);
                urlContents = await getStringTask;
            }
            catch (Exception ex1)
            {
                urlContents = string.Format(this.FailedResponseXml, "10000", WebUtility.UrlEncode(ex1.ToString()));
            }

            try
            {
                return XDocument.Parse(urlContents);
            }
            catch (Exception ex2)
            {
                urlContents = string.Format(this.FailedResponseXml, "11000", WebUtility.UrlEncode(ex2.ToString()));
                return XDocument.Parse(urlContents);
            }
        }

        /// <summary>
        /// Async Get the External Media Status
        /// </summary>
        /// <param name="u">the uri of the device</param>
        /// <returns>returns an XDocument object representing success OR failure</returns>
        /// <remarks>error codes "10001 - server error" and "11001 - xml parse error"</remarks>
        public async Task<XDocument> GetExternalMediaStatusAsync(Uri u)
        {
            string urlContents = string.Empty;

            try
            {
                HttpClient client = new HttpClient();
                string requestString = @u.ToString() + CommonRequestStringPortion + Enum_MethodName_GeneralInterface.QueryExternalMediaStatus.ToString();
                Task<string> getStringTask = client.GetStringAsync(requestString);
                urlContents = await getStringTask;
            }
            catch (Exception ex1)
            {
                urlContents = string.Format(this.FailedResponseXml, "10001", WebUtility.UrlEncode(ex1.ToString()));
            }

            try
            {
                return XDocument.Parse(urlContents);
            }
            catch (Exception ex2)
            {
                urlContents = string.Format(this.FailedResponseXml, "11001", WebUtility.UrlEncode(ex2.ToString()));
                return XDocument.Parse(urlContents);
            }
        }

        /// <summary>
        /// Async Get the Devices Board Information
        /// </summary>
        /// <param name="u">the uri of the device</param>
        /// <returns>returns an XDocument object representing success OR failure</returns>
        /// <remarks>error codes "10002 - server error" and "11002 - xml parse error"</remarks>
        public async Task<XDocument> GetBoardInfoAsync(Uri u)
        {
            string urlContents = string.Empty;

            try
            {
                HttpClient client = new HttpClient();
                string requestString = @u.ToString() + CommonRequestStringPortion + Enum_MethodName_GeneralInterface.QueryBoardInfo.ToString();
                Task<string> getStringTask = client.GetStringAsync(requestString);
                urlContents = await getStringTask;
            }
            catch (Exception ex1)
            {
                urlContents = string.Format(this.FailedResponseXml, "10002", WebUtility.UrlEncode(ex1.ToString()));
            }

            try
            {
                return XDocument.Parse(urlContents);
            }
            catch (Exception ex2)
            {
                urlContents = string.Format(this.FailedResponseXml, "11002", WebUtility.UrlEncode(ex2.ToString()));
                return XDocument.Parse(urlContents);
            }
        }
    }
}
