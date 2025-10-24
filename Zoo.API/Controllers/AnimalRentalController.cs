using Microsoft.AspNetCore.Mvc;
using Zoo.API.DTOs;
using Zoo.API.Mappers;
using Zoo.BLL.Services;
using Zoo.DAL.Repositories;
using Zoo.DL.Enum;

namespace Zoo.API.Controllers
{
    public class AnimalRentalController : Controller
    {
        private readonly AnimalRentalService _animalRentalService;

        public AnimalRentalController(AnimalRentalService animalRentalService)
        {
            _animalRentalService = animalRentalService;
        }

        [HttpPatch("UpdateAnimalMovements")]
        public ActionResult<AnimalIndexDTO> UpdateAnimalMovements()
        {
            _animalRentalService.UpdateAnimalMovements();
            return Ok();
        }

        [HttpPost("GetMovementsToHandle")]
        public ActionResult<List<AnimalMovementDto>> GetMovementsToHandle([FromForm] int page = 0, [FromForm] int nbPage = 10)
        {
            var movements = _animalRentalService.GetAll(page, nbPage,
                predicate: m => m.Type == AnimalMovementType.ToReceive
                             || m.Type == AnimalMovementType.ToDispatch,
                includes: new[] { "Animal", "Animal.Owner", "Animal.Species", "CounterPart" }
                );

            var movementDtos = movements.Select(m => m.ToAnimalMovementDto());

            return Ok(movementDtos);
        }

        [HttpPost("Rent")]
        public ActionResult<AnimalRentFormDto> RentAnimal([FromForm] AnimalRentFormDto animal)
        {
            _animalRentalService.RentAnimal(animal.Id, animal.StartDate, animal.EndDate);
            return Ok();
        }

        [HttpPost("HireNew")]
        public ActionResult<AnimalIndexDTO> HireNewAnimal([FromForm] AnimalFormDTO animalform, [FromForm] AnimalHireFormDto hireform)
        {
            _animalRentalService.HireNewAnimal(animalform.FromAnimalFormDTO(), hireform.StartDate, hireform.EndDate);
            return Ok();
        }

        [HttpPost("HireExisting")]
        public ActionResult<AnimalIndexDTO> HireExistingAnimal([FromForm] AnimalHireFormDto animal)
        {
            _animalRentalService.HireExistingAnimal(animal.Id, animal.StartDate, animal.EndDate);
            return Ok();
        }

        [HttpPost("Receive")]
        public ActionResult<AnimalReceptionFormDto> ReceiveAnimal([FromForm] AnimalReceptionFormDto animal)
        {
            _animalRentalService.ReceiveAnimal(animal.AnimalId, animal.ReceptionDate);
            return Ok($"The animal {animal.AnimalId} has just been received !");
        }

        [HttpPost("Dispatch")]
        public ActionResult<AnimalDispatchingFormDto> DispatchAnimal([FromForm] AnimalDispatchingFormDto animal)
        {
            _animalRentalService.DispatchAnimal(animal.AnimalId, animal.DispatchingDate);
            return Ok();
        }
    }
}
