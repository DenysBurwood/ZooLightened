using Zoo.DAL.Contexts;

namespace Zoo.DAL.Repositories
{
    public abstract class BaseRepository<TEntity, TId> where TEntity : class
    {
        protected readonly ZooContext _context;
        public BaseRepository(ZooContext context)
        {
            _context=context;
            //context.<TEntity>;
        }

        

        
    }

}
