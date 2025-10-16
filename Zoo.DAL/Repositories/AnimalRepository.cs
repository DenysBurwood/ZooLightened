using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoo.DAL.Contexts;
using Zoo.DL.Entities;
using Zoo.DL.Enum;

namespace Zoo.DAL.Repositories
{
    public class AnimalRepository
    {
        private readonly DbSet<Animal> _animals;
        private readonly DbSet<AnimalSpecies> _animalSpecies;       //  Adjoindre les espèces animales pour pouvoir récupérer le nom dans la liste d'animaux
        private readonly ZooContext _context;

        public AnimalRepository(ZooContext context)
        {
            _context=context;
            _animals=context.Animals;
            _animalSpecies=context.AnimalSpecies;
        }

        public List<Animal> GetAllAnimals() 
        {
            List<Animal> animals = _animals.ToList();
            animals.ForEach(animal => { animal.Species=_animalSpecies.FirstOrDefault(x => x.Id==animal.SpeciesId)!; });
            return animals;
        }
        public List<Animal> GetSomeAnimals(int page, int sizePage) 
        {
            List<Animal> animals = _animals.Skip(page*sizePage).Take(sizePage).ToList();
            animals.ForEach(animal => { animal.Species=_animalSpecies.FirstOrDefault(x => x.Id==animal.SpeciesId)!; });
            return animals;
        }

        public Animal? GetAnimalById(int id) 
        {
            Animal? animal = _animals.FirstOrDefault(x => x.Id==id);
            if(animal is not null) 
            {
                animal.Species=_animalSpecies.FirstOrDefault(x => x.Id==animal.SpeciesId)!;
            }
            return animal;
        }
        public AnimalSpecies? GetSpeciesName(Animal animal) 
        {
            return _animalSpecies.FirstOrDefault(x => x.Id==animal.SpeciesId);
        }

        public void Add(Animal entity)
        {
            _animals.Add(entity);
            _context.SaveChanges();
        }

        //  Update not working properly. Need to further inquire inside.
        public void Update(Animal entity)
        {
            Animal current = _animals.FirstOrDefault(x => x.Id==entity.Id)!;
            if(current is not null) 
            {
                current.Name=entity.Name;
                current.SpeciesId=entity.SpeciesId;
                current.Sex=entity.Sex;
                current.Species=entity.Species;
            }
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
    }
}
