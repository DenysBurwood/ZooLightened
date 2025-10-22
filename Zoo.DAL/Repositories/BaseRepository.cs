using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Zoo.DAL.Contexts;
using Zoo.DL.Entities;

namespace Zoo.DAL.Repositories
{
    public class BaseRepository<T> where T : BaseEntity
    {
        private readonly ZooContext _context;
        private readonly DbSet<T> _entities;


        public BaseRepository(ZooContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        //public List<T> GetAll(int page = 0, int nbPage = 10, Func<T, bool>? predicate = null)
        //{

        //    IEnumerable<T> query = _entities;

        //    if (predicate != null)
        //    {
        //        query = query.Where(predicate);
        //    }

        //    return query
        //        .OrderBy(p => p.Id)
        //        .Skip(page * nbPage)
        //        .Take(nbPage)
        //        .ToList();
        //}

        public List<T> GetAll(int page = 0,int nbPage = 10,Expression<Func<T, bool>>? predicate = null,params string[]? includes)
        {
            IQueryable<T> query = _entities.AsQueryable();


            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return query
                .OrderBy(e => EF.Property<object>(e, "Id"))
                .Skip(page * nbPage)
                .Take(nbPage)
                .ToList();
        }

        public T? GetEntity(int id)
        {
            return _entities.Find(id);
        }

        public void Add(T entity)
        {
            _entities.Add(entity);
            _context.SaveChanges();
        }

        public void Add(List<T> entities)
        {
            _entities.AddRange(entities);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _entities.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _entities.Remove(entity);
            _context.SaveChanges();
        }
    }
}