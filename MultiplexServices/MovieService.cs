using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MultiplexData;
using MultiplexData.Models;
using MultiplexServices.Interfaces;
using MultiplexServices.Models.Movies;
using MultiplexServices.Models.Runs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MultiplexServices
{
    public class MovieService : ServiceBase<MovieIndexListingModel, Movie>
    {
        private readonly IConfiguration _configuration;

        public MovieService(MultiplexDbContext context, IConfiguration configuration) :base(context)
        {
            _configuration = configuration;
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
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                Duration = model.Duration,
                Type = model.Type,
                Year = model.Year, 
                Poster = model.Poster
            };
        }

        private string GetImageBase64(string filePath)
        {
            if (System.IO.File.Exists(filePath))
            {
                byte[] bytes = System.IO.File.ReadAllBytes(filePath);
                string file = Convert.ToBase64String(bytes);
                return file;
            }
            return string.Empty;
        }

        public MovieDetailModel ToModel(Movie movie)
        {
            return new MovieDetailModel
            {
                Id = movie.Id,
                Year = movie.Year,
                Title = movie.Title,
                Poster = this.GetImageBase64(Path.Combine(_configuration["ImagesFolder"], movie.Id.ToString()) + movie.Poster),
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
        }

        public void Add(MovieDetailModel movieDetailModel)
        {
            var movie = FromModel(movieDetailModel);
            DbContext.Add(movie);
            DbContext.SaveChanges();
            movieDetailModel.Id = movie.Id;
        }

        public MovieIndexModel GetUpcomingMovies()
        {

            var listingResults = DbContext.Movies.
                Include(Movie => Movie.MovieCategories).
                Include(movie => movie.Runs).
                ThenInclude(run => run.Room).
                Include(movie => movie.Runs).
                ThenInclude(run => run.Movie).
                Where(m => m.Runs.Any(r => r.Date > DateTime.Now))
                .Select(result => new MovieIndexListingModel
                {
                    Id = result.Id,
                    Title = result.Title,
                    Year = result.Year,
                    Type = result.Type,
                    Duration = result.Duration,
                    Poster = this.GetImageBase64(Path.Combine(_configuration["ImagesFolder"], result.Id.ToString()) + result.Poster),
                    Description = result.Description,
                    Runs = result.Runs.Select(x => new RunIndexListingModel()
                    {
                        Id = x.Id,
                        DateTime = x.Date,
                        MovieId = x.Movie.Id,
                        RoomName = x.Room.RoomName
                    })
                    }).ToList();

           
            return new MovieIndexModel {Movies = listingResults} ;
        }

        public MovieDetailModel GetById(int id)
        {
            var movieDetailModel = ToModel(DbContext.Movies.
                Include(Movie => Movie.MovieCategories).
                Include(movie => movie.Runs).
                ThenInclude(run => run.Room)
                .FirstOrDefault(m => m.Id == id));
            return movieDetailModel;
        }

        public void Update(MovieDetailModel movieDetailModel)
        {
            var movie = FromModel(movieDetailModel);
            DbContext.Update(movie);
            DbContext.SaveChanges();
        }
    }
}
