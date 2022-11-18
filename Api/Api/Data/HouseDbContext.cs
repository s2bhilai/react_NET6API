using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Data
{
    public class HouseDbContext: DbContext
    {
        public HouseDbContext(DbContextOptions<HouseDbContext> o)
            : base(o)
        {
                
        }

        public DbSet<HouseEntity> Houses => Set<HouseEntity>();
        public DbSet<BidEntity> Bids => Set<BidEntity>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);

            optionsBuilder
                .UseSqlite($"Data Source={Path.Join(path, "houses.db")}");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedData.Seed(modelBuilder);
        }
    }
}
