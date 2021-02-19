using Hotel.BL.Interfaces;
using Hotel.Shared.FilterModels;
using Microsoft.AspNetCore.Mvc;
using System;
using Hotel.BL.Models;
using Hotel.BL.Services;

namespace Hotel.Web.Controllers
{
    public class GuestController : Controller
    {
        private IGuestService guestService;

        public GuestController(IGuestService guestService)
        {
            this.guestService = guestService;
        }
        public IActionResult Index(string searchString, int? pageNumber, GuestFilter guestFilter)
        {
            try
            {

                ViewData["CurrentSort"] = guestFilter.SortOrder;
                ViewData["FirstNameSortParm"] = String.IsNullOrEmpty(guestFilter.SortOrder) ? "fName" : "";
                ViewData["LastNameSortParm"] = guestFilter.SortOrder == "Last Name" ? "lName_desc" : "Last Name";
                ViewData["CurrentFilter"] = searchString;
                guestFilter.Name = searchString;
                guestFilter.Take = 5;
                pageNumber ??= 1;
                guestFilter.Skip = (pageNumber.Value - 1) * guestFilter.Take;
                var (guests, count) = guestService.ReadGuests(guestFilter);
                return View(PaginatedList<GuestViewModel>.Create(guests, count, pageNumber.Value, guestFilter.Take));
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
