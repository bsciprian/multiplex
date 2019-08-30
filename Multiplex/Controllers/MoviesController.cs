using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MultiplexData;
using MultiplexServices;
using MultiplexServices.Models.Movies;
using MultiplexServices.Models.Runs;
using System;
using System.IO;
using System.Linq;
using System.Drawing;

namespace Multiplex.Controllers
{
    [LogAction]
    public class MoviesController : BaseController<MovieIndexListingModel, MovieService>
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IConfiguration _configuration;

        public MoviesController(MovieService movieService, IHostingEnvironment hostingEnvironment, IConfiguration configuration) : base(movieService)
        {
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
        }


        public IActionResult Index()
        {
            var model = Service.GetUpcomingMovies();

            return View(model);

        }

        public IActionResult Detail(int id)
        {
            var model = Service.GetById(id);
            return View(model);
        }

        public ActionResult AddMovie()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddMovie(MovieDetailModel movieDetailModel, IFormFile file)
        {
            log.Info("Adding a movie started....");
            try
            {
                movieDetailModel.Poster = Path.GetExtension(file.FileName);
                Service.Add(movieDetailModel);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            if (file != null)
            {
                string ImageName = movieDetailModel.Id + Path.GetExtension(file.FileName);
                string SavePath = Path.Combine(_configuration["ImagesFolder"], ImageName);
                using (var stream = new FileStream(SavePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }
            return RedirectToAction("Index");
        }
    }
}
