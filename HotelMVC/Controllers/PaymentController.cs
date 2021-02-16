using Hotel.BL.Interfaces;
using Hotel.Shared.FilterModels;
using Microsoft.AspNetCore.Mvc;
using Hotel.BL.Models;
using System;
using Hotel.BL.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Collections.Generic;

namespace Hotel.Web.Controllers
{
    public class PaymentController : Controller
    {
        private IPaymentService paymentService;
        private IGuestService guestService;
        private IReservationService reservationService;
        public PaymentController(IPaymentService paymentService, IGuestService guestService, IReservationService reservationService)
        {
            this.paymentService = paymentService;
            this.guestService = guestService;
            this.reservationService = reservationService;
        }
        public IActionResult Index(int? pageNumber, PaymentFilter paymentFilter)
        {
            try
            {

                ViewData["CurrentSort"] = paymentFilter.SortOrder;
                ViewData["AmountSortParm"] = String.IsNullOrEmpty(paymentFilter.SortOrder) ? "amount" : "";
                ViewData["PayTimeSortParm"] = paymentFilter.SortOrder == "PayTime" ? "payTime" : "PayTime";
                paymentFilter.Take = 5;
                pageNumber ??= 1;
                paymentFilter.Skip = (pageNumber.Value - 1) * paymentFilter.Take;
                var (payments, count) = paymentService.ReadPayments(paymentFilter);
                return View(PaginatedList<PaymentViewModel>.Create(payments, count, pageNumber.Value, paymentFilter.Take));

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

            var reservations = reservationService.ReadReservations();
            var reservationItems = reservations.Select(r => new SelectListItem { Value = $"{r.Id}", Text = $"{r.GuestName} ({r.ReservationDate}) room: {r.RoomId}" });
            ViewBag.Reservations = reservationItems;

            return View();
        }

        [HttpPost]
        public IActionResult Create(PaymentViewModel model)
        {
            if (ModelState.IsValid)
            {
                paymentService.AddPayment(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Details(int? id)
        {
            try
            {
                var payment = paymentService.ReadSingle(id);
                return View(payment);
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

                var reservations = reservationService.ReadReservations();
                var reservationItems = reservations.Select(r => new SelectListItem { Value = $"{r.Id}", Text = $"{r.GuestName} ({r.ReservationDate}) room:{r.RoomId}" });
                ViewBag.Reservations = reservationItems;
                var payment = paymentService.ReadSingle(id);
                return View(payment);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Edit(int id, PaymentViewModel model)
        {
            if (ModelState.IsValid)
            {
                paymentService.UpdatePayment(id, model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            try
            {
                var payment = paymentService.ReadSingle(id);
                return View(payment);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            paymentService.DeletePayment(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
