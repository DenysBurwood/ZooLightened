using Microsoft.AspNetCore.Mvc;
using Zoo.API.DTOs;
using Zoo.API.Mappers;
using Zoo.BLL.Services;

namespace Zoo.API.Controllers
{
    [Route("api/Animal/[controller]")]
    [ApiController]
    public class AnimalController:ControllerBase
    {
        private readonly AnimalService _animalService;

        public AnimalController(AnimalService animalService) 
        {
            _animalService = animalService;
        }
        [HttpGet("Index")]
        public ActionResult<AnimalIndexDTO> DisplaySomeAnimals([FromQuery] int page = 0, int sizePage=3)
        {
            List<AnimalIndexDTO> animals = _animalService.DisplayAnimals(page, sizePage).Select(a => a.ToAnimalIndexDTO()).ToList();
            return Ok(animals);
        }

        [HttpGet("IndexFull")]
        public ActionResult<AnimalIndexDTO> DisplayAllAnimals()
        {
            List<AnimalIndexDTO> animals = _animalService.DisplayAnimals().Select(a => a.ToAnimalIndexDTO()).ToList();
            return Ok(animals);
        }

        [HttpGet("IndexOne")]
        public ActionResult<AnimalIndexDTO> GetOne([FromQuery] int id) 
        {
            AnimalIndexDTO animal = _animalService.GetAnimal(id).ToAnimalIndexDTO();
            return Ok(animal);
        }

        [HttpPost("Birth")]
        public ActionResult<AnimalFormDTO> AddOne([FromForm] AnimalFormDTO animal) 
        {
            _animalService.AddAnimal(animal.FromAnimalFormDTO());
            return Ok();
        }

        [HttpPatch("Modification")]
        public ActionResult<AnimalFormDTO> Modify([FromForm] AnimalFormDTO animal,[FromForm] int id) 
        {
            _animalService.Modify(id, animal.FromAnimalFormDTO());
            return Ok();
        }

        [HttpPost("Death")]
        public ActionResult<AnimalFormDTO> DeleteOne([FromForm] int id) 
        {
            _animalService.DeleteAnimal(id);
            return Ok();
        }
    }
}
