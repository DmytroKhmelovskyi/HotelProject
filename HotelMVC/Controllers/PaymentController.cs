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
        public IActionResult Index(int? pageNumber)
        {
            var payments = paymentService.ReadPayments();
            int pageSize = 5;
            return View(PaginatedList<Payment>.Create(payments, pageNumber ?? 1, pageSize));
        }
    }
}
