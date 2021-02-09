using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel.Shared.FilterModels;
using Hotel.Shared.Interfaces;
using Hotel.Shared.Models;
using Hotel.Web.Interfaces;
using Hotel.Web.VIewModel;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Web.Controllers
{
    public class ReservationController : Controller
    {
        private IReservationService reservationService;
        public ReservationController(IReservationService reservationService)
        {
            this.reservationService = reservationService;
        }
        public IActionResult Index(string sortOrder, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["CheckInDateSortParm"] = String.IsNullOrEmpty(sortOrder) ? "CheckInDate" : "";
            ViewData["CheckOutDateSortParm"] = sortOrder == "CheckOutDate" ? "CheckOutDate" : "CheckOutDate";
            int pageSize = 5;
            pageNumber ??= 1;
            var filter = new ReservationFilter
            {
                Take = pageSize,
                Skip = (pageNumber.Value - 1) * pageSize,
                SortOrder = sortOrder,
            };
            var (reservations, count) = reservationService.ReadReservations(filter);
            return View(PaginatedList<ReservationViewModel>.Create(reservations, count, pageNumber.Value, pageSize));
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ReservationViewModel model)
        {
            if (ModelState.IsValid)
            {
                reservationService.AddReservation(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Details(int? id)
        {

            var reservation = reservationService.ReadSingle(id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var reservation = reservationService.ReadSingle(id);
            if (reservation == null)
            {
                return NotFound();
            }
            return View(reservation);
        }

        [HttpPost]
        public IActionResult Edit(int id, ReservationViewModel model)
        {
            if (ModelState.IsValid)
            {
                reservationService.UpdateReservation(id, model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            var reservation = reservationService.ReadSingle(id);
            if (reservation == null)
            {
                return NotFound();
            }
            return View(reservation);

        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            reservationService.DeleteReservation(id);
            return RedirectToAction(nameof(Index));
        }


    }
}
