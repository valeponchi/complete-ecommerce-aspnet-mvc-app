using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace eTickets.Data.Base
{
    //T means Generic type (actor/movie/producer etc)
    public interface IEntityBaseRepository<T> where T : class, IEntityBase, new() 
    {
        Task<IEnumerable<T>> GetAllAsync();    //R return type(any) GET ALL
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties);    //R return type(any) GET ALL+include

        Task<T> GetByIdAsync(int id);          //R return a single GET ONE
        Task AddAsync(T entity);          //C return nothing ADD ONE
        Task UpdateAsync(int id, T entity); //UPDATE
        Task DeleteAsync(int id);            //DELETE
    }
}
