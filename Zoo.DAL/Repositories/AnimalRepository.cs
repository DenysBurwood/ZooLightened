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
            //_animalSpecies.Select(a => a.Id==1);
            //animals.ForEach(animal => { _animalSpecies.Select(a => a.Id==(int)animal.Species) });
            return animals;
        }

        public Animal? GetAnimalById(int id) 
        {
            Animal? animal = _animals.FirstOrDefault(x => x.Id==id);
            if(animal is not null) 
            {
                animal.Species=_animalSpecies.FirstOrDefault(x => x.Id==animal.SpeciesId)!;
            }
            //animal.Species.Name=_animals.FirstOrDefault(x => x.Id==id).Name;
            return animal;
        }

        public void Add(Animal entity)
        {
            _animals.Add(entity);
            _context.SaveChanges();
        }


        public void Update(int id,Animal entity)
        {
            Animal? animal=_animals.FirstOrDefault(x => x.Id == id);
            if(animal is not null) 
            {
                _animals.Update(entity);
                _context.SaveChanges();
            }
        }

        //public string GetSpeciesName(Animal animal) 
        //{
        //    AnimalSpecies species = new AnimalSpecies();

        //}
    }
}
