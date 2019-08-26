using Microsoft.AspNetCore.Mvc;
using MultiplexServices.Interfaces;

namespace Multiplex.Controllers
{
    public abstract class BaseController<TModel, TService> : Controller
        where TService:IServiceBase
        where TModel:class
    {
        protected TService Service;

        protected BaseController(TService service)
        {
            Service = service;
        }

        //    [HttpPost]
        //    public IActionResult AddEntity(TModel model)
        //    {         
        //        Service.Add(model);
        //        return RedirectToAction("Index");
        //    }
    }
}
