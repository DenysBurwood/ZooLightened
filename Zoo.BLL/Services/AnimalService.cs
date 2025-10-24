using Microsoft.EntityFrameworkCore.Metadata.Conventions;
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

        public List<Animal> DisplayAnimals(int page,int sizePage,string? query)
        {
            Func<Animal,bool>? func = null;
            string[]? queries = [];
            //List<string> names = [];
            //if(name is not null)
            //{
            //    names=name.Split(',').Select(n => n.Trim()).ToList();
            //}
            //Func<Animal,bool>? func = null;
            //foreach(string animalName in names)
            //{

            //}
            //Func<Animal,bool> func = (a) => { if(a.Name.Equals("")) { return true; } else { return false; } };
            //if(query is not null) 
            //{
            //    queries = query.Split(',');
            //    queries.Select(q => q.Trim());
            //    Console.WriteLine(query);
            //    Console.WriteLine(queries);

            //    func=(animal) => (animal.Name.Equals());
            //}
            List<Animal>? animals = _animalRepository.GetAll(page,sizePage, func).ToList();
            if(animals.Count()==0) 
            {
                throw new AnimalNotFoundException();
            }
            List<AnimalSpecies> species = animals.Select(animal => animal.Species=_animalRepository.GetSpeciesById(animal.SpeciesId)!).ToList();
            for(int i = 0; i<animals.Count(); i++) 
            {
                animals[i].Species = species[i];
                //animals.ElementAt(i).Species = species[i];
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

        //public AnimalSpecies GetSpeciesBySpeciesId

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
            if(animal is null)
            {
                throw new AnimalNotFoundException();
            }
            Animal? current = _animalRepository.GetEntityById(id);
            if(_animalRepository.GetSpeciesById(animal.SpeciesId) is null || current is null)
            {
                throw new AnimalNotFoundException($"Species name of animal with id {id} and spieciesId: {animal.SpeciesId} not found");
            }
            _animalRepository.Update(current);
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

        public void RentAnimal(int id, DateTime startdate, DateTime? enddate)
        {
            Animal? animal = _animalRepository.GetEntityById(id);
            if (_animalRepository.GetEntityById(id) is null)
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
            Animal? animal = _animalRepository.GetEntityById(id);

            if (_animalRepository.GetEntityById(id) is null)
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
