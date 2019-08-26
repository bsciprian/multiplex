using Microsoft.AspNetCore.Mvc;
using MultiplexData;
using MultiplexServices;
using MultiplexServices.Models.Movies;
using MultiplexServices.Models.Runs;
using System;
using System.Linq;

namespace Multiplex.Controllers
{
    public class MoviesController : BaseController<MovieIndexListingModel, MovieService>
    {
        public MoviesController(MovieService movieService) : base(movieService)
        {

        }

        public IActionResult Index()
        {
            var movieModels = Service.GetUpcomingMovies();
            //var listingResult = Service.GetUpcomingMovies();

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
            var movie = Service.GetById(id);

            var model = new MovieDetailModel
            {
                Id = movie.Id,
                Title = movie.Title,
                Poster = movie.Poster,
                Duration = movie.Duration,
                Type = movie.Type,
                Description = movie.Description,
                Runs = movie.Runs.Where(r => r.Date > DateTime.Now).Select(x => new RunIndexListingModel()
                {
                    Id = x.Id,
                    DateTime = x.Date,
                    MovieId = x.Movie.Id,
                    RoomName = x.Room.RoomName
                }).ToList()
            };

            return View(model);
        }

        public ActionResult AddMovie()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddMovie(MovieDetailModel movieDetailModel)
        {
            Service.Add(movieDetailModel);
            return RedirectToAction("Index");
        }
    }
}
