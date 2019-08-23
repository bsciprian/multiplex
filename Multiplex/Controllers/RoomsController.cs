using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Multiplex.Models.Rooms;

namespace Multiplex.Controllers
{
    public class RoomsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult AddRoom()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddRoom(RoomsModel room)
        {

            return RedirectToAction("AddRoom");
        }
    }
}