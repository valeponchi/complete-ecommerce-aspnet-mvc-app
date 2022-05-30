using eTickets.Data;
using eTickets.Data.Base;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public class ActorsService : EntityBaseRepository<Actor>, IActorsService //inherit the interface in here
        //we implement the generic methods from the EntityBaseRepository
    {
        //to be able to work with DB - inject AppDbContext file
        public ActorsService(AppDbContext context) : base(context) { } //ctor

    }
}


//VALID CODE IF NOT USING THE IEntityBase/EntityBaseRepo/IEntityBaseRepo
//public class ActorsService : IActorsService //inherit the interface in here
//{
//    //to be able to work with DB - inject AppDbContext file
//    private readonly AppDbContext _context;
//    public ActorsService(AppDbContext context) //ctor
//    {
//        _context = context;
//    }

//    public async Task AddAsync(Actor actor)
//    {
//        await _context.Actors.AddAsync(actor);
//        await _context.SaveChangesAsync();
//    }

//    public async Task DeleteAsync(int id)
//    {
//        var result = await _context.Actors.FirstOrDefaultAsync(n => n.Id == id);
//         _context.Actors.Remove(result);
//        await _context.SaveChangesAsync();
//    }

//    public async Task<IEnumerable<Actor>> GetAllAsync()
//    {
//        var result = await _context.Actors.ToListAsync();
//        return result;
//    }

//    public async Task<Actor> GetByIdAsync(int id)
//    {
//            var result = await _context.Actors.FirstOrDefaultAsync(n => n.Id == id);
//            return result;
//    }

//    public async Task<Actor> UpdateAsync(int id, Actor newActor)
//    {
//        _context.Update(newActor);
//        await _context.SaveChangesAsync();
//        return newActor;
//    }
//}