using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class RealEstateAgencyDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.; Database=RealEstateAgencyDb;Truested_Connection=True");
        }
        public DbSet<City> CITIES { get; set; }
        public DbSet<District> DISTRICTS { get; set; }
        public DbSet<Neighborhood> NEIGHBORHOODS { get; set; }
        public DbSet<NumberOfRoom> NUMBEROFROOMS { get; set; }
        public DbSet<RealEstate> REALESTATES { get; set; }
        public DbSet<RealEstateClassified> REALESTATECLASSIFIEDS { get; set; }
        public DbSet<OperationClaim> OPERATIONCLAIMS { get; set; }
        public DbSet<User> USERS { get; set; }
        public DbSet<UserOperationClaim> USEROPERATIONCLAIMS { get; set; }
        public DbSet<RealEstateImage> REALESTATEIMAGES { get; set; }
    }
}
