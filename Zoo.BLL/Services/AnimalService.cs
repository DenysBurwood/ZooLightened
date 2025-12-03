using Zoo.BLL.Exceptions;
using Zoo.DAL.Repositories;
using Zoo.DL.Entities;

namespace Zoo.BLL.Services
{
    public class AnimalService
    {
        private readonly AnimalRepository _animalRepository;
        private readonly AnimalMovementRepository _animalMovementRepository;
        public AnimalService(AnimalRepository animalRepository, AnimalMovementRepository animalMovementRepository) 
        {
            _animalRepository = animalRepository;
            _animalMovementRepository = animalMovementRepository;
        }

        public List<Animal> DisplayAnimals(int page,int sizePage,string? query)
        {
            Func<Animal,bool>? func = null;
            string[]? queries = [];

            List<Animal>? animals = _animalRepository.GetAll(page,sizePage).ToList();
            //if(animals.Count()==0) 
            //{
            //    throw new AnimalNotFoundException();
            //}
            List<AnimalSpecies> species = animals.Select(animal => animal.Species=_animalRepository.GetSpeciesById(animal.SpeciesId)!).ToList();
            for(int i = 0; i<animals.Count(); i++) 
            {
                animals[i].Species = species[i];
            }
            return animals;
        }

        public Animal GetAnimal(int id)
        {
            Animal? animal = _animalRepository.GetEntityById(id);
            if(animal is null) 
            {
                throw new AnimalNotFoundException($"No animal found with id: {id}.");
            }
            animal.Species=_animalRepository.GetSpeciesById(animal.SpeciesId)!;
            return _animalRepository.GetEntityById(id)!;
        }

        public Animal GetAnimalByName(string name) 
        {
            Animal? animal = _animalRepository.GetAnimalByName(name);
            if(animal is null) 
            {
                throw new AnimalNotFoundException($"The animal named {name} was not found");
            }
            animal.Species=GetSpecies().FirstOrDefault(sp => sp.Id==animal.SpeciesId);
            animal.Owner=_animalRepository.GetOwnerByOwnerId(animal.OwnerId);
            if(animal.Species is null) 
            {
                throw new NotFoundException("Species not found");
            }
            return animal;
        }

        public List<AnimalSpecies> GetSpecies()
        {
            List<AnimalSpecies> animalSpecies = _animalRepository.GetSpecies();
            return animalSpecies;
        }

        public int NumberAnimalSpecies(string speciesName) 
        {
            return _animalRepository.NumberAnimalSpecies(speciesName);
        }

        public void AddAnimal(Animal animal, string speciesName) 
        {
            if(animal is null) 
            {
                throw new AnimalNotFoundException();
            }
            animal.Species=_animalRepository.GetSpeciesBySpeciesName(speciesName);
            if(animal.Species is null) 
            {
                throw new AnimalNotFoundException($"Species name of animal {animal.Name} not found");
            }
            animal.SpeciesId=animal.Species.Id;
            _animalRepository.Add(animal);
        }

        public void Modify(int id, Animal animal)
        {
            if (animal is null) throw new AnimalNotFoundException();
            _animalRepository.Update(id, animal);
        }
        public void DeleteAnimal(int id) 
        {
            Animal? animal = _animalRepository.GetEntityById(id);
            if(_animalRepository.GetEntityById(id) is null) 
            {
                throw new AnimalNotFoundException($"No animal with id: {id} to delete.");
            }
            _animalRepository.Delete(animal!);
        }
    }
}