using Hotel.BL.Interfaces;
using Hotel.Shared.FilterModels;
using Microsoft.AspNetCore.Mvc;
using System;
using Hotel.BL.Services;
using Hotel.BL.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hotel.Web.Controllers
{
    public class ReservationController : Controller
    {
        private IReservationService reservationService;
        private IGuestService guestService;
        private IRoomService roomService;
        public ReservationController(IReservationService reservationService, IGuestService guestService, IRoomService roomService)
        {
            this.reservationService = reservationService;
            this.guestService = guestService;
            this.roomService = roomService;
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
            var guests = guestService.ReadGuests();
            var guestItems = guests.Select(g => new SelectListItem { Value = $"{g.Id}", Text = $"{g.FirstName} {g.LastName}" });
            ViewBag.Guests = guestItems;

            var rooms = roomService.ReadRooms();
            var roomItems = rooms.Select(r => new SelectListItem { Value = $"{r.Id}", Text = $"room status: {r.RoomStatusName} room number: {r.RoomNumber}" });
            ViewBag.Rooms = roomItems;

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
                var guests = guestService.ReadGuests();
                var guestItems = guests.Select(g => new SelectListItem { Value = $"{g.Id}", Text = $"{g.FirstName} {g.LastName}" });
                ViewBag.Guests = guestItems;

                var rooms = roomService.ReadRooms();
                var roomItems = rooms.Select(r => new SelectListItem { Value = $"{r.Id}", Text = $"{r.Id} ({r.RoomStatusName}) room number:{r.RoomNumber}" });
                ViewBag.Rooms = roomItems;

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
