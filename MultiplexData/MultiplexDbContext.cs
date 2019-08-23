using Microsoft.EntityFrameworkCore;
using MultiplexData.Models;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace MultiplexData
{
    public class MultiplexDbContext : IdentityDbContext<IdentityUser>
    {
        public string ConnectionString
        { get; private set; }
        public MultiplexDbContext()
        { }
        public MultiplexDbContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder();
                builder.SetBasePath(Directory.GetCurrentDirectory());
                builder.AddJsonFile("appsettings.json");
                IConfiguration Configuration = builder.Build();

                optionsBuilder.UseSqlServer(
                    Configuration.GetConnectionString("MultiplexConnection"));
                base.OnConfiguring(optionsBuilder);
            }
        }

        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Run> Runs { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<SeatRun> SeatRun { get; set; }
        public virtual DbSet<MovieCategory> MovieCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<MovieCategory>().HasKey(mc => new { mc.MovieId, mc.CategoryId });
            modelBuilder.Entity<SeatRun>().HasKey(s => new { s.SeatRoomId, s.RunId });

            base.OnModelCreating(modelBuilder);

        }


    }
}
