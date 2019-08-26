using Microsoft.EntityFrameworkCore;
using MultiplexData;
using MultiplexData.Models;
using MultiplexServices.Models.Runs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiplexServices
{
    public class RunService : ServiceBase<RunDetailModel, Run>
    {
        public RunService(MultiplexDbContext context) : base(context)
        {

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
                MoviePoster = run.Movie.Poster,
                MovieDuration = run.Movie.Duration,
                MovieType = run.Movie.Type,
                MovieDescription = run.Movie.Description,
                DateTime = run.Date,
                Rows = run.Room.SeatsNumber.Split(',').Select(Int32.Parse).ToList(),
                Seats = run.SeatsRun.Select(x => new SeatRunDetailModel(x.RunId, x.SeatRoom.Id, x.SeatRoom.SeatName, x.IsBooked)).ToList()
            };

            return model;
        }

        public IEnumerable<Run> GetAll()
        {
            return DbContext.Runs.
                Include(run => run.Movie).
                Include(run => run.Room);
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
            DbContext.Add(run);
            DbContext.SaveChanges();
            var seatsRoom = DbContext.SeatRoom.Where(m => m.RoomId == room.Id).ToList();
            foreach (var seatRoom in seatsRoom)
            {
                DbContext.Add(new SeatRun {SeatRoomId = seatRoom.Id, RunId = run.Id, IsBooked = false });
            }
            DbContext.SaveChanges();
            return true;
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

    }
}
