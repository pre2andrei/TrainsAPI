using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Xml;
using TrainsAPI.Models;

namespace TrainsAPI.Data
{
    public class TrainsDataContext : IdentityDbContext<IdentityUser>
    {

        public TrainsDataContext(DbContextOptions<TrainsDataContext> options) : base(options)
        {

        }

        public DbSet<Train> Trains { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Road> Roads { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarType> CarTypes { get; set; }
        public DbSet<Engine> Engines { get; set; }
        public DbSet<EngineType> EngineTypes { get; set; }
        public DbSet<Journey> Journeys { get; set; }
        public DbSet<JourneyRoad> JourneyRoads { get; set; }

        protected override void OnModelCreating(ModelBuilder model)
        {
            base.OnModelCreating(model);
            model.Entity<Journey>().HasOne(t => t.Train).WithMany(t => t.Journeys).HasForeignKey(t => t.TrainId);

            model.Entity<JourneyRoad>().HasOne(jr => jr.Journey).WithMany(j => j.JourneyPaths).HasForeignKey(jr => jr.JourneyId);
            model.Entity<JourneyRoad>().HasOne(jr => jr.Road).WithMany(p => p.JourneyPaths).HasForeignKey(jr => jr.RoadId);

            model.Entity<Road>().HasOne(r => r.From).WithMany().HasForeignKey(s => s.FromId).OnDelete(DeleteBehavior.ClientCascade);
            model.Entity<Road>().HasOne(r => r.To).WithMany().HasForeignKey(s => s.ToId).OnDelete(DeleteBehavior.ClientCascade);
            model.Entity<Road>().HasAlternateKey(r => new { r.ToId, r.FromId });
            model.Entity<Road>().ToTable(r => r.HasCheckConstraint("CK_From_!=_To", "[FromId] <> [ToId]"));

            model.Entity<Train>().HasOne(t => t.Engine).WithOne(e => e.Train).HasForeignKey<Train>(t => t.EngineId);

            model.Entity<Engine>().HasOne(e => e.EngineType).WithMany(et => et.Engines).HasForeignKey(e => e.EngineTypeId);

            model.Entity<Car>().HasOne(c => c.CarType).WithMany(ct => ct.Cars).HasForeignKey(e => e.CarTypeId);
            model.Entity<Car>().HasOne(c => c.Train).WithMany(ct => ct.Cars).HasForeignKey(e => e.TrainId);

            SeedRoles(model);
        }

        private static void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
                new IdentityRole() { Name = "User", ConcurrencyStamp = "2", NormalizedName = "User" }
                );
        }
    }
}