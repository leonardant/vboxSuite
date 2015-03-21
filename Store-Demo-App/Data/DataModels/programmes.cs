using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace mpeg2_player.Data.DataModels
{
    public class programmes
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int programmeId { get; set; }
        public string programmeStart { get; set; }
        public string programmeStop { get; set; }
        public string programmeChannel { get; set; }
        public string programmeTitle { get; set; }
        public string programmeDescription { get; set; }
        public string programmeEpisodeNumberSystem { get; set; }
        public string programmeEpisodeNumberProgId { get; set; }
    }
}
