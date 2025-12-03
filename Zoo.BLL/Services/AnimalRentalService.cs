using System.Linq.Expressions;
using Zoo.BLL.Exceptions;
using Zoo.DAL.Repositories;
using Zoo.DL.Entities;
using Zoo.DL.Enum;

namespace Zoo.BLL.Services
{
    public class AnimalRentalService
    {
        private readonly AnimalRepository _animalRepository;
        private readonly AnimalMovementRepository _animalMovementRepository;
        public AnimalRentalService(AnimalRepository animalRepository, AnimalMovementRepository animalMovementRepository)
        {
            _animalRepository = animalRepository;
            _animalMovementRepository = animalMovementRepository;
        }

        public void UpdateAnimalMovements()
        {
            List<AnimalMovement> movements = _animalMovementRepository.GetAllNotClosed().ToList();

            foreach(var  movement in movements)
            {
                bool toupdate = false;

                if (movement.StartDate >= DateTime.Now && (movement.EndDate < DateTime.Now || movement.EndDate is null) && movement.Type == AnimalMovementType.Initialized)
                {
                    movement.Type = movement.Direction == Direction.OUT ? AnimalMovementType.ToDispatch : AnimalMovementType.ToReceive;
                    toupdate = true;
                }
                else if (movement.EndDate >= DateTime.Now)
                {
                    movement.Type = movement.Direction == Direction.OUT ? AnimalMovementType.ToReceive : AnimalMovementType.ToDispatch;
                    toupdate = true;
                }

                if (toupdate)
                {
                    _animalMovementRepository.Update(movement);
                }
            }
        }

        public List<AnimalMovement> GetAll(int page = 0, int nbPage = 10, Expression<Func<AnimalMovement, bool>>? predicate = null, params string[]? includes)
        {
            return _animalMovementRepository.GetAll(page, nbPage, predicate, includes);
        }

        public void RentAnimal(int id, DateTime startdate, DateTime? enddate)
        {
            Animal? animal = _animalRepository.GetAnimalById(id);
            if (_animalRepository.GetAnimalById(id) is null)
            {
                throw new AnimalNotFoundException($"No animal with id: {id} is not to be found.");
            }
            if (animal!.RIPDate is not null)
            {
                throw new AnimalNotAvailableForRentException($"You may not rent a dead animal. (id: {id})");
            }

            if (animal!.OwnerId != 1)
            {
                throw new AnimalNotAvailableForRentException($"The animal with id: {id} doesn't belong to our zoo.");
            }

            if (!_animalRepository.IsAnimalAvailableForMovement(animal, startdate, enddate))
            {
                throw new AnimalNotAvailableForRentException($"The animal with id: {id} is not available to rent from {startdate} till {enddate}.");
            }

            _animalRepository.InsertAnimalMovement(animal, startdate, enddate, DL.Enum.Direction.OUT);
        }

        public void HireNewAnimal(Animal animal, DateTime startdate, DateTime? enddate)
        {
            if (animal.OwnerId == 1)
            {
                throw new AnimalNotAvailableForHireException($"Impossible to hire an animal which belongs to our zoo.");
            }

            _animalRepository.Add(animal);

            _animalRepository.InsertAnimalMovement(animal, startdate, enddate, DL.Enum.Direction.IN);
        }

        public void HireExistingAnimal(int id, DateTime startdate, DateTime? enddate)
        {
            Animal? animal = _animalRepository.GetAnimalById(id);

            if (_animalRepository.GetAnimalById(id) is null)
            {
                throw new AnimalNotFoundException($"No animal with id: {id} is not to be found.");
            }
            if (animal!.RIPDate is not null)
            {
                throw new AnimalNotAvailableForHireException($"You may not hire a dead animal. (id: {id})");
            }
            if (animal!.OwnerId == 1)
            {
                throw new AnimalNotAvailableForHireException($"Impossible to hire an animal which belongs to our zoo.");
            }
            if (!_animalRepository.IsAnimalAvailableForMovement(animal, startdate, enddate))
            {
                throw new AnimalNotAvailableForHireException($"The animal with id: {id} is not available to hire from {startdate} till {enddate}.");
            }

            _animalRepository.InsertAnimalMovement(animal, startdate, enddate, DL.Enum.Direction.IN);

        }

        public void ReceiveAnimal(int id, DateTime receptiondate)
        {
            Animal? animal = _animalRepository.GetAnimalById(id);

            if (_animalRepository.GetAnimalById(id) is null)
            {
                throw new AnimalNotFoundException($"No animal with id: {id} is not to be found.");
            }
            if (animal!.RIPDate is not null)
            {
                throw new AnimalNotAvailableException($"You may not receive a dead animal. (id: {id})");
            }
            if (animal.IsAvailable)
            {
                throw new AnimalNotAvailableException($"The animal has already been received. (id: {id})");
            }

            AnimalMovement? movement = _animalRepository.GetMovementToReceiveById(id)!;

            if (movement is null)
            {
                throw new AnimalNotAvailableException($"No pending movement (Opened or ToReceive) has been found for this animal. (id: {id})");
            }

            animal.IsAvailable = true;
            _animalRepository.Update(animal);

            if (movement.Direction == Direction.OUT)
            {
                movement.EndDate = receptiondate;
                movement.Type = AnimalMovementType.Closed;
            }
            else if (movement.Direction == Direction.IN)
            {
                movement.StartDate = receptiondate;
                movement.Type = AnimalMovementType.Opened;
            }

            _animalMovementRepository.Update(movement);
        }

        public void DispatchAnimal(int id, DateTime dispatchingdate)
        {
            Animal? animal = _animalRepository.GetAnimalById(id);

            if (_animalRepository.GetAnimalById(id) is null)
            {
                throw new AnimalNotFoundException($"No animal with id: {id} is not to be found.");
            }
            if (animal!.RIPDate is not null)
            {
                throw new AnimalNotAvailableException($"You may not dispatch a dead animal. (id: {id})");
            }
            if (!animal.IsAvailable)
            {
                throw new AnimalNotAvailableException($"The animal has already been dispatched. (id: {id})");
            }

            AnimalMovement? movement = _animalRepository.GetMovementToDispatchById(id)!;

            if (movement is null)
            {
                throw new AnimalNotAvailableException($"No pending movement (Opened or ToDispatch) has been found for this animal. (id: {id})");
            }

            animal.IsAvailable = false;
            _animalRepository.Update(animal);

            if (movement.Direction == Direction.OUT)
            {
                movement.StartDate = dispatchingdate;
                movement.Type = AnimalMovementType.Opened;
            }
            else if (movement.Direction == Direction.IN)
            {
                movement.EndDate = dispatchingdate;
                movement.Type = AnimalMovementType.Closed;
            }

            _animalMovementRepository.Update(movement);
        }
    }
}
