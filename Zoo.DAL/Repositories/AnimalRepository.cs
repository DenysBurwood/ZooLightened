using Microsoft.EntityFrameworkCore;
using Zoo.DAL.Contexts;
using Zoo.DL.Entities;
using Zoo.DL.Enum;

namespace Zoo.DAL.Repositories
{
    public class AnimalRepository
    {
        private readonly DbSet<Animal> _animals;
        private readonly DbSet<AnimalSpecies> _animalSpecies;       //  Adjoindre les espèces animales pour pouvoir récupérer le nom dans la liste d'animaux
        private readonly DbSet<AnimalMovement> _animalMovements;
        private readonly ZooContext _context;

        public AnimalRepository(ZooContext context)
        {
            _context = context;
            _animals = context.Animals;
            _animalSpecies = context.AnimalSpecies;
            _animalMovements = context.AnimalMovements;
        }

        public List<Animal> GetAllAnimals()
        {
            List<Animal> animals = _animals.ToList();
            animals.ForEach(animal => { animal.Species = _animalSpecies.FirstOrDefault(x => x.Id == animal.SpeciesId)!; });
            return animals;
        }
        public List<Animal> GetSomeAnimals(int page, int sizePage)
        {
            List<Animal> animals = _animals.Skip(page * sizePage).Take(sizePage).ToList();
            animals.ForEach(animal => { animal.Species = _animalSpecies.FirstOrDefault(x => x.Id == animal.SpeciesId)!; });
            return animals;
        }

        public Animal? GetAnimalById(int id)
        {
            Animal? animal = _animals.FirstOrDefault(x => x.Id == id);
            if (animal is not null)
            {
                animal.Species = _animalSpecies.FirstOrDefault(x => x.Id == animal.SpeciesId)!;
            }
            return animal;
        }
        public AnimalSpecies? GetSpeciesById(int speciesId)
        {
            return _animalSpecies.FirstOrDefault(x => x.Id == speciesId);
        }

        public List<AnimalSpecies> GetSpecies()
        {
            List<AnimalSpecies> species = _animalSpecies.ToList();
            species.GroupBy(sp => sp.Name);
            return species;
        }

        public int NumberAnimalSpecies(string speciesName)
        {
            return _animals.Count(a => a.Species.Name == speciesName);
        }

        public void Add(Animal entity)
        {
            _animals.Add(entity);
            _context.SaveChanges();

        }

        //  Update not working properly. Need to further inquire inside.
        public void Update(Animal entity)
        {
            //Animal current = _animals.FirstOrDefault(x => x.Id==entity.Id)!;
            //current.Name=entity.Name;
            //current.SpeciesId=entity.SpeciesId;
            //current.Sex=entity.Sex;
            //current.Species=entity.Species;
            _context.SaveChanges();
        }

        public void Delete(Animal animal)
        {
            _animals.Remove(animal);
            _context.SaveChanges();
        }
        //public string GetSpeciesName(Animal animal) 
        //{
        //    AnimalSpecies species = new AnimalSpecies();

        //}

        public bool IsAnimalAvailableForMovement(Animal animal, DateTime startdate, DateTime? enddate)
        {
            var requestedEndDate = enddate ?? DateTime.MaxValue;

            bool isOccupied = _animalMovements
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
            AnimalMovementType type;

            if (direction == Direction.OUT)
            {
                type = startdate <= DateTime.Now ? AnimalMovementType.Opened : AnimalMovementType.Initialized;
            }
            else
            {
                type = startdate <= DateTime.Now ? AnimalMovementType.ToReceive : AnimalMovementType.Initialized;
            }

                AnimalMovement movement = new()
                {
                    Direction = direction,
                    Animal = animal,
                    StartDate = startdate,
                    EndDate = enddate,
                    Type = type,
                };

            _animalMovements.Add(movement);

            _context.SaveChanges();
        }

        public AnimalMovement? GetAnimalMovementById(int id)
        {
            return _animalMovements.FirstOrDefault(x => x.AnimalId == id && x.Type.ToString() != "Closed" && x.Type.ToString() != "ToDispatch")!;
        }
    }
}
