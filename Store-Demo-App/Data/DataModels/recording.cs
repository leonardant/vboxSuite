using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Store_Demo_App.Data.DataModels
{
    public class recording
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int recordingId { get; set; }
        public string recordingChannel { get; set; }
        public string recordingStart { get; set; }
        public string recordingStop { get; set; }
        public string recordingChannelName { get; set; }
        public string recordingTitle { get; set; }
        public string recordingDescription { get; set; }
        public string recordingState { get; set; }
        public string recordingRecordId { get; set; }
        public string recordingUrl { get; set; }
        public string recordingLocalTarget { get; set; }
    }
}
