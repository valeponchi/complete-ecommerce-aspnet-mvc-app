using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Data.Static;
using eTickets.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class ProducersController : Controller
    {
        private readonly IProducersService _service;
        public ProducersController(IProducersService service)
        {
            _service = service;
        }

        //GET ALL the producers
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allProducers = await _service.GetAllAsync();
            return View(allProducers);
            //if the View would have been called IndexNew,
            //and the action name stays Index(), then:
            //return View("IndexNew", allProducers);
        }


        //GET : producers/details/id
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var producerDetails = await _service.GetByIdAsync(id);
            if (producerDetails == null) return View("NotFound");
            return View(producerDetails);
        }


        //____________________________________________________________________
        //POST (as a GET producers/create_form)
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Bio")] Producer producer)
        {
            if (!ModelState.IsValid) return View(producer);
            await _service.AddAsync(producer);
            return RedirectToAction(nameof(Index));
        }


        //____________________________________________________________________
        //UPDATE (as a GET producers/Edit_form)
        public async Task<IActionResult> Edit(int id)
        {
            var producerDetails = await _service.GetByIdAsync(id);
            if (producerDetails == null) return View("NotFound");
            return View(producerDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, FullName,ProfilePictureURL,Bio")] Producer producer)
        {

            if (!ModelState.IsValid) return View(producer);
            if (id == producer.Id)
            {
                await _service.UpdateAsync(id, producer);
                return RedirectToAction(nameof(Index));
            }
            return View(producer);
        }

        //____________________________________________________________________
        //DELETE producerDetails/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var producerDetails = await _service.GetByIdAsync(id);
            if (producerDetails == null) return View("NotFound");
            return View(producerDetails);
        }


        //DELETE producerDetails
        [HttpPost, ActionName("Delete")]//if you want to keep same same on both methods
        public async Task<IActionResult> DeleteConfirmed(int id)//changed name bc you cannot have 2 methods with same-name-&-same-param
        {
            var producerDetails = await _service.GetByIdAsync(id);
            if (producerDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index)); //and return the new Index-View
        }

    }
}

//VALID CODE WITHOUT ENTITY FRAMEWORK
//private readonly AppDbContext _context;
//public ProducersController(AppDbContext context)
//{
//    _context = context;
//}

//public async Task<IActionResult> Index()
//{
//    //get all the producers
//    var allProducers = await _context.Producers.ToListAsync();
//    return View(allProducers);
//    //if the View would have been called IndexNew,
//    //and the action name stays Index(), then:
//    //return View("IndexNew", allProducers);
//}
