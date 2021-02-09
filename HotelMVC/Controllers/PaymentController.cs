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
        public IActionResult Index(string sortOrder, int? pageNumber)
        {

            ViewData["CurrentSort"] = sortOrder;
            ViewData["AmountSortParm"] = String.IsNullOrEmpty(sortOrder) ? "amount" : "";
            ViewData["PayTimeSortParm"] = sortOrder == "PayTime" ? "payTime" : "PayTime";
            int pageSize = 5;
            pageNumber ??= 1;
            var filter = new PaymentFilter {Take = pageSize, Skip = (pageNumber.Value - 1) * pageSize, SortOrder = sortOrder, };
            var (payments, count) = paymentService.ReadPayments(filter);
            return View(PaginatedList<PaymentViewModel>.Create(payments, count, pageNumber.Value, pageSize));
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
