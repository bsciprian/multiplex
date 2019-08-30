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
            var model = Service.GetAll();

            return View(model);
        }

        public IActionResult Detail(int id)
        {
            var model = Service.GetById(id);

            return View(model);
        }

        public IActionResult ErrorModel()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Update(RunDetailModel runDetailModel)
        {
            Service.Update(runDetailModel);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult SaveRun(string selectedSeats)
        {
            var result = JsonConvert.DeserializeObject<SeatRunDetailViewModel>(selectedSeats);
            foreach (var seatRun in result.SeatRuns)
            {
                Service.Update(seatRun);
            }

            var runDetailModel = Service.GetRunDetailModel(result.SeatRuns.FirstOrDefault());
            runDetailModel.MoviePoster = null;
            runDetailModel.BookedSeats = new List<string>();
            foreach (var seatRun in result.SeatRuns)
            {
                runDetailModel.BookedSeats.Add(seatRun.SeatName);
            }
            return Json(Url.Action("RunTickets", "Runs", runDetailModel));       
        }

        public IActionResult RunTickets(RunDetailModel runDetailModel)
        {
            return View(runDetailModel);
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
                return RedirectToAction("Index", "Movies");
            }
            else
            {
                return RedirectToAction("ErrorModel","Runs");
            }
        }
    }
}
