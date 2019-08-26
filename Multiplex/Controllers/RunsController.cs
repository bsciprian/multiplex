using Microsoft.AspNetCore.Mvc;
using MultiplexServices;
using MultiplexServices.Models.Runs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Multiplex.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class RunsController : BaseController<RunDetailModel, RunService>
    {
        public RunsController(RunService runService) : base(runService)
        {

        }

        public IActionResult Index()
       {
            var runsModels = Service.GetAll();

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
            var model = Service.GetById(id);

            return View(model);
        }

        [HttpPost]
        public IActionResult Update(RunDetailModel runDetailModel)
        {
            Service.Update(runDetailModel);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult SaveRun(string selectedSeats)
        {
            var result = JsonConvert.DeserializeObject<SeatRunDetailViewModel>(selectedSeats);
            foreach (var seatRun in result.SeatRuns)
            {
                Service.Update(seatRun);
            }

            return RedirectToAction("Runs/Index");
        }

        public IActionResult AddRun()
        {
            var addRunModels = Service.GetAddRunModel();
            return View(addRunModels);
        }

        [HttpPost]
        public IActionResult AddRun(AddRunModel model)
        {
            bool addRunModels = Service.AddRunModel(model);
            if (addRunModels)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("ErrorModel");
            }
        }
    }
}
