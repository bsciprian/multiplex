using MultiplexServices.Models.Runs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiplexServices.Models.Movies
{
    public class MovieIndexListingModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Type { get; set; }
        public TimeSpan Duration { get; set; }
        public string Poster { get; set; }
        public string Description { get; set; }
        public IEnumerable<RunIndexListingModel> Runs { get; set; }
    }
}
