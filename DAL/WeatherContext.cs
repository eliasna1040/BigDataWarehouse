using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class WeatherContext : DbContext
    {
        public DbSet<DataSet> DataSets { get; set; }
        public DbSet<Property> Properties { get; set; }

        public WeatherContext(DbContextOptions options) : base(options) { }
    }
}
