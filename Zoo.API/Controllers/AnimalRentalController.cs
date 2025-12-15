using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zoo.API.DTOs;
using Zoo.API.DTOs.Animals;
using Zoo.API.Mappers;
using Zoo.BLL.Exceptions;
using Zoo.BLL.Services;
using Zoo.DAL.Repositories;
using Zoo.DL.Entities;
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

        //[Authorize(Roles = "Director,Admin,Veterinarian")]
        [HttpPatch("UpdateAnimalMovements")]
        public ActionResult<AnimalIndexDTO> UpdateAnimalMovements()
        {
            _animalRentalService.UpdateAnimalMovements();
            return Ok();
        }

        //[Authorize(Roles = "Director,Admin")]
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

        //[Authorize(Roles = "Director,Admin")]
        [HttpPost("Rent")]
        public ActionResult<AnimalRentFormDto> RentAnimal([FromForm] AnimalRentFormDto animal)
        {
            if (animal is null || !ModelState.IsValid)
            {
                throw new NotAllowedException("Form not valid. Rental aborted.");
            }
            _animalRentalService.RentAnimal(animal.Id, animal.StartDate, animal.EndDate);
            return Ok();
        }

        //[Authorize(Roles = "Director,Admin")]
        [HttpPost("HireNew")]
        public ActionResult<AnimalIndexDTO> HireNewAnimal([FromForm] AnimalFormDTO animalform, [FromForm] AnimalHireFormDto hireform)
        {
            if (hireform is null || !ModelState.IsValid)
            {
                throw new NotAllowedException("Form not valid. Rental aborted.");
            }
            _animalRentalService.HireNewAnimal(animalform.FromAnimalFormDTO(), hireform.StartDate, hireform.EndDate);
            return Ok();
        }

        //[Authorize(Roles = "Director,Admin")]
        [HttpPost("HireExisting")]
        public ActionResult<AnimalIndexDTO> HireExistingAnimal([FromForm] AnimalHireFormDto animal)
        {
            if (animal is null || !ModelState.IsValid)
            {
                throw new NotAllowedException("Form not valid. Rental aborted.");
            }
            _animalRentalService.HireExistingAnimal(animal.Id, animal.StartDate, animal.EndDate);
            return Ok();
        }

        //[Authorize(Roles = "Admin,Administration")]
        [HttpPost("Receive")]
        public ActionResult<AnimalReceptionFormDto> ReceiveAnimal([FromForm] AnimalReceptionFormDto animal)
        {
            if (animal is null || !ModelState.IsValid)
            {
                throw new NotAllowedException("Form not valid. Reception aborted.");
            }
            _animalRentalService.ReceiveAnimal(animal.AnimalId, animal.ReceptionDate);
            return Ok($"The animal {animal.AnimalId} has just been received !");
        }

        //[Authorize(Roles = "Admin,Administration")]
        [HttpPost("Dispatch")]
        public ActionResult<AnimalDispatchingFormDto> DispatchAnimal([FromForm] AnimalDispatchingFormDto animal)
        {
            if (animal is null || !ModelState.IsValid)
            {
                throw new NotAllowedException("Form not valid. Dispatching aborted.");
            }
            _animalRentalService.DispatchAnimal(animal.AnimalId, animal.DispatchingDate);
            return Ok();
        }
    }
}
