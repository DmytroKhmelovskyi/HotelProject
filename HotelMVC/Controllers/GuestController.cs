using Hotel.Shared.Interfaces;
using Hotel.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace HotelMVC.Controllers
{
    public class GuestController : Controller
    {
        private IGuestService guestService;
        public GuestController(IGuestService guestService)
        {
            this.guestService = guestService;
        }
        public IActionResult Index(string sortOrder, int? pageNumber, string searchString, string currentFilter)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["FirstNameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "fName" : "";
            ViewData["LastNameSortParm"] = sortOrder == "Last Name" ? "lName_desc" : "Last Name";
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;
            var guests = guestService.ReadGuests();
            if (!String.IsNullOrEmpty(searchString))
            {
                guests = guests.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "fName":
                    guests = guests.OrderBy(g => g.FirstName);
                    break;
                case "lName_desc":
                    guests = guests.OrderByDescending(g => g.LastName);
                    break;
                default:
                    guests = guests.OrderBy(g => g.Id);
                    break;
            }

            int pageSize = 5;

            return View(PaginatedList<Guest>.Create(guests, pageNumber ?? 1, pageSize));
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("Id,FirstName,LastName,Email,Phone,City,Country,ReservationsCount")] Guest guest)
        {
            if (ModelState.IsValid)
            {
                guestService.AddGuest(guest);
                return RedirectToAction(nameof(Index));
            }
            return View(guest);
        }
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guest = guestService.ReadSingle(id);
            if (guest == null)
            {
                return NotFound();
            }

            return View(guest);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guest = guestService.ReadSingle(id);
            if (guest == null)
            {
                return NotFound();
            }
            return View(guest);
        }

        [HttpPost]
        public IActionResult Edit(int id, [Bind("Id,FirstName,LastName,Email,Phone,City,Country,ReservationsCount")] Guest guest)
        {
            if (id != guest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //try
                //{
                    guestService.UpdateGuests(id, guest);
                //}
                //catch (Exception)
                //{
                //    throw new Exception("Something goes wrong");
                //}
                return RedirectToAction(nameof(Index));
            }
            return View(guest);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var guest = guestService.ReadSingle(id);
            if (guest == null)
            {
                return NotFound();
            }
            return View(guest);

        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            guestService.DeleteGuests(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
