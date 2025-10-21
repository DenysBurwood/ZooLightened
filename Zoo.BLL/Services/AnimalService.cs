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

        public void RentAnimal(int id, DateTime startdate, DateTime? enddate)
        {
            Animal? animal = _animalRepository.GetAnimalById(id);
            if (_animalRepository.GetAnimalById(id) is null)
            {
                throw new AnimalNotFoundException($"No animal with id: {id} is not to be found.");
            }
            if (animal!.RIPDate is not null)
            {
                throw new AnimalNotAvailableForRentException($"You may not rent a dead animal. (id: {id})");
            }

            if (animal!.OwnerId != 1)
            {
                throw new AnimalNotAvailableForRentException($"The animal with id: {id} doesn't belong to our zoo.");
            }

            if (!_animalRepository.IsAnimalAvailableForMovement(animal, startdate, enddate))
            {
                throw new AnimalNotAvailableForRentException($"The animal with id: {id} is not available to rent from {startdate} till {enddate}.");
            }

            _animalRepository.InsertAnimalMovement(animal, startdate, enddate, DL.Enum.Direction.OUT);
        }

        public void HireNewAnimal(Animal animal, DateTime startdate, DateTime? enddate)
        {
            if (animal.OwnerId == 1)
            {
                throw new AnimalNotAvailableForHireException($"Impossible to hire an animal which belongs to our zoo.");
            }

            _animalRepository.Add(animal);

            _animalRepository.InsertAnimalMovement(animal, startdate, enddate, DL.Enum.Direction.IN);
        }

        public void HireExistingAnimal(int id, DateTime startdate, DateTime? enddate)
        {
            Animal? animal = _animalRepository.GetAnimalById(id);

            if (_animalRepository.GetAnimalById(id) is null)
            {
                throw new AnimalNotFoundException($"No animal with id: {id} is not to be found.");
            }
            if (animal!.RIPDate is not null)
            {
                throw new AnimalNotAvailableForHireException($"You may not hire a dead animal. (id: {id})");
            }
            if (animal!.OwnerId == 1)
            {
                throw new AnimalNotAvailableForHireException($"Impossible to hire an animal which belongs to our zoo.");
            }
            if (!_animalRepository.IsAnimalAvailableForMovement(animal, startdate, enddate))
            {
                throw new AnimalNotAvailableForHireException($"The animal with id: {id} is not available to hire from {startdate} till {enddate}.");
            }

            _animalRepository.InsertAnimalMovement(animal, startdate, enddate, DL.Enum.Direction.IN);

        }
    }
}
