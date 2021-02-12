using Hotel.Shared.FilterModels;
using Hotel.Web.Interfaces;
using Hotel.Web.VIewModel;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Hotel.Web.Controllers
{
    public class GuestController : Controller
    {
        private IGuestService guestService;

        public GuestController(IGuestService guestService)
        {
            this.guestService = guestService;
        }
        public IActionResult Index(string sortOrder, int? pageNumber, string searchString)
        {
            try
            {

                ViewData["CurrentSort"] = sortOrder;
                ViewData["FirstNameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "fName" : "";
                ViewData["LastNameSortParm"] = sortOrder == "Last Name" ? "lName_desc" : "Last Name";
                ViewData["CurrentFilter"] = searchString;
                int pageSize = 5;
                pageNumber ??= 1;
                var filter = new GuestFilter { Name = searchString, Take = pageSize, Skip = (pageNumber.Value - 1) * pageSize, SortOrder = sortOrder, };
                var (guests, count) = guestService.ReadGuests(filter);
                return View(PaginatedList<GuestViewModel>.Create(guests, count, pageNumber.Value, pageSize));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(GuestViewModel model)
        {
            if (ModelState.IsValid)
            {
                guestService.AddGuest(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            try
            {
                var guest = guestService.ReadSingle(id);
                return View(guest);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                var guest = guestService.ReadSingle(id);
                return View(guest);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Edit(int id, GuestViewModel guest)
        {
            if (ModelState.IsValid)
            {
                guestService.UpdateGuests(id, guest);
                return RedirectToAction(nameof(Index));
            }
            return View(guest);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                var guest = guestService.ReadSingle(id);
                return View(guest);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            guestService.DeleteGuests(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
