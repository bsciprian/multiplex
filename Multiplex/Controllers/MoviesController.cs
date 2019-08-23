using Microsoft.AspNetCore.Mvc;
using Multiplex.Models.Movies;
using Multiplex.Models.Runs;
using MultiplexData;
using System;
using System.Linq;

namespace Multiplex.Controllers
{
    public class MoviesController : Controller
    {
        private IMultiplexMovie _movies;
        public MoviesController(IMultiplexMovie movies)
        {
            _movies = movies;
        }

        public IActionResult Index()
        {
            var movieModels = _movies.GetUpcomingMovies();

            var listingResult = movieModels.Select(result => new MovieIndexListingModel
            {
                Id = result.Id,
                Title = result.Title,
                Year = result.Year,
                Type = result.Type,
                Duration = result.Duration,
                Poster = result.Poster,
                Description = result.Description,
                Runs = result.Runs.Select(x => new RunIndexListingModel()
                {
                    Id = x.Id,
                    DateTime = x.Date,
                    MovieId = x.Movie.Id,
                    RoomName = x.Room.RoomName
                }).ToList()
            });

            var model = new MovieIndexModel()
            {
                Movies = listingResult
            };

            return View(model);
            
        }

        public IActionResult Detail(int id)
        {
            var movie = _movies.GetById(id);

            var model = new MovieDetailModel
            {
                Id = movie.Id,
                Title = movie.Title,
                Poster = movie.Poster,
                Duration = movie.Duration,
                Type = movie.Type,
                Description = movie.Description,
                Runs = movie.Runs.Where(r=>r.Date > DateTime.Now).Select(x => new RunIndexListingModel()
                {
                    Id = x.Id,
                    DateTime = x.Date,
                    MovieId = x.Movie.Id,
                    RoomName = x.Room.RoomName
                }).ToList()
            };

            return View(model);
        }

    }
}
