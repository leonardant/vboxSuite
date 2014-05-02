//-----------------------------------------------------------------------
// <copyright file="RecordingsInterfaceLibraryAsync.cs" company="Anthony Leonard">
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

namespace PortableVbox_GHome_RecordingsInterface
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using Vbox_Home_CommonLibrary;

    /// <summary>
    /// Async Portable RECORDINGS Interface Library to the device.
    /// </summary>
    public class RecordingsInterfaceLibraryAsync : PortableInterfaceLibraryCommon
    {
        /// <summary>
        /// Async Schedule a Channel and time to Record
        /// </summary>
        /// <param name="u">the uri of the device</param>
        /// <param name="channelID">the channelID to record</param>
        /// <param name="startTime">the start time of the recording</param>
        /// <param name="endTime">the end time of the recording</param>
        /// <returns>returns an XDocument object representing success OR failure</returns>
        /// <remarks>error codes "30001 - server error" and "31001 - xml parse error" and "30002 / 30003 / 30004 - input error"</remarks>
        public async Task<XDocument> ScheduleChannelRecordAsync(Uri u, string channelID, string startTime, string endTime)
        {
            string urlContents = string.Empty;

            if (string.IsNullOrEmpty(channelID))
            {
                urlContents = string.Format(this.FailedResponseXml, "30002", WebUtility.UrlEncode("null or empty channel id"));
            }
            else if (string.IsNullOrEmpty(startTime))
            {
                urlContents = string.Format(this.FailedResponseXml, "30003", WebUtility.UrlEncode("null or empty start time"));
            }
            else if (string.IsNullOrEmpty(endTime))
            {
                urlContents = string.Format(this.FailedResponseXml, "30004", WebUtility.UrlEncode("null or empty end time"));
            }
            else
            {
                try
                {
                    using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromMinutes(2) })
                    {
                        string requestString = @u.ToString() + CommonRequestStringPortion + Enum_MethodNames_RecordingsInterface.ScheduleChannelRecord.ToString() + "&ChannelID=" + channelID +
                                "&StartTime=" + startTime +
                                "&EndTime=" + endTime;
                        Task<string> getStringTask = client.GetStringAsync(requestString);
                        urlContents = await getStringTask;
                    }
                }
                catch (Exception ex1)
                {
                    urlContents = string.Format(this.FailedResponseXml, "30001", WebUtility.UrlEncode(ex1.ToString()));
                }
            }

            try
            {
                return XDocument.Parse(urlContents);
            }
            catch (Exception ex2)
            {
                urlContents = string.Format(this.FailedResponseXml, "31001", WebUtility.UrlEncode(ex2.ToString()));
                return XDocument.Parse(urlContents);
            }
        }

        /// <summary>
        /// Async Schedule a Channel and time to Record
        /// </summary>
        /// <param name="u">the uri of the device</param>
        /// <param name="channelID">the channelID to record</param>
        /// <param name="programTitle">The Program name from XMLTVs title field</param>
        /// <param name="startTime">the start time of the recording</param>
        /// <returns>returns an XDocument object representing success OR failure</returns>
        /// <remarks>error codes "30005 - server error" and "31005 - xml parse error" and "30006 / 30007 / 30008 - input error"</remarks>
        public async Task<XDocument> ScheduleProgramRecordAsync(Uri u, string channelID, string programTitle, string startTime)
        {
            string urlContents = string.Empty;

            if (string.IsNullOrEmpty(channelID))
            {
                urlContents = string.Format(this.FailedResponseXml, "30006", WebUtility.UrlEncode("null or empty channel id"));
            }
            else if (string.IsNullOrEmpty(startTime))
            {
                urlContents = string.Format(this.FailedResponseXml, "30007", WebUtility.UrlEncode("null or empty start time"));
            }
            else if (string.IsNullOrEmpty(programTitle))
            {
                urlContents = string.Format(this.FailedResponseXml, "30008", WebUtility.UrlEncode("null or empty end time"));
            }
            else
            {
                try
                {
                    using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromMinutes(2) })
                    {
                        string requestString = @u.ToString() + CommonRequestStringPortion + Enum_MethodNames_RecordingsInterface.ScheduleProgramRecord.ToString() + "&ChannelID=" + channelID +
                                "&StartTime=" + startTime +
                                "&ProgramTitle=" + programTitle;
                        Task<string> getStringTask = client.GetStringAsync(requestString);
                        urlContents = await getStringTask;
                    }
                }
                catch (Exception ex1)
                {
                    urlContents = string.Format(this.FailedResponseXml, "30005", WebUtility.UrlEncode(ex1.ToString()));
                }
            }

            try
            {
                return XDocument.Parse(urlContents);
            }
            catch (Exception ex2)
            {
                urlContents = string.Format(this.FailedResponseXml, "31005", WebUtility.UrlEncode(ex2.ToString()));
                return XDocument.Parse(urlContents);
            }
        }

        /// <summary>
        /// Async Start a Recording now
        /// </summary>
        /// <param name="u">the uri of the device</param>
        /// <param name="channelID">the channelID to record</param>
        /// <returns>returns an XDocument object representing success OR failure</returns>
        /// <remarks>error codes "30009 - server error" and "31009 - xml parse error" and "30010 - input error"</remarks>
        public async Task<XDocument> StartRecordAsync(Uri u, string channelID)
        {
            string urlContents = string.Empty;

            if (string.IsNullOrEmpty(channelID))
            {
                urlContents = string.Format(this.FailedResponseXml, "30010", WebUtility.UrlEncode("null or empty channel id"));
            }
            else
            {
                try
                {
                    using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromMinutes(2) })
                    {
                        string requestString = @u.ToString() + CommonRequestStringPortion + Enum_MethodNames_RecordingsInterface.StartRecord.ToString() + "&ChannelID=" + channelID;
                        Task<string> getStringTask = client.GetStringAsync(requestString);
                        urlContents = await getStringTask;
                    }
                }
                catch (Exception ex1)
                {
                    urlContents = string.Format(this.FailedResponseXml, "30009", WebUtility.UrlEncode(ex1.ToString()));
                }
            }

            try
            {
                return XDocument.Parse(urlContents);
            }
            catch (Exception ex2)
            {
                urlContents = string.Format(this.FailedResponseXml, "31009", WebUtility.UrlEncode(ex2.ToString()));
                return XDocument.Parse(urlContents);
            }
        }

        /// <summary>
        /// Async Stop a Recording now
        /// </summary>
        /// <param name="u">the uri of the device</param>
        /// <param name="channelID">the channelID to stop record</param>
        /// <returns>returns an XDocument object representing success OR failure</returns>
        /// <remarks>error codes "30011 - server error" and "31011 - xml parse error" and "30012 - input error"</remarks>
        public async Task<XDocument> StopRecordAsync(Uri u, string channelID)
        {
            string urlContents = string.Empty;

            if (string.IsNullOrEmpty(channelID))
            {
                urlContents = string.Format(this.FailedResponseXml, "30012", WebUtility.UrlEncode("null or empty channel id"));
            }
            else
            {
                try
                {
                    using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromMinutes(2) })
                    {
                        string requestString = @u.ToString() + CommonRequestStringPortion + Enum_MethodNames_RecordingsInterface.StopRecord.ToString() + "&ChannelID=" + channelID;
                        Task<string> getStringTask = client.GetStringAsync(requestString);
                        urlContents = await getStringTask;
                    }
                }
                catch (Exception ex1)
                {
                    urlContents = string.Format(this.FailedResponseXml, "30011", WebUtility.UrlEncode(ex1.ToString()));
                }
            }

            try
            {
                return XDocument.Parse(urlContents);
            }
            catch (Exception ex2)
            {
                urlContents = string.Format(this.FailedResponseXml, "31011", WebUtility.UrlEncode(ex2.ToString()));
                return XDocument.Parse(urlContents);
            }
        }

        /// <summary>
        /// Async Get Recordings List
        /// </summary>
        /// <param name="u">the uri of the device</param>
        /// <param name="externals">recordings from other VBOX devices - if not set defaults to true</param>
        /// <returns>returns an XDocument object representing success OR failure</returns>
        /// <remarks>error codes "30013 - server error" and "31013 - xml parse error"</remarks>
        public async Task<XDocument> GetRecordsListAsync(Uri u, bool externals = true)
        {
            string urlContents = string.Empty;

            string ext = "NO";

            if (externals.Equals(true))
            {
                ext = "YES";
            }

            try
            {
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromMinutes(2) })
                {
                    string requestString = @u.ToString() + CommonRequestStringPortion + Enum_MethodNames_RecordingsInterface.GetRecordsList.ToString() + "Externals=" + ext;
                    Task<string> getStringTask = client.GetStringAsync(requestString);
                    urlContents = await getStringTask;
                }
            }
            catch (Exception ex1)
            {
                urlContents = string.Format(this.FailedResponseXml, "30013", WebUtility.UrlEncode(ex1.ToString()));
            }

            try
            {
                return XDocument.Parse(urlContents);
            }
            catch (Exception ex2)
            {
                urlContents = string.Format(this.FailedResponseXml, "31013", WebUtility.UrlEncode(ex2.ToString()));
                return XDocument.Parse(urlContents);
            }
        }

        /// <summary>
        /// Async Get Recordings Information
        /// </summary>
        /// <param name="u">the uri of the device</param>
        /// <param name="fileName">Full path to recorded stream url encoded (as returned by QueryRecordsList under LocalTarget)</param>
        /// <returns>returns an XDocument object representing success OR failure</returns>
        /// <remarks>error codes "30014 - server error" and "31014 - xml parse error" and "30015 - input error"</remarks>
        public async Task<XDocument> GetRecordInfoAsync(Uri u, string fileName)
        {
            string urlContents = string.Empty;

            if (string.IsNullOrEmpty(fileName))
            {
                urlContents = string.Format(this.FailedResponseXml, "30015", WebUtility.UrlEncode("null or empty file Name id"));
            }
            else
            {
                try
                {
                    using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromMinutes(2) })
                    {
                        string requestString = @u.ToString() + CommonRequestStringPortion + Enum_MethodNames_RecordingsInterface.GetRecordInfo.ToString() + "FileName=" + fileName;
                        Task<string> getStringTask = client.GetStringAsync(requestString);
                        urlContents = await getStringTask;
                    }
                }
                catch (Exception ex1)
                {
                    urlContents = string.Format(this.FailedResponseXml, "30014", WebUtility.UrlEncode(ex1.ToString()));
                }
            }

            try
            {
                return XDocument.Parse(urlContents);
            }
            catch (Exception ex2)
            {
                urlContents = string.Format(this.FailedResponseXml, "31014", WebUtility.UrlEncode(ex2.ToString()));
                return XDocument.Parse(urlContents);
            }
        }

        /// <summary>
        /// Async Cancel Recording
        /// </summary>
        /// <param name="u">the uri of the device</param>
        /// <param name="recordID">The Record ID (as return by QueryRecordsList)</param>
        /// <returns>returns an XDocument object representing success OR failure</returns>
        /// <remarks>error codes "30016 - server error" and "31016 - xml parse error"  and "30017 - input error"</remarks>
        public async Task<XDocument> CancelRecordingAsync(Uri u, string recordID)
        {
            string urlContents = string.Empty;

            if (string.IsNullOrEmpty(recordID))
            {
                urlContents = string.Format(this.FailedResponseXml, "30017", WebUtility.UrlEncode("null or empty record id"));
            }
            else
            {
                try
                {
                    using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromMinutes(2) })
                    {
                        string requestString = @u.ToString() + CommonRequestStringPortion + Enum_MethodNames_RecordingsInterface.CancelRecord.ToString() + "RecordID=" + recordID;
                        Task<string> getStringTask = client.GetStringAsync(requestString);
                        urlContents = await getStringTask;
                    }
                }
                catch (Exception ex1)
                {
                    urlContents = string.Format(this.FailedResponseXml, "30016", WebUtility.UrlEncode(ex1.ToString()));
                }
            }

            try
            {
                return XDocument.Parse(urlContents);
            }
            catch (Exception ex2)
            {
                urlContents = string.Format(this.FailedResponseXml, "31016", WebUtility.UrlEncode(ex2.ToString()));
                return XDocument.Parse(urlContents);
            }
        }

        /// <summary>
        /// Async Delete Recording
        /// </summary>
        /// <param name="u">the uri of the device</param>
        /// <param name="recordID">The Record ID (as return by QueryRecordsList)</param>
        /// <returns>returns an XDocument object representing success OR failure</returns>
        /// <remarks>error codes "30018 - server error" and "31018 - xml parse error"  and "30019 - input error"</remarks>
        public async Task<XDocument> DeleteRecordingAsync(Uri u, string recordID)
        {
            string urlContents = string.Empty;

            if (string.IsNullOrEmpty(recordID))
            {
                urlContents = string.Format(this.FailedResponseXml, "30019", WebUtility.UrlEncode("null or empty record id"));
            }
            else
            {
                try
                {
                    using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromMinutes(2) })
                    {
                        string requestString = @u.ToString() + CommonRequestStringPortion + Enum_MethodNames_RecordingsInterface.DeleteRecord.ToString() + "RecordID=" + recordID;
                        Task<string> getStringTask = client.GetStringAsync(requestString);
                        urlContents = await getStringTask;
                    }
                }
                catch (Exception ex1)
                {
                    urlContents = string.Format(this.FailedResponseXml, "30018", WebUtility.UrlEncode(ex1.ToString()));
                }
            }

            try
            {
                return XDocument.Parse(urlContents);
            }
            catch (Exception ex2)
            {
                urlContents = string.Format(this.FailedResponseXml, "31018", WebUtility.UrlEncode(ex2.ToString()));
                return XDocument.Parse(urlContents);
            }
        }
    }
}
