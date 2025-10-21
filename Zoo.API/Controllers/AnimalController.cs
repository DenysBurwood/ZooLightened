using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zoo.API.DTOs;
using Zoo.API.Mappers;
using Zoo.BLL.Services;

namespace Zoo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalController:ControllerBase
    {
        private readonly AnimalService _animalService;

        public AnimalController(AnimalService animalService) 
        {
            _animalService = animalService;
        }
        [HttpGet()]
        public ActionResult<AnimalIndexDTO> DisplaySomeAnimals([FromQuery] int page = 0, int sizePage=3)
        {
            List<AnimalIndexDTO> animals = _animalService.DisplayAnimals(page, sizePage).Select(a => a.ToAnimalIndexDTO()).ToList();
            return Ok(animals);
        }

        //[HttpGet("IndexFull")]
        //public ActionResult<AnimalIndexDTO> DisplayAllAnimals()
        //{
        //    List<AnimalIndexDTO> animals = _animalService.DisplayAnimals().Select(a => a.ToAnimalIndexDTO()).ToList();
        //    return Ok(animals);
        //}

        [HttpGet("{id:int}")]
        public ActionResult<AnimalIndexDTO> GetOne([FromRoute] int id) 
        {
            AnimalIndexDTO animal = _animalService.GetAnimal(id).ToAnimalIndexDTO();
            return Ok(animal);
        }

        [HttpGet("AnimalSpecies")]
        public ActionResult<List<AnimalSpeciesDTO>> GetSpecies()
        {
            List<AnimalSpeciesDTO> animalSpecies = [];
            animalSpecies=_animalService.GetSpecies().Select(sp => sp.ToAnimalSpeciesDTO()).ToList();
            animalSpecies.ForEach(species => { species.NumberAnimals=_animalService.NumberAnimalSpecies(species.Name); });
            return animalSpecies;
        }

        [Authorize]
        [HttpPost("Birth")]
        public ActionResult<AnimalFormDTO> AddOne([FromForm] AnimalFormDTO animal) 
        {
            _animalService.AddAnimal(animal.FromAnimalFormDTO());
            return Ok();
        }

        [Authorize]
        [HttpPatch("Modification")]
        public ActionResult<AnimalFormDTO> Modify([FromForm] AnimalFormDTO animal,[FromForm] int id) 
        {
            _animalService.Modify(id, animal.FromAnimalFormDTO());
            return Ok();
        }

        [Authorize]
        [HttpPost("Death")]
        public ActionResult<AnimalFormDTO> DeleteOne([FromForm] int id) 
        {
            _animalService.DeleteAnimal(id);
            return Ok();
        }

        [HttpPost("Rent")]
        public ActionResult<AnimalRentFormDto> RentAnimal([FromForm] AnimalRentFormDto animal)
        {
            _animalService.RentAnimal(animal.Id, animal.StartDate, animal.EndDate);
            return Ok();
        }

        [HttpPost("Hire")]
        public ActionResult<AnimalIndexDTO> HireAnimal([FromForm] AnimalFormDTO animalform, [FromForm] AnimalHireFormDto hireform)
        {
            _animalService.HireAnimal(animalform.FromAnimalFormDTO(), hireform.StartDate, hireform.EndDate);
            return Ok();
        }
    }
}
