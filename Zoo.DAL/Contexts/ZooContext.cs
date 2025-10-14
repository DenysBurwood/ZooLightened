using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoo.DAL.Database.Configs;
using Zoo.DL.Entities;
using Zoo.DL.Entities.Humans;

namespace Zoo.DAL.Contexts
{
    public class ZooContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        //public DbSet<Employee> Employees { get; set; }

        public ZooContext(DbContextOptions<ZooContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ZooContext).Assembly);
        }
    }
}
