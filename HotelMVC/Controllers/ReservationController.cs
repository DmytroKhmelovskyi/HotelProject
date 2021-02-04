using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel.Shared.Interfaces;
using Hotel.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelMVC.Controllers
{
    public class ReservationController : Controller
    {
        private IReservationService reservationService;
        public ReservationController(IReservationService reservationService)
        {
            this.reservationService = reservationService;
        }
        public IActionResult Index(int? pageNumber)
        {
            var reservations = reservationService.ReadReservations();
            int pageSize = 5;
            return View(PaginatedList<Reservation>.Create(reservations, pageNumber ?? 1, pageSize));

        }
    }
}
