using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Multiplex.Models.Runs;
using Multiplex.Models.Runs.Multiplex.Models.Runs;
using MultiplexData;
using System;
using System.Linq;

namespace Multiplex.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class RunsController : Controller
    {
        private IMultiplexRun _runs;
        public RunsController(IMultiplexRun runs)
        {
            _runs = runs;
        }

        public IActionResult Index()
       {
            var runsModels = _runs.GetAll();

            var listingResult = runsModels.Select(result => new RunIndexListingModel
            {
                Id = result.Id,
                DateTime = result.Date,
                MovieId = result.Movie.Id,
                RoomName = result.Room.RoomName
            });

            var model = new RunIndexModel()
            {
                Runs = listingResult
            };

            return View(model);

        }

        public IActionResult Detail(int id)
        {
            var run = _runs.GetById(id);



            var model = new RunDetailModel
            {
                Id = run.Id,
                MovieName = run.Movie.Title,
                MoviePoster = run.Movie.Poster,
                MovieDuration = run.Movie.Duration,
                MovieType = run.Movie.Type,
                MovieDescription = run.Movie.Description,
                DateTime = run.Date,
                Rows = run.Room.SeatsNumber.Split(',').Select(Int32.Parse).ToList(),
                Seats = run.SeatsRun.Select(x => new SeatRunDetailModel(x.SeatRoom.SeatName, x.IsBooked)).ToList()
            };

            return View(model);
        }

    }
}
