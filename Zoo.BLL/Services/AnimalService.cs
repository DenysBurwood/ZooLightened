using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoo.BLL.Exceptions;
using Zoo.DAL.Repositories;
using Zoo.DL.Entities;

namespace Zoo.BLL.Services
{
    public class AnimalService
    {
        private readonly AnimalRepository _animalRepository;
        public AnimalService(AnimalRepository animalRepository) 
        {
            _animalRepository = animalRepository;
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

        public void AddAnimal(Animal animal) 
        {
            if(animal is null) 
            {
                throw new AnimalNotFoundException();
            }
            if(_animalRepository.GetSpeciesName(animal) is null) 
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
            if(_animalRepository.GetSpeciesName(animal) is null || _animalRepository.GetAnimalById(id) is null)
            {
                throw new AnimalNotFoundException($"Species name of animal with id {id} and spieciesId: {animal.SpeciesId} not found");
            }
            _animalRepository.Update(animal);
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
