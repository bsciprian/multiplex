using System;
using System.Collections.Generic;
using System.Text;

namespace MultiplexServices.Models.Runs
{
    public class AddRunModel
    {
        public IEnumerable<string> MoviesTitle { get; set; }
        public IEnumerable<string> RoomNames { get; set; }
        public string MovieName { get; set; }
        public string RoomName { get; set; }
        public DateTime DateTime { get; set; }
    }
}
