using Hotel.BL.Interfaces;
using Hotel.Shared.FilterModels;
using Microsoft.AspNetCore.Mvc;
using Hotel.BL.Models;
using System;
using Hotel.BL.Services;

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
