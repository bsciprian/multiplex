using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MultiplexData;
using MultiplexData.Models;
using MultiplexServices.Models.Runs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MultiplexServices
{
    public class RunService : ServiceBase<RunDetailModel, Run>
    {
        private readonly IConfiguration _configuration;
        public RunService(MultiplexDbContext context, IConfiguration configuration) : base(context)
        {
            _configuration = configuration;
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

        public void Add(Run run)
        {
            DbContext.Add(run);
            DbContext.SaveChanges();
        }

        public override Run FromModel(RunDetailModel model)
        {
            return new Run {
                Id = model.Id,
                Date = model.DateTime
            };
        }

        public SeatRun FromModel(SeatRunDetailModel model)
        {
            return new SeatRun { RunId = model.RunId, SeatRoomId = model.SeatRoomID, IsBooked = model.IsBooked };
        }

        public RunDetailModel ToModel(Run run)
        {
            var model = new RunDetailModel
            {
                Id = run.Id,
                MovieName = run.Movie.Title,
                MoviePoster = this.GetImageBase64(Path.Combine(_configuration["ImagesFolder"], run.Movie.Id.ToString()) + run.Movie.Poster),
                MovieDuration = run.Movie.Duration,
                MovieType = run.Movie.Type,
                MovieDescription = run.Movie.Description,
                DateTime = run.Date,
                RoomName = run.Room.RoomName,
                Rows = run.Room.SeatsNumber.Split(',').Select(Int32.Parse).ToList(),
                Seats = run.SeatsRun.Select(x => new SeatRunDetailModel(x.RunId, x.SeatRoom.Id, x.SeatRoom.SeatName, x.IsBooked)).ToList()
            };
                return model;
        }

        public RunIndexModel GetAll()
        {
            var listingResult = DbContext.Runs.
                Include(run => run.Movie).
                Include(run => run.Room).Select(result => new RunIndexListingModel
            {
                Id = result.Id,
                DateTime = result.Date,
                MovieId = result.Movie.Id,
                RoomName = result.Room.RoomName
            });

            var runIndexModel = new RunIndexModel()
            {
                Runs = listingResult
            };

            return runIndexModel;
        }

        public RunDetailModel GetById(int id)
        {
            return ToModel(DbContext.Runs.
                Include(run => run.Movie).
                Include(run => run.Room).
                Include(run => run.SeatsRun).
                ThenInclude(seatRun => seatRun.SeatRoom).
                FirstOrDefault(m => m.Id == id));
        }

        public bool AddRunModel(AddRunModel model)
        {
            var room = DbContext.Rooms.Where(m => m.RoomName == model.RoomName).FirstOrDefault();
            var run = new Run
            {
                Date = model.DateTime,
                MovieId = DbContext.Movies.Where(m => m.Title == model.MovieName).FirstOrDefault().Id,
                RoomId = room.Id
            };

            var runs = DbContext.Runs
                .Include(r => r.Movie)
                .Include(r => r.Room)
                .Where(r => r.Date.Date == model.DateTime.Date && r.Room.RoomName == model.RoomName);
            bool isPossible = true;
            foreach (var runr in runs)
            {
                if (model.DateTime >= runr.Date && model.DateTime <= (runr.Date + runr.Movie.Duration))
                {
                    isPossible = false;
                }
            }
            if (isPossible)
            {
                DbContext.Add(run);
                DbContext.SaveChanges();
                var seatsRoom = DbContext.SeatRoom.Where(m => m.RoomId == room.Id).ToList();
                foreach (var seatRoom in seatsRoom)
                {
                    DbContext.Add(new SeatRun { SeatRoomId = seatRoom.Id, RunId = run.Id, IsBooked = false });
                }
                DbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public void Update(SeatRunDetailModel model)
        {
            var entity = FromModel(model);

            DbContext.Update(entity);
            DbContext.SaveChanges();
        }

        public AddRunModel GetAddRunModel()
        {
            return new AddRunModel
            {
                MoviesTitle = DbContext.Movies.Select(m => m.Title),
                RoomNames = DbContext.Rooms.Select(r => r.RoomName)
            };
        }

        public RunDetailModel GetRunDetailModel(SeatRunDetailModel seatRunDetailModel)
        {
            var runDetailModel = GetById(seatRunDetailModel.RunId);
            runDetailModel.Seats = null;

            return runDetailModel;
        }

    }
}
