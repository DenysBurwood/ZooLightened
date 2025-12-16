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
        private readonly DbSet<Owner> _owners;
        private readonly ZooContext _context;

        public AnimalRepository(ZooContext context):base(context)
        {
            _context = context;
            _animals = context.Animals;
            _animalSpecies = context.AnimalSpecies;
            _animalMovements = context.AnimalMovements;
            _owners=context.Owners;
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

        public Animal? GetAnimalByName(string name) 
        {
            Animal? animal = _animals.FirstOrDefault(a => a.Name==name);
            return animal;
        }

        public AnimalSpecies? GetSpeciesById(int speciesId) 
        {
            return _animalSpecies.FirstOrDefault(x => x.Id == speciesId);
        }
        public AnimalSpecies? GetSpeciesBySpeciesName(string speciesName) 
        {
            return _animalSpecies.FirstOrDefault(sp => sp.Name==speciesName);
        }

        public Owner? GetOwnerByOwnerId(int ownerId) 
        {
            return _owners.FirstOrDefault(y => y.Id==ownerId);
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

        public void Update(int id, Animal entity)
        {
            var current = _context.Set<Animal>()
                .Include(a => a.Species)
                .FirstOrDefault(a => a.Id == id);

            if (current is null) throw new Exception($"Animal {id} not found");

            var species = _animalSpecies.FirstOrDefault(s => s.Name == entity.Species.Name);

            if (species is null) throw new Exception($"Species '{entity.Species.Name}' not found");

            current.Name = entity.Name;
            current.Sex = entity.Sex;
            current.OwnerId = entity.OwnerId;
            current.BirthDate = entity.BirthDate;
            current.RIPDate = entity.RIPDate;

           
            current.SpeciesId = species.Id;


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

        public AnimalMovement? GetMovementToReceiveById(int id)
        {
            return _animalMovements.FirstOrDefault(x => x.AnimalId == id && x.Type != AnimalMovementType.Closed && x.Type != AnimalMovementType.ToDispatch)!;
        }

        public AnimalMovement? GetMovementToDispatchById(int id)
        {
            return _animalMovements.FirstOrDefault(x => x.AnimalId == id && x.Type != AnimalMovementType.Closed && x.Type != AnimalMovementType.ToReceive)!;
        }
        
    }
}
