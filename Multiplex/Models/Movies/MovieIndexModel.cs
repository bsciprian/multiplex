using System.Collections.Generic;

namespace Multiplex.Models.Movies
{
    public class MovieIndexModel
    {
        public IEnumerable<MovieIndexListingModel> Movies { get; set; }
    }
}
