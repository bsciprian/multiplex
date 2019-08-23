using Microsoft.EntityFrameworkCore;
using MultiplexData;
using MultiplexData.Models;
using System.Collections.Generic;
using System.Linq;

namespace MultiplexServices
{
    public class RunService : IMultiplexRun
    {
        private MultiplexDbContext _context;

        public RunService(MultiplexDbContext context)
        {
            _context = context;
        }

        public void Add(Run run)
        {
            _context.Add(run);
            _context.SaveChanges();
        }

        public IEnumerable<Run> GetAll()
        {
            return _context.Runs.
                Include(run => run.Movie).
                Include(run => run.Room);
        }

        public Run GetById(int id)
        {
            return _context.Runs.
                Include(run => run.Movie).
                Include(run => run.Room).
                Include(run => run.SeatsRun).
                ThenInclude(seatRun => seatRun.SeatRoom).
                FirstOrDefault(m => m.Id == id);
        }

    }
}
