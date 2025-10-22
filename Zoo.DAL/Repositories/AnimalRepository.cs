using Microsoft.EntityFrameworkCore;
using Zoo.DAL.Contexts;
using Zoo.DL.Entities;
using Zoo.DL.Enum;

namespace Zoo.DAL.Repositories
{
    public class AnimalRepository:BaseRepository<Animal>
    {
        private readonly DbSet<Animal> _animals;
        private readonly DbSet<AnimalSpecies> _animalSpecies;       //  Adjoindre les espèces animales pour pouvoir récupérer le nom dans la liste d'animaux
        private readonly DbSet<AnimalMovement> _animalMovements;
        private readonly ZooContext _context;

        public AnimalRepository(ZooContext context):base(context)
        {
            _context=context;
            _animals=context.Animals;
            _animalSpecies=context.AnimalSpecies;
            _animalMovements = context.AnimalMovements;
        }

        public List<Animal> GetAllAnimals() 
        {
            List<Animal> animals = _animals.ToList();
            animals.ForEach(animal => { animal.Species=_animalSpecies.FirstOrDefault(x => x.Id==animal.SpeciesId)!; });
            return animals;
        }

        public AnimalSpecies? GetSpeciesById(int speciesId) 
        {
            return _animalSpecies.FirstOrDefault(x => x.Id==speciesId);
        }

        public List<AnimalSpecies> GetSpecies()
        {
            List<AnimalSpecies> species = _animalSpecies.ToList();
            species.GroupBy(sp => sp.Name);
            return species;
        }

        public int NumberAnimalSpecies(string speciesName) 
        {
            return _animals.Count(a => a.Species.Name==speciesName);
        }

        public bool IsAnimalAvailableForMovement(Animal animal, DateTime startdate, DateTime? enddate)
        {
                var requestedEndDate = enddate ?? DateTime.MaxValue;

                bool isOccupied =_animalMovements
                    .Any(m => m.AnimalId == animal.Id &&
                              (
                                  // Cas 1 : location en cours sans fin et commence avant ou pendant la période demandée
                                  (m.EndDate == null && m.StartDate <= requestedEndDate)

                                  // Cas 2 : chevauchement classique de deux périodes
                                  || (m.EndDate != null &&
                                      m.StartDate <= requestedEndDate &&
                                      m.EndDate >= startdate)
                              ));
                return !isOccupied;
        }

        public void InsertAnimalMovement(Animal animal, DateTime startdate, DateTime? enddate, Direction direction)
        {

            AnimalMovement movement = new()
            {
                Direction = direction,
                Animal = animal,
                StartDate = startdate,
                EndDate = enddate,
            };
            _animalMovements.Add(movement);
            _context.SaveChanges();
        }
    }
}
