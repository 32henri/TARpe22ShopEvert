using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TARpe22ShopEvert.Core.Domain;

namespace TARpe22ShopEvert.Data
{
    public class TARpe22ShopEvertContext : DbContext
    {
        public TARpe22ShopEvertContext(DbContextOptions<TARpe22ShopEvertContext> options) : base(options) { }

        public DbSet<Spaceship> Spaceships { get; set; }
        public DbSet<FileToDatabase> FilesToDatabase { get; set; }
        public DbSet<RealEstate> RealEstates { get; set; }
        public DbSet<FileToApi> FilesToApi { get; set; }
    }
}
