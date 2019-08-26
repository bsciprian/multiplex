using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiplexServices.Models.Runs
{
    public class RunIndexModel
    {
        public IEnumerable<RunIndexListingModel> Runs { get; set; }
    }
}
