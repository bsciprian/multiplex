using Microsoft.EntityFrameworkCore;
using MultiplexData;
using MultiplexData.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiplexServices
{
    public class MovieService : IMultiplexMovie
    {
        private MultiplexDbContext _context;

        public MovieService(MultiplexDbContext context)
        {
            _context = context;
        }

        public void Add(Movie movie)
        {
            _context.Add(movie);
            _context.SaveChanges();
        }

        public IEnumerable<Movie> GetUpcomingMovies()
        {
            var movies = _context.Movies.
                Include(Movie => Movie.MovieCategories).
                Include(movie => movie.Runs).
                ThenInclude(run => run.Room).
                Include(movie => movie.Runs).
                ThenInclude(run => run.Movie);
            return movies.Where(m => m.Runs.Any(r => r.Date > DateTime.Now));
        }

        public Movie GetById(int id)
        {
            return _context.Movies.
                Include(Movie => Movie.MovieCategories).
                Include(movie => movie.Runs).
                ThenInclude(run => run.Room)
                .FirstOrDefault(m => m.Id == id);
        }
    }
}
