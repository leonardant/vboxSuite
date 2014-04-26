//-----------------------------------------------------------------------
// <copyright file="PortableXMLTVInterfaceLibraryAsync.cs" company="Anthony Leonard">
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
// <date>25/04/2014 09:02:39</date>
//-----------------------------------------------------------------------

namespace Vbox_Home_XMLTVInterfaceLibrary
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using Vbox_Home_CommonLibrary;

    /// <summary>
    /// Async Portable XMLTV Interface Library to the device.
    /// </summary>
    public class PortableXMLTVInterfaceLibraryAsync : PortableInterfaceLibraryCommon
    {
        /// <summary>
        /// Async Get the Number Of Channels from the device 
        /// </summary>
        /// <param name="u">the uri of the device</param>
        /// <returns>returns an XDocument object representing success OR failure</returns>
        /// <remarks>error codes "20000 - server error" and "21000 - xml parse error"</remarks>
        public async Task<XDocument> GetNumOfChannelsAsync(Uri u)
        {
            string urlContents = string.Empty;

            try
            {
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromMinutes(2) })
                {
                    string requestString = @u.ToString() + CommonRequestStringPortion + Enum_MethodName_XmltvInterface.QueryXmltvNumOfChannels.ToString();
                    Task<string> getStringTask = client.GetStringAsync(requestString);
                    urlContents = await getStringTask;
                }
            }
            catch (Exception ex1)
            {
                urlContents = string.Format(this.FailedResponseXml, "20000", WebUtility.UrlEncode(ex1.ToString()));
            }

            try
            {
                return XDocument.Parse(urlContents);
            }
            catch (Exception ex2)
            {
                urlContents = string.Format(this.FailedResponseXml, "21000", WebUtility.UrlEncode(ex2.ToString()));
                return XDocument.Parse(urlContents);
            }
        }

        /// <summary>
        /// Async Get the Entire XMLTV File (formatted in XMLTV format) from the device 
        /// </summary>
        /// <param name="u">the uri of the device</param>
        /// <returns>returns an XDocument object representing success OR failure</returns>
        /// <remarks>error codes "20001 - server error" and "21001 - xml parse error"</remarks>
        public async Task<XDocument> GetXmltvEntireFileAsync(Uri u)
        {
            string urlContents = string.Empty;

            try
            {
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromMinutes(2) })
                {
                    string requestString = @u.ToString() + CommonRequestStringPortion + Enum_MethodName_XmltvInterface.GetXmltvEntireFile.ToString();
                    Task<string> getStringTask = client.GetStringAsync(requestString);
                    urlContents = await getStringTask;
                }               
            }
            catch (Exception ex1)
            {
                urlContents = string.Format(this.FailedResponseXml, "20001", WebUtility.UrlEncode(ex1.ToString()));
            }

            try
            {
                return XDocument.Parse(urlContents);
            }
            catch (Exception ex2)
            {
                urlContents = string.Format(this.FailedResponseXml, "21001", WebUtility.UrlEncode(ex2.ToString()));
                return XDocument.Parse(urlContents);
            }
        }

        /// <summary>
        /// Async Get sub-range of channels and programs in XMLTV file format by range of channels and range of time
        /// </summary>
        /// <param name="u">the uri of the device</param>
        /// <param name="fromChIndex">a string representing the first channel in a range to query against</param>
        /// <param name="toChIndex">a string representing the last channel in a range to query against</param>
        /// <param name="startTime">(optional) The start time in XMLTV time format "20120315130000+0200"</param>
        /// <param name="endTime">(optional) The end time in XMLTV time format "20120315143000+0200"</param>
        /// <returns>returns an XDocument object representing success OR failure</returns>
        /// <remarks>error codes "20012 - server error" and "21012 - xml parse error" and "20013 - input error"</remarks>
        public async Task<XDocument> GetXmltvSectionAsync(Uri u, string fromChIndex, string toChIndex, string startTime = "", string endTime = "")
        {
            string urlContents = string.Empty;

            if (string.IsNullOrEmpty(fromChIndex) || string.IsNullOrEmpty(toChIndex))
            {
                urlContents = string.Format(this.FailedResponseXml, "20013", WebUtility.UrlEncode("null or empty channel index"));
            }
            else
            {
                try
                {
                    using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromMinutes(2) })
                    {
                        string requestString = @u.ToString() + CommonRequestStringPortion + Enum_MethodName_XmltvInterface.GetXmltvSection.ToString() + "&FromChIndex=" + fromChIndex + "&ToChIndex=" + toChIndex +
                                (string.IsNullOrEmpty(startTime) ? "&StartTime=" + startTime : string.Empty) +
                                (string.IsNullOrEmpty(endTime) ? "&EndTime=" + endTime : string.Empty);
                        Task<string> getStringTask = client.GetStringAsync(requestString);
                        urlContents = await getStringTask;
                    }
                }
                catch (Exception ex1)
                {
                    urlContents = string.Format(this.FailedResponseXml, "20012", WebUtility.UrlEncode(ex1.ToString()));
                }
            }

            try
            {
                return XDocument.Parse(urlContents);
            }
            catch (Exception ex2)
            {
                urlContents = string.Format(this.FailedResponseXml, "21012", WebUtility.UrlEncode(ex2.ToString()));
                return XDocument.Parse(urlContents);
            }
        }

        /// <summary>
        /// Async Get sub-range of channels and programs in XMLTV file format by range of channels and range of time
        /// </summary>
        /// <param name="u">the uri of the device</param>
        /// <param name="channelsList">a comma delimited string representing the channels in a range to query against</param>
        /// <param name="startTime">(optional) The start time in XMLTV time format "20120315130000+0200"</param>
        /// <param name="endTime">(optional) The end time in XMLTV time format "20120315143000+0200"</param>
        /// <returns>returns an XDocument object representing success OR failure</returns>
        /// <remarks>error codes "20014 - server error" and "21014 - xml parse error" and "20015 / 20016 - input error"</remarks>
        public async Task<XDocument> GetXmltvSectionAsync(Uri u, string channelsList, string startTime = "", string endTime = "")
        {
            string urlContents = string.Empty;

            if (string.IsNullOrEmpty(channelsList))
            {
                urlContents = string.Format(this.FailedResponseXml, "20015", WebUtility.UrlEncode("null or empty channel list"));
            }
            else if (!channelsList.Contains(","))
            {
                urlContents = string.Format(this.FailedResponseXml, "20016", WebUtility.UrlEncode("channel list does not appear to be a comma seperated list"));
            }
            else
            {
                try
                {
                    using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromMinutes(2) })
                    {
                        string requestString = @u.ToString() + CommonRequestStringPortion + Enum_MethodName_XmltvInterface.GetXmltvSection.ToString() + "&ChannelsList=" + channelsList +
                                (string.IsNullOrEmpty(startTime) ? "&StartTime=" + startTime : string.Empty) +
                                (string.IsNullOrEmpty(endTime) ? "&EndTime=" + endTime : string.Empty);
                        Task<string> getStringTask = client.GetStringAsync(requestString);
                        urlContents = await getStringTask;
                    }
                }
                catch (Exception ex1)
                {
                    urlContents = string.Format(this.FailedResponseXml, "20014", WebUtility.UrlEncode(ex1.ToString()));
                }
            }

            try
            {
                return XDocument.Parse(urlContents);
            }
            catch (Exception ex2)
            {
                urlContents = string.Format(this.FailedResponseXml, "21014", WebUtility.UrlEncode(ex2.ToString()));
                return XDocument.Parse(urlContents);
            }
        }

        /// <summary>
        /// Async get the XMLTV Channels List (formatted in XMLTV format) from the device 
        /// </summary>
        /// <param name="u">the uri of the device</param>
        /// <param name="fromChIndex">a string representing the first channel in a range to query against</param>
        /// <param name="toChIndex">a string representing the last channel in a range to query against</param>
        /// <returns>returns an XDocument object representing success OR failure</returns>
        /// <remarks>error codes "20002 - server error" and "21002 - xml parse error" and "20003 - input error"</remarks>
        public async Task<XDocument> GetXmltvChannelsListAsync(Uri u, string fromChIndex, string toChIndex)
        {
            string urlContents = string.Empty;

            if (string.IsNullOrEmpty(fromChIndex) || string.IsNullOrEmpty(toChIndex))
            {
                urlContents = string.Format(this.FailedResponseXml, "20003", WebUtility.UrlEncode("null or empty channel index"));
            }
            else
            {
                try
                {
                    using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromMinutes(2) })
                    {
                        string requestString = @u.ToString() + CommonRequestStringPortion + Enum_MethodName_XmltvInterface.GetXmltvChannelsList.ToString() + "&FromChIndex=" + fromChIndex + "&ToChIndex=" + toChIndex;
                        Task<string> getStringTask = client.GetStringAsync(requestString);
                        urlContents = await getStringTask;
                    }
                }
                catch (Exception ex1)
                {
                    urlContents = string.Format(this.FailedResponseXml, "20002", WebUtility.UrlEncode(ex1.ToString()));
                }
            }

            try
            {
                return XDocument.Parse(urlContents);
            }
            catch (Exception ex2)
            {
                urlContents = string.Format(this.FailedResponseXml, "21002", WebUtility.UrlEncode(ex2.ToString()));
                return XDocument.Parse(urlContents);
            }
        }

        /// <summary>
        /// Async get the XMLTV Channels List (formatted in XMLTV format) from the device
        /// </summary>
        /// <param name="u">the uri of the device</param>
        /// <param name="channelsList">a comma delimited string representing the channels in a range to query against</param>
        /// <returns>returns an XDocument object representing success OR failure</returns>
        /// <remarks>error codes "20004 - server error" and "21004 - xml parse error" and "20005 / 20006 - input error"</remarks>
        public async Task<XDocument> GetXmltvChannelsListAsync(Uri u, string channelsList)
        {
            string urlContents = string.Empty;

            if (string.IsNullOrEmpty(channelsList))
            {
                urlContents = string.Format(this.FailedResponseXml, "20005", WebUtility.UrlEncode("null or empty channel list"));
            }
            else if (!channelsList.Contains(","))
            {
                urlContents = string.Format(this.FailedResponseXml, "20006", WebUtility.UrlEncode("channel list does not appear to be a comma seperated list"));
            }
            else
            {
                try
                {
                    using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromMinutes(2) })
                    {
                        string requestString = @u.ToString() + CommonRequestStringPortion + Enum_MethodName_XmltvInterface.GetXmltvChannelsList.ToString() + "&ChannelsList=" + channelsList;
                        Task<string> getStringTask = client.GetStringAsync(requestString);
                        urlContents = await getStringTask;
                    }
                }
                catch (Exception ex1)
                {
                    urlContents = string.Format(this.FailedResponseXml, "20004", WebUtility.UrlEncode(ex1.ToString()));
                }
            }

            try
            {
                return XDocument.Parse(urlContents);
            }
            catch (Exception ex2)
            {
                urlContents = string.Format(this.FailedResponseXml, "21004", WebUtility.UrlEncode(ex2.ToString()));
                return XDocument.Parse(urlContents);
            }
        }

        /// <summary>
        /// Async Get List of programs belong to specific Channel in XMLTV file format
        /// </summary>
        /// <param name="u">the uri of the device</param>
        /// <param name="channelList">The index of the channel in a range to query against</param>
        /// <returns>returns an XDocument object representing success OR failure</returns>
        /// <remarks>error codes "20017 - server error" and "21017 - xml parse error" and "20018 / 20019 - input error"</remarks>
        public async Task<XDocument> GetXmltvProgramsListUsingChannelListAsync(Uri u, string channelList)
        {
            string urlContents = string.Empty;

            if (string.IsNullOrEmpty(channelList))
            {
                urlContents = string.Format(this.FailedResponseXml, "20018", WebUtility.UrlEncode("null or empty channel list"));
            }
            else if (!channelList.Contains(","))
            {
                urlContents = string.Format(this.FailedResponseXml, "20019", WebUtility.UrlEncode("channel list does not appear to be a comma seperated list"));
            }
            else
            {
                try
                {
                    using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromMinutes(2) })
                    {
                        string requestString = @u.ToString() + CommonRequestStringPortion + Enum_MethodName_XmltvInterface.GetXmltvProgramsList.ToString() + "&ChannelsList=" + channelList;
                        Task<string> getStringTask = client.GetStringAsync(requestString);
                        urlContents = await getStringTask;
                    }
                }
                catch (Exception ex1)
                {
                    urlContents = string.Format(this.FailedResponseXml, "20017", WebUtility.UrlEncode(ex1.ToString()));
                }
            }

            try
            {
                return XDocument.Parse(urlContents);
            }
            catch (Exception ex2)
            {
                urlContents = string.Format(this.FailedResponseXml, "21017", WebUtility.UrlEncode(ex2.ToString()));
                return XDocument.Parse(urlContents);
            }
        }

        /// <summary>
        /// Async Get List of programs belong to specific Channel in XMLTV file format
        /// </summary>
        /// <param name="u">the uri of the device</param>
        /// <param name="channelIndex">The index of the channel</param>
        /// <returns>returns an XDocument object representing success OR failure</returns>
        /// <remarks>error codes "20020 - server error" and "21020 - xml parse error" and "20021 / 20022 - input error"</remarks>
        public async Task<XDocument> GetXmltvProgramsListUsingChannelIndexAsync(Uri u, string channelIndex)
        {
            string urlContents = string.Empty;

            if (string.IsNullOrEmpty(channelIndex))
            {
                urlContents = string.Format(this.FailedResponseXml, "20021", WebUtility.UrlEncode("null or empty channel index"));
            }
            else if (channelIndex.Contains(","))
            {
                urlContents = string.Format(this.FailedResponseXml, "20022", WebUtility.UrlEncode("channel index appears to be a comma seperated list"));
            }
            else
            {
                try
                {
                    using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromMinutes(2) })
                    {
                        string requestString = @u.ToString() + CommonRequestStringPortion + Enum_MethodName_XmltvInterface.GetXmltvProgramsList.ToString() + "&ChannelIndex=" + channelIndex;
                        Task<string> getStringTask = client.GetStringAsync(requestString);
                        urlContents = await getStringTask;
                    }
                }
                catch (Exception ex1)
                {
                    urlContents = string.Format(this.FailedResponseXml, "20020", WebUtility.UrlEncode(ex1.ToString()));
                }
            }

            try
            {
                return XDocument.Parse(urlContents);
            }
            catch (Exception ex2)
            {
                urlContents = string.Format(this.FailedResponseXml, "21020", WebUtility.UrlEncode(ex2.ToString()));
                return XDocument.Parse(urlContents);
            }
        }

        /// <summary>
        /// Async Get List of programs belong to specific Channel in XMLTV file format
        /// </summary>
        /// <param name="u">the uri of the device</param>
        /// <param name="channelIndex">The index of the channel</param>
        /// <returns>returns an XDocument object representing success OR failure</returns>
        /// <remarks>error codes "20020 - server error" and "21020 - xml parse error" and "20021 / 20022 - input error"</remarks>
        public async Task<XDocument> GetXmltvProgramsListUsingChannelIndexAsync(Uri u, int channelIndex)
        {
            return await this.GetXmltvProgramsListUsingChannelIndexAsync(u, channelIndex.ToString());
        }

        /// <summary>
        /// Async get the XMLTV Language Tracks(formatted in XMLTV format) from the device
        /// </summary>
        /// <param name="u">the uri of the device</param>
        /// <param name="channelID">the channel to query against</param>
        /// <param name="langTrackType">an enumeration from ENUM_METHODNAME_XMLTVINTERFACELANGUAGETRACKTYPES</param>
        /// <returns>returns an XDocument object representing success OR failure</returns>
        /// <remarks>error codes "20008 - server error" and "21008 - xml parse error" and "20007 - input error"</remarks>
        public async Task<XDocument> GetXmltvLanguageTracksAsync(Uri u, string channelID, Enum_MethodName_XmltvInterfaceLanguageTrackTypes langTrackType)
        {
            string urlContents = string.Empty;

            if (string.IsNullOrEmpty(channelID))
            {
                urlContents = string.Format(this.FailedResponseXml, "20007", WebUtility.UrlEncode("null or empty channel ID"));
            }
            else
            {
                try
                {
                    using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromMinutes(2) })
                    {
                        string requestString = @u.ToString() + CommonRequestStringPortion + Enum_MethodName_XmltvInterface.GetXmltvLanguageTracks.ToString() + "&ChannelID =" + channelID + "&Type=" + langTrackType.ToString();
                        Task<string> getStringTask = client.GetStringAsync(requestString);
                        urlContents = await getStringTask;
                    }
                }
                catch (Exception ex1)
                {
                    urlContents = string.Format(this.FailedResponseXml, "20008", WebUtility.UrlEncode(ex1.ToString()));
                }
            }

            try
            {
                return XDocument.Parse(urlContents);
            }
            catch (Exception ex2)
            {
                urlContents = string.Format(this.FailedResponseXml, "21008", WebUtility.UrlEncode(ex2.ToString()));
                return XDocument.Parse(urlContents);
            }
        }

        /// <summary>
        /// Async get the EPG data for a particular channel from the device
        /// </summary>
        /// <param name="u">the uri of the device</param>
        /// <param name="channelID">the channel to query against</param>
        /// <returns>returns an XDocument object representing success OR failure</returns>
        /// <remarks>error codes "20010 - server error" and "21010 - xml parse error" and "20009 - input error"</remarks>
        public async Task<XDocument> ScanEPGAsync(Uri u, string channelID)
        {
            string urlContents = string.Empty;

            if (string.IsNullOrEmpty(channelID))
            {
                urlContents = string.Format(this.FailedResponseXml, "20009", WebUtility.UrlEncode("null or empty channel ID"));
            }
            else
            {
                try
                {
                    using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromMinutes(2) })
                    {
                        string requestString = @u.ToString() + CommonRequestStringPortion + Enum_MethodName_XmltvInterface.ScanEPG.ToString() + "&ChannelID =" + channelID;
                        Task<string> getStringTask = client.GetStringAsync(requestString);
                        urlContents = await getStringTask;
                    }
                }
                catch (Exception ex1)
                {
                    urlContents = string.Format(this.FailedResponseXml, "20010", WebUtility.UrlEncode(ex1.ToString()));
                }
            }

            try
            {
                return XDocument.Parse(urlContents);
            }
            catch (Exception ex2)
            {
                urlContents = string.Format(this.FailedResponseXml, "21010", WebUtility.UrlEncode(ex2.ToString()));
                return XDocument.Parse(urlContents);
            }
        }

        /// <summary>
        /// Async get the DataBase Version used from the device
        /// </summary>
        /// <param name="u">the uri of the device</param>
        /// <returns>returns an XDocument object representing success OR failure</returns>
        /// <remarks>error codes "20011 - server error" and "21011 - xml parse error"</remarks>
        public async Task<XDocument> QueryDataBaseVersionAsync(Uri u)
        {
            string urlContents = string.Empty;

            try
            {
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromMinutes(2) })
                {
                    string requestString = @u.ToString() + CommonRequestStringPortion + Enum_MethodName_XmltvInterface.ScanEPG.ToString();
                    Task<string> getStringTask = client.GetStringAsync(requestString);
                    urlContents = await getStringTask;
                }
            }
            catch (Exception ex1)
            {
                urlContents = string.Format(this.FailedResponseXml, "20011", WebUtility.UrlEncode(ex1.ToString()));
            }

            try
            {
                return XDocument.Parse(urlContents);
            }
            catch (Exception ex2)
            {
                urlContents = string.Format(this.FailedResponseXml, "21011", WebUtility.UrlEncode(ex2.ToString()));
                return XDocument.Parse(urlContents);
            }
        }
    }
}
