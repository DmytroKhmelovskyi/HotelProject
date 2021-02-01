using Hotel.Shared.Interfaces;
using Hotel.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityFrameworkProgect.Services
{
   public class EfPaymentService : IPaymentService
    {
        private readonly HotelDatabaseContext context;
        public EfPaymentService(HotelDatabaseContext _context)
        {
            context = _context;
        }


        public IEnumerable<Payment> ReadPayments()
        {

            IEnumerable<Payment> paymentsList = context.Payments.ToList();
            return paymentsList;
        }
        public Payment AddPayment(Payment payment)
        {
            Payment newPayment = new Payment
            {
                Id = payment.Id,
                GuestId = payment.GuestId,
                ReservationId = payment.ReservationId,
                Amount = payment.Amount,
                PayTime = payment.PayTime
            };

            context.Payments.Add(newPayment);
            context.SaveChanges();
            return newPayment;
        }
        public Payment UpdatePayment(int id, Payment payment)
        {
            var paymentUpdate = context.Payments.SingleOrDefault(p => p.Id == id);
            paymentUpdate.Id = payment.Id;
            paymentUpdate.GuestId = payment.GuestId;
            paymentUpdate.ReservationId = payment.ReservationId;
            paymentUpdate.Amount = payment.Amount;
            paymentUpdate.PayTime = payment.PayTime;

            context.Payments.Update(paymentUpdate);
            context.SaveChanges();
            return paymentUpdate;
        }
        public void DeletePayment(int id)
        {
            Guest guest = context.Guests.Single(g => g.Id == id);
            context.Guests.Remove(guest);
            context.SaveChanges();

        }

    }
}
