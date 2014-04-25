//-----------------------------------------------------------------------
// <copyright file="PortableInterfaceLibraryCommon.cs" company="Anthony Leonard">
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
// <date>26/03/2014 22:50:39</date>
//-----------------------------------------------------------------------

namespace Vbox_Home_CommonLibrary
{
    using System;
    using System.IO;
    using System.Net;
    using System.Xml.Linq;

    /// <summary>
    /// Common Interface Library from which all other classes inherit
    /// </summary>
    public class PortableInterfaceLibraryCommon
    {
        /// <summary>
        /// the public enumeration for TimeFormat
        /// </summary>
        public enum Enum_TimeFormat
        {
            /// <summary>
            /// The date and time
            /// </summary>
            /// <example>gets output similar to : Thu Feb 16, 2012 - 08:44:38 AM</example>
            DateAndTime,

            /// <summary>
            /// The time
            /// </summary>
            /// <example>gets output similar to : 08:44:38 AM</example>
            Time,

            /// <summary>
            /// The date
            /// </summary>
            /// <example>gets output similar to : Thu Feb 16, 2012</example>
            Date,

            /// <summary>
            /// The XMLTV
            /// </summary>
            /// <example>gets output similar to : 20120216064400 +0200</example>
            XMLTV
        }

        /// <summary>
        /// the public enumeration for common query type to run
        /// </summary>
        public enum Enum_MethodName_GeneralInterface
        {
            /// <summary>
            /// The query system time
            /// </summary>
            QuerySystemTime,

            /// <summary>
            /// The query board information
            /// </summary>
            QueryBoardInfo,

            /// <summary>
            /// The query external media status
            /// </summary>
            QueryExternalMediaStatus
        }

        /// <summary>
        /// the public enumeration for XMLTV query type to run
        /// </summary>
        public enum Enum_MethodName_XmltvInterface
        {
            /// <summary>
            /// The query system time
            /// </summary>
            QueryXmltvNumOfChannels,

            /// <summary>
            /// The query board information
            /// </summary>
            GetXmltvEntireFile,

            /// <summary>
            /// The query external media status
            /// </summary>
            GetXmltvSection,

            /// <summary>
            /// The query Get XMLTV Channels List
            /// </summary>
            GetXmltvChannelsList,

            /// <summary>
            /// The query Get XMLTV Programs List
            /// </summary>
            GetXmltvProgramsList,

            /// <summary>
            /// The query Get XMLTV Language Tracks
            /// </summary>
            GetXmltvLanguageTracks,

            /// <summary>
            /// The query Scan EPG 
            /// </summary>
            ScanEPG,

            /// <summary>
            /// The query Query DataBase version 
            /// </summary>
            QueryDataBaseversion
        }

        /// <summary>
        /// the public enumeration for XMLTV language track types
        /// </summary>
        public enum Enum_MethodName_XmltvInterfaceLanguageTrackTypes
        {
            /// <summary>
            /// The lang track type ALL
            /// </summary>
            ALL,

            /// <summary>
            /// The lang track type AUDIO
            /// </summary>
            AUDIO,

            /// <summary>
            /// The lang track type SUBTITLE
            /// </summary>
            SUBTITLE
        }

        /// <summary>
        /// Gets common Request String Portion is the portion of the URI that is constant in every call
        /// </summary>
        public string CommonRequestStringPortion
        {
            get { return "cgi-bin/HttpControl/HttpControlApp?OPTION=1&Method="; }
        }

        /// <summary>
        /// Gets the xml string into which to embed the other possible errors  
        /// </summary>
        public string FailedResponseXml
        {
            get
            {
                return @"<?xml version=""1.0"" encoding=""utf-8""?>
                            <VboxHttpControl>
                                    <Status>   
                                        <ErrorCode>{0}</ErrorCode>
                                        <ErrorDescription>{1}</ErrorDescription>
                                    </Status>
                                    <Reply> 
                                    </Reply>
                            </VboxHttpControl>";
            }
        }
    }
}
