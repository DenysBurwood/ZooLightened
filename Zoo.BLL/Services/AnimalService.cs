using Microsoft.EntityFrameworkCore;
using Zoo.BLL.Exceptions;
using Zoo.DAL.Repositories;
using Zoo.DL.Entities;
using Zoo.DL.Enum;

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

        public List<Animal> DisplayAnimals(int page, int sizePage)
        {
            List<Animal>? animals = _animalRepository.GetSomeAnimals(page,sizePage);
            if(animals.Count()==0) 
            {
                throw new AnimalNotFoundException();
            }
            return animals;
        }

        public List<Animal> DisplayAnimals()
        {
            List<Animal>? animals = _animalRepository.GetAllAnimals();
            if(animals.Count()==0) 
            {
                throw new AnimalNotFoundException();
            }
            return animals;
        }

        public Animal GetAnimal(int id)
        {
            Animal? animal = _animalRepository.GetAnimalById(id);
            if(animal is null) 
            {
                throw new AnimalNotFoundException($"No animal found with id: {id}.");
            } 
            return _animalRepository.GetAnimalById(id)!;
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

        public void AddAnimal(Animal animal) 
        {
            if(animal is null) 
            {
                throw new AnimalNotFoundException();
            }
            if(_animalRepository.GetSpeciesById(animal.SpeciesId) is null) 
            {
                throw new AnimalNotFoundException($"Species name of animal {animal.Name} with spieciesId: {animal.SpeciesId} not found");
            }
            _animalRepository.Add(animal);
        }

        public void Modify(int id, Animal animal) 
        {
            if(animal is null)
            {
                throw new AnimalNotFoundException();
            }
            Animal? current = _animalRepository.GetAnimalById(id);
            if(_animalRepository.GetSpeciesById(animal.SpeciesId) is null || current is null)
            {
                throw new AnimalNotFoundException($"Species name of animal with id {id} and spieciesId: {animal.SpeciesId} not found");
            }
            current.Name=animal.Name;
            current.SpeciesId=animal.SpeciesId;
            current.Sex=animal.Sex;
            current.Species=animal.Species;
            _animalRepository.Update(current);
        }
        public void DeleteAnimal(int id) 
        {
            Animal? animal = _animalRepository.GetAnimalById(id);
            if(_animalRepository.GetAnimalById(id) is null) 
            {
                throw new AnimalNotFoundException($"No animal with id: {id} to delete.");
            }
            _animalRepository.Delete(animal!);
        }
    }
}