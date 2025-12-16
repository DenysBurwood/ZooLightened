using Zoo.API.DTOs;
using Zoo.API.DTOs.Animals;
using Zoo.DL.Entities;

namespace Zoo.API.Mappers
{
    public static class AnimalMapper
    {
        public static AnimalIndexDTO ToAnimalIndexDTO(this Animal Animal) 
        {
            return new AnimalIndexDTO()
            {
                Id = Animal.Id,
                Name = Animal.Name,
                SpeciesName=Animal.Species.Name,
                Sex=Animal.Sex.ToString(),
                SexId = (int)Animal.Sex,
            };
        }

        //public static AnimalFormDTO ToAnimalFormDTO(this Animal animal) 
        //{
        //    return new AnimalFormDTO()
        //    {
        //        Name=animal.Name,
        //        Sex=animal.Sex,
        //        SpeciesName=animal.Species.Name,
        //        OwnerId =animal.OwnerId,
        //        BirthDate=animal.BirthDate,
        //        RIPDate=animal.RIPDate,
        //    };
        //}

        public static AnimalDetailsDTO ToAnimalDetailsDTO(this Animal animal) 
        {
            return new AnimalDetailsDTO()
            {
                Id=animal.Id,
                Name=animal.Name,
                OwnerName=animal.Owner is null ? null : animal.Owner.Name,
                OwnerId=animal.OwnerId,
                SpeciesName=animal.Species.Name,
                BirthDate=animal.BirthDate,
                RIPDate=animal.RIPDate,
                Sex=animal.Sex.ToString(),
                SexId=(int)animal.Sex,
                IsAvailable=animal.IsAvailable,
                Description=animal.Description,
            };
        }

        public static Animal FromAnimalFormDTO(this AnimalFormDTO dto)
        {
            return new Animal
            {
                Name = dto.Name,
                Sex = dto.Sex,
                OwnerId = dto.OwnerId,
                BirthDate = dto.BirthDate,
                RIPDate = dto.RIPDate,
                Species = new AnimalSpecies { Name = dto.SpeciesName },
                Description = dto.Description,
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

        public static AnimalMovementDto ToAnimalMovementDto(this AnimalMovement m)
        {
            return new AnimalMovementDto()
            {
                Id = m.Id,
                Type = m.Type.ToString(),
                StartDate = m.StartDate,
                EndDate = m.EndDate,

                AnimalId = m.Animal.Id,
                AnimalName = m.Animal.Name,
                SpeciesName = m.Animal.Species.Name,
                OwnerName = m.Animal.Owner!.Name,
                CounterPartName = m.CounterPart.Name,
            };
        }
    }
}
