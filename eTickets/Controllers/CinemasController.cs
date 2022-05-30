using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Data.Static;
using eTickets.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]

    public class CinemasController : Controller
    {

        private readonly ICinemasService _service;
        public CinemasController(ICinemasService service)
        {
            _service = service;
        }

        //GET ALL the cinemas
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allCinemas = await _service.GetAllAsync();
            return View(allCinemas);
            //if the View would have been called IndexNew,
            //and the action name stays Index(), then:
            //return View("IndexNew", allCinemas);
        }

        //GET : cinemas/details/id
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var cinemasDetails = await _service.GetByIdAsync(id);
            if (cinemasDetails == null) return View("NotFound");
            return View(cinemasDetails);
        }

        //____________________________________________________________________
        //POST (as a GET cinemas/create_form)
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Logo, Name, Description")] Cinema cinema)
        {
            if (!ModelState.IsValid) return View(cinema);
            await _service.AddAsync(cinema);
            return RedirectToAction(nameof(Index));
        }


        //____________________________________________________________________
        //UPDATE (as a GET cinemas/Edit_form)
        public async Task<IActionResult> Edit(int id)
        {
            var cinemasDetails = await _service.GetByIdAsync(id);
            if (cinemasDetails == null) return View("NotFound");
            return View(cinemasDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name,Logo,Description")] Cinema cinema)
        {

            {
                if (!ModelState.IsValid) return View(cinema);
                //if (id == cinema.Id)
                await _service.UpdateAsync(id, cinema);
                return RedirectToAction(nameof(Index));
            }
            //return View(cinema);
        }

        //____________________________________________________________________
        //DELETE cinemas/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var cinemasDetails = await _service.GetByIdAsync(id);
            if (cinemasDetails == null) return View("NotFound");
            return View(cinemasDetails);
        }


        //DELETE producerDetails
        [HttpPost, ActionName("Delete")]//if you want to keep same same on both methods
        public async Task<IActionResult> DeleteConfirmed(int id)//changed name bc you cannot have 2 methods with same-name-&-same-param
        {
            var cinemasDetails = await _service.GetByIdAsync(id);
            if (cinemasDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index)); //and return the new Index-View
        }
    }
}
