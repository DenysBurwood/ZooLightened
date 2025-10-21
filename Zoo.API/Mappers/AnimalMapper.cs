using Zoo.API.DTOs;
using Zoo.DL.Entities;

namespace Zoo.API.Mappers
{
    public static class AnimalMapper
    {
        public static AnimalIndexDTO ToAnimalIndexDTO(this Animal Animal) 
        {
            return new AnimalIndexDTO()
            {
                Name = Animal.Name,
                SpeciesName=Animal.Species.Name,
                Sex=Animal.Sex,
            };
        }

        public static AnimalFormDTO ToAnimalFormDTO(this Animal animal) 
        {
            return new AnimalFormDTO()
            {
                Name=animal.Name,
                Sex=animal.Sex,
                SpeciesId=animal.Species.Id,
            };
        }
        public static Animal FromAnimalFormDTO(this AnimalFormDTO animalForm) 
        {
            return new Animal()
            {
                Name=animalForm.Name,
                SpeciesId=animalForm.SpeciesId,
                OwnerId=animalForm.OwnerId,
                Sex = animalForm.Sex,
            };
        }

        public static AnimalSpeciesDTO ToAnimalSpeciesDTO(this AnimalSpecies species) 
        {
            return new AnimalSpeciesDTO()
            {
                Name=species.Name,
                Description=species.Description,
                NumberAnimals=0,
            };
        }

        public static Animal FromAnimalRentFormDto(this AnimalRentFormDto animalRentForm)
        {
            return new Animal()
            {
                Id = animalRentForm.Id,
                Name = animalRentForm.Name,
            };
        }
    }
}
