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
        public IActionResult Index(int? pageNumber, ReservationFilter reservationFilter)
        {
            ViewData["CurrentSort"] = reservationFilter.SortOrder;
            ViewData["CheckInDateSortParm"] = String.IsNullOrEmpty(reservationFilter.SortOrder) ? "CheckInDate" : "";
            ViewData["CheckOutDateSortParm"] = reservationFilter.SortOrder == "CheckOutDate" ? "CheckOutDate" : "CheckOutDate";
            reservationFilter.Take = 5;
            pageNumber ??= 1;
            reservationFilter.Skip = (pageNumber.Value - 1) * reservationFilter.Take;
            var (reservations, count) = reservationService.ReadReservations(reservationFilter);
            return View(PaginatedList<ReservationViewModel>.Create(reservations, count, pageNumber.Value, reservationFilter.Take));
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
