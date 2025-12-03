using Microsoft.EntityFrameworkCore;
using Zoo.DAL.Contexts;
using Zoo.DL.Entities;

namespace Zoo.DAL.Repositories
{
    public class AnimalMovementRepository : BaseRepository<AnimalMovement>
    {
        private readonly DbSet<AnimalMovement> _animalMovements;
        private readonly ZooContext _context;

        public AnimalMovementRepository(ZooContext context) : base(context)
        {
            _context = context;
            _animalMovements = context.AnimalMovements;
        }

        public IEnumerable<AnimalMovement> GetAllNotClosed()
        {
            return [.._animalMovements.Where(am => am.Type != DL.Enum.AnimalMovementType.Closed)];
        }
    }
}
