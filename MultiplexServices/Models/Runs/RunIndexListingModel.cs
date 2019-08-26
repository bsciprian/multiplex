using MultiplexData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiplexServices.Models.Runs
{
    public class RunIndexListingModel
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int MovieId { get; set; }
        public string RoomName { get; set; }
    }
}
