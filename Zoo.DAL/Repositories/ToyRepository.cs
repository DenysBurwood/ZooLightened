using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoo.DAL.Contexts;
using Zoo.DL.Entities;

namespace Zoo.DAL.Repositories
{
    public class ToyRepository : BaseRepository<Toy>
    {
        private readonly DbSet<Toy> _toys;
        private readonly DbSet<AnimalSpecies> _animalSpecies;
        private readonly ZooContext _context;

        public ToyRepository(ZooContext context) : base(context)
        {
            _context = context;
            _toys = context.Toys;
            _animalSpecies = context.AnimalSpecies;
        }

        public string GetSpeciesNameById(int id)
        {
            return _animalSpecies.FirstOrDefault(x => x.Id == id)!.Name;
        }

    }
}
