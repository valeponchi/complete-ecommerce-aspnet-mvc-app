using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace eTickets.Data.Base
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new() //where T is a class
    {
        //BRING THE CONNECTION TO DB HERE
        private readonly AppDbContext _context;
        public EntityBaseRepository(AppDbContext context)
        {
            _context = context;
        }

        //__________________________________________________________________
        //POST
        public async Task AddAsync(T entity)
        { 
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync(); //NEEDED IF USING ENTITYBASE
        }

        //__________________________________________________________________
        //GET ALL
        //public async Task<IEnumerable<T>> GetAllAsync()
        //{
        //    var result = await _context.Set<T>().ToListAsync();
        //    return result;
        //}
        public async Task<IEnumerable<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();

        //__________________________________________________________________
        //GET ALL with include() from other parts
        //example: in movies we call the cinmea.name
        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return await query.ToListAsync();
        }

        //__________________________________________________________________
        //GET ONE
        //public async Task<T> GetByIdAsync(int id)
        //{
        //    var result = await _context.Set<T>().FirstOrDefaultAsync(n => n.Id == id);
        //    return result;
        //}
        public async Task<T> GetByIdAsync(int id) => await _context.Set<T>().FirstOrDefaultAsync(n => n.Id == id);

        //__________________________________________________________________
        //UPDATE
        public async Task UpdateAsync(int id, T entity)
        {
            EntityEntry entityEntry = _context.Entry<T>(entity);
            entityEntry.State = EntityState.Modified;

            await _context.SaveChangesAsync(); //NEEDED IF USING ENTITYBASE

        }

        //__________________________________________________________________
        //DELETE
        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(n => n.Id == id);
            EntityEntry entityEntry = _context.Entry<T>(entity);
            entityEntry.State = EntityState.Deleted;

            await _context.SaveChangesAsync(); //NEEDED IF USING ENTITYBASE

        }

        
    }
}
