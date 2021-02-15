using Hotel.BL.Interfaces;
using Hotel.Shared.FilterModels;
using Microsoft.AspNetCore.Mvc;
using System;
using Hotel.BL.Services;
using Hotel.BL.Models;

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
            try
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
            try
            {
                var reservation = reservationService.ReadSingle(id);
                return View(reservation);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            try
            {
                var reservation = reservationService.ReadSingle(id);
                return View(reservation);
            }
            catch (Exception)
            {
                return BadRequest();
            }
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
            try
            {
                var reservation = reservationService.ReadSingle(id);
                return View(reservation);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            reservationService.DeleteReservation(id);
            return RedirectToAction(nameof(Index));
        }


    }
}
