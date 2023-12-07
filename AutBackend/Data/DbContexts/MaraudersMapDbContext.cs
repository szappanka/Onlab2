using Data.DbContexts;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DbContexts
{
    public class MaraudersMapDbContext : DbContext
    {
        public MaraudersMapDbContext(DbContextOptions<MaraudersMapDbContext> options) : base(options)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        //}


        public DbSet<MarauderUser> MarauderUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MarauderUser>().HasData(new MarauderUser
            {
                Id = 1,
                Name = "Harry Potter",
                LastUpdate = 0,
                IsActivated = false,
                Coordinates = "0 0 0"
            });

            modelBuilder.Entity<MarauderUser>().HasData(new MarauderUser
            {
                Id = 2,
                Name = "Ron Weasley",
                LastUpdate = 0,
                IsActivated = false,
                Coordinates = "1 0 0"
            });
        }
    }
}
