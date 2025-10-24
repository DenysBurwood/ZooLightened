using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zoo.API.DTOs.Animals;
using Zoo.API.Mappers;
using Zoo.BLL.Services;
using Zoo.DL.Entities;
using Zoo.DL.Entities.Humans;

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
        public ActionResult<AnimalIndexDTO> DisplaySomeAnimals([FromQuery] int page = 0, int sizePage=3, string? query=null)
        {
            List<AnimalIndexDTO> animals = _animalService.DisplayAnimals(page, sizePage, query).Select(a => a.ToAnimalIndexDTO()).ToList();
            return Ok(animals);
        }

        //[HttpGet("IndexFull")]
        //public ActionResult<AnimalIndexDTO> DisplayAllAnimals()
        //{
        //    List<AnimalIndexDTO> animals = _animalService.DisplayAnimals().Select(a => a.ToAnimalIndexDTO()).ToList();
        //    return Ok(animals);
        //}

        [HttpGet("{name}")]
        public ActionResult<AnimalDetailsDTO> GetOne([FromRoute] string name) 
        {
            AnimalDetailsDTO animal = _animalService.GetAnimalByName(name).ToAnimalDetailsDTO();
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

        [Authorize(Roles = "Admin,Veterinarian")]
        [HttpPost("Birth")]
        public ActionResult<AnimalFormDTO> AddOne([FromForm] AnimalFormDTO animal) 
        {
            _animalService.AddAnimal(animal.FromAnimalFormDTO(), animal.SpeciesName);
            return Ok();
        }

        [Authorize(Roles = "Admin,Veterinarian")]
        [HttpPatch("Modification")]
        public ActionResult<AnimalFormDTO> Modify([FromForm] AnimalFormDTO animal,[FromForm] int id) 
        {
            _animalService.Modify(id, animal.FromAnimalFormDTO());
            return Ok();
        }

        [Authorize(Roles = "Admin,Veterinarian")]
        [HttpPost("Death")]
        public ActionResult<AnimalFormDTO> DeleteOne([FromForm] int id) 
        {
            _animalService.DeleteAnimal(id);
            return Ok();
        }

        [Authorize(Roles = "Admin,Director,Administration")]
        [HttpPost("Rent")]
        public ActionResult<AnimalRentFormDto> RentAnimal([FromForm] AnimalRentFormDto animal)
        {
            _animalService.RentAnimal(animal.Id, animal.StartDate, animal.EndDate);
            return Ok();
        }

        [Authorize(Roles = "Admin,Director,Treasurer")]
        [HttpPost("HireNew")]
        public ActionResult<AnimalIndexDTO> HireNewAnimal([FromForm] AnimalFormDTO animalform, [FromForm] AnimalHireFormDto hireform)
        {
            _animalService.HireNewAnimal(animalform.FromAnimalFormDTO(), hireform.StartDate, hireform.EndDate);
            return Ok();
        }

        [Authorize(Roles = "Admin,Director,Treasurer")]
        [HttpPost("HireExisting")]
        public ActionResult<AnimalIndexDTO> HireExistingAnimal([FromForm] AnimalHireFormDto animal)
        {
            _animalService.HireExistingAnimal(animal.Id, animal.StartDate, animal.EndDate);
            return Ok();
        }

    }
}
