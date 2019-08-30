using Microsoft.AspNetCore.Mvc;
using MultiplexServices;
using MultiplexServices.Models.Rooms;

namespace Multiplex.Controllers
{
    public class RoomsController : BaseController<RoomModel, RoomService>
    { 
        public RoomsController(RoomService roomService):base(roomService)
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

        public ActionResult AddRoom()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddRoom(RoomModel roomModel)
        {
            Service.Add(roomModel);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Update(RoomModel roomModel)
        {
            Service.Update(roomModel);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(RoomModel roomModel)
        {
            Service.Delete(roomModel);
            return RedirectToAction("Index");
        }

    }
}