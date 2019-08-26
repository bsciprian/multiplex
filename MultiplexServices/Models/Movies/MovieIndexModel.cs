using System.Collections.Generic;

namespace MultiplexServices.Models.Movies
{
    public class MovieIndexModel
    {
        public IEnumerable<MovieIndexListingModel> Movies { get; set; }
    }
}
