using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel.Shared.Interfaces;
using Hotel.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelMVC.Controllers
{
    public class PaymentController : Controller
    {
        private IPaymentService paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            this.paymentService = paymentService;
        }
        public IActionResult Index(string sortOrder, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["AmountSortParm"] = String.IsNullOrEmpty(sortOrder) ? "amount" : "";
            ViewData["PayTimeSortParm"] = sortOrder == "PayTime" ? "payTime" : "PayTime";
            var payments = paymentService.ReadPayments();

            switch (sortOrder)
            {
                case "amount":
                    payments = payments.OrderBy(g => g.Amount);
                    break;
                case "payTime":
                    payments = payments.OrderBy(g => g.PayTime);
                    break;
                default:
                    payments = payments.OrderBy(g => g.Id);
                    break;
            }


            int pageSize = 5;
            return View(PaginatedList<Payment>.Create(payments, pageNumber ?? 1, pageSize));
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("Id,GuestId,ReservationId,Amount,PayTime")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                paymentService.AddPayment(payment);
                return RedirectToAction(nameof(Index));
            }
            return View(payment);
        }
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = paymentService.ReadSingle(id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = paymentService.ReadSingle(id);
            if (payment == null)
            {
                return NotFound();
            }
            return View(payment);
        }

        [HttpPost]
        public IActionResult Edit(int id, [Bind("Id,FirstName,LastName,Email,Phone,City,Country,ReservationsCount")] Payment payment)
        {
            if (id != payment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                paymentService.UpdatePayment(id, payment);
                return RedirectToAction(nameof(Index));
            }
            return View(payment);
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var payment = paymentService.ReadSingle(id);
            if (payment == null)
            {
                return NotFound();
            }
            return View(payment);

        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            paymentService.DeletePayment(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
