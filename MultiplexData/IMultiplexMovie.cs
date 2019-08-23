using MultiplexData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiplexData
{
    public interface IMultiplexMovie
    {
        IEnumerable<Movie> GetUpcomingMovies();
        Movie GetById(int id);
        void Add(Movie movie);
    }
}
