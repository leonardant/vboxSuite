using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace mpeg2_player.Data.DataModels
{
    public class channel
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int channelId { get; set; }
        public string channelUniqueId { get; set; }
        public string channelIcon { get; set; }
        public string channelName { get; set; }
        public string channelType { get; set; }
        public string channelCode { get; set; }
        public string channelPPV { get; set; }
        public string channelSource { get; set; }
        public string channelUrl { get; set; }
    }
}
