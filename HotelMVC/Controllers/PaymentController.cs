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
    public class PaymentController : Controller
    {
        private IPaymentService paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            this.paymentService = paymentService;
        }
        public IActionResult Index(int? pageNumber, PaymentFilter paymentFilter)
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
        [HttpGet]
        public IActionResult Create()
        {
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
            var payment = paymentService.ReadSingle(id);
            if (payment == null)
            {
                return NotFound();
            }
            return View(payment);
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
