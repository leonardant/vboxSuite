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
    using System.Text;

    /// <summary>
    /// Async Portable RECORDINGS Interface Library to the device.
    /// </summary>
    public class RecordingsInterfaceLibraryAsync
    {
        /*
            ScheduleChannelRecord 
                ChannelID
                StartTime
                EndTime
            ScheduleProgramRecord
                ChannelID
                ProgramTitle
                StartTime
            StartRecord
                ChannelID
            StopRecord
                ChannelID
            GetRecordsList              
            GetRecordInfo
                FileName [see QuryRecordsList should be GetRecordsList under LocalTarget field]
            CancelRecord
                RecordID [see QuryRecordsList should be GetRecordsList under record-id field]
            DeleteRecord
                RecordID [see QuryRecordsList should be GetRecordsList under record-id field]
        */
    }
}
