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
    }
}
