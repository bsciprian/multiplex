using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MultiplexData;
using MultiplexData.Models;
using MultiplexServices.Interfaces;
using MultiplexServices.Models.Movies;
using MultiplexServices.Models.Runs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiplexServices
{
    public class MovieService : ServiceBase<MovieIndexListingModel, Movie>
    {
       

        public MovieService(MultiplexDbContext context):base(context)
        {

        }

        public override Movie FromModel(MovieIndexListingModel model)
        {
            return new Movie { Id = model.Id, Title = model.Title, Description= model.Description,
                                Duration =model.Duration, Poster = model.Poster, Runs = FromModel(model.Runs) 
                                , Type = model.Type, Year = model.Year };
        }

        private IEnumerable<Run> FromModel(IEnumerable<RunIndexListingModel> runs)
        {
            var a = new List<Run>();
            foreach (var run in runs)
            {
                a.Add(new Run { Id = run.Id, Date = run.DateTime, MovieId = run.MovieId, RoomId = run.MovieId });
            }
            return a;
        }

        public Movie FromModel(MovieDetailModel model)
        {
            return new Movie
            {
                Title = model.Title,
                Description = model.Description,
                Duration = model.Duration,
                Type = model.Type,
                Year = model.Year
            };
        }

        public void Add(MovieDetailModel movieDetailModel)
        {
            DbContext.Add(FromModel(movieDetailModel));
            DbContext.SaveChanges();
        }

        public IEnumerable<Movie> GetUpcomingMovies()
        {
            var movies = DbContext.Movies.
                Include(Movie => Movie.MovieCategories).
                Include(movie => movie.Runs).
                ThenInclude(run => run.Room).
                Include(movie => movie.Runs).
                ThenInclude(run => run.Movie);
            return movies.Where(m => m.Runs.Any(r => r.Date > DateTime.Now));
        }

        public Movie GetById(int id)
        {
            return DbContext.Movies.
                Include(Movie => Movie.MovieCategories).
                Include(movie => movie.Runs).
                ThenInclude(run => run.Room)
                .FirstOrDefault(m => m.Id == id);
        }

    }
}
