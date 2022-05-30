//we define the method signature
//  bc an interface is just a contract
//      contract: any class that implements the interface,
//          must have that interface's set of public methods
//          Classes that implement the interface agree to provide code
//          for the methods that are defined in the interface.

using eTickets.Data.Base;
using eTickets.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public interface IActorsService:IEntityBaseRepository<Actor>
    {
    }
}

        //VALID CODE IF NOT USING THE IEntityBase/EntityBaseRepo/IEntityBaseRepo
        ////from the DB:
        ////METHODS 
        ////Task<Return> name(param)
        //Task<IEnumerable<Actor>> GetAllAsync();    //R return type(enum-of-actor) GET ALL
        //Task<Actor> GetByIdAsync(int id);          //R return a single actor GET ONE
        //Task AddAsync(Actor actor);          //C return nothing ADD ONE
        //Task <Actor> UpdateAsync(int id, Actor newActor); //UPDATE
        //Task DeleteAsync(int id);            //DELETE
