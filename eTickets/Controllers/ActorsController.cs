using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Data.Static;
using eTickets.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    //this is called DECORATOR
    [Authorize(Roles = UserRoles.Admin)]
    //without any role it'll only check if you are logged in or not

    public class ActorsController : Controller
    {
        //CONTROLLER: get/send data from/to DB

        //DECLARATION of the DB-translator here
        //it's our AppDbContext file
        private readonly IActorsService _service;

        //TO USE the TRANSLATOR we inject it in the ctor
        public ActorsController(IActorsService service)
        //public ActorsController(AppDbContext context)
        {
            //_context = context
            _service = service;
        }

        //____________________________________________________________________
        //GET ALL Actors/
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync(); //get all the actors
            return View(data); //list of actors to the View
        }

        //when you want to call this View you have
        // ../Actors from ActorsController
        // ../Index from the name of the action being called
        //Index action is default
        //so it'll work also just as ../Actors

        //____________________________________________________________________
        //GET Actors/Details/id
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);

            if (actorDetails == null) return View("NotFound");
            return View(actorDetails);
        }


        //____________________________________________________________________
        //POST Actors/Create_form
        public IActionResult Create()
        {
            return View();
        }

        //POST Actor
        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Bio")]Actor actor)
        {
            if (!ModelState.IsValid) //if not all the model-required-fields are sent
            {
                return View(actor); //we return the same View with the error-msg
            }
            await _service.AddAsync(actor); //otherwise se add the actor to DB
            return RedirectToAction(nameof(Index)); //and return the new Index-View
        }


        //____________________________________________________________________
        //UPDATE Actors/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);
            if (actorDetails == null) return View("NotFound");
            return View(actorDetails);
        }


        //UPDATE Actor
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, FullName,ProfilePictureURL,Bio")] Actor actor)
        {
            if (!ModelState.IsValid) //if not all the model-required-fields are sent
            {
                return View(actor); //we return the same View with the error-msg
            }
            await _service.UpdateAsync(id, actor); //otherwise se add the actor to DB
            return RedirectToAction(nameof(Index)); //and return the new Index-View
        }

        //____________________________________________________________________
        //DELETE Actors/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);
            if (actorDetails == null) return View("NotFound");
            return View(actorDetails);
        }


        //DELETE Actor
        [HttpPost, ActionName("Delete")]//if you want to keep same same on both methods
        public async Task<IActionResult> DeleteConfirmed(int id)//changed name bc you cannot have 2 methods with same-name-&-same-param
        {
            var actorDetails = await _service.GetByIdAsync(id);
            if (actorDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index)); //and return the new Index-View
        }


    }
}
