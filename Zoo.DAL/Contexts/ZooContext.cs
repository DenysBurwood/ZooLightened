using Microsoft.EntityFrameworkCore;
using Zoo.DL.Entities;
using Zoo.DL.Entities.Humans;

namespace Zoo.DAL.Contexts
{
    public class ZooContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<AnimalSpecies> AnimalSpecies { get; set; }

        public DbSet<AnimalMovement> AnimalMovements { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Owner> Owners { get; set; }

        public DbSet<Toy> Toys { get; set; }

        public DbSet<ToyDonation> ToyDonations { get; set; }

        public ZooContext(DbContextOptions<ZooContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ZooContext).Assembly);
        }
    }
}
