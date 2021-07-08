using CountryInfo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountryInfo.EF
{
    public class CountryDataContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<CountryRegion> Regions { get; set; }
        public CountryDataContext() : base("MyCBase") 
        { }
    }
}
