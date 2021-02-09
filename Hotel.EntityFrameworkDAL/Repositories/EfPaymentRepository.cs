

using Hotel.Shared.FilterModels;
using Hotel.Shared.Interfaces;
using Hotel.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hotel.EntityFrameworkDAL.Repositories
{
    public class EfPaymentRepository : IPaymentRepository
    {
        private readonly HotelDatabaseContext context;
        public EfPaymentRepository(HotelDatabaseContext _context)
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
            paymentUpdate.GuestId = payment.GuestId;
            paymentUpdate.ReservationId = payment.ReservationId;
            paymentUpdate.Amount = payment.Amount;
            paymentUpdate.PayTime = payment.PayTime;

            context.Payments.Update(paymentUpdate);
            context.SaveChanges();
            return paymentUpdate;
        }
        public Payment DeletePayment(int id)
        {
            Payment payment = context.Payments.Single(g => g.Id == id);
            context.Payments.Remove(payment);
            context.SaveChanges();
            return payment;

        }

        public Payment ReadSingle(int? id)
        {
            var payment = context.Payments.SingleOrDefault(p => p.Id == id);
            return payment;
        }

        public (IEnumerable<Payment>, int) ReadPayments(PaymentFilter filter)
        {
            var orderBy = "";
            switch (filter.SortOrder)
            {
                case "amount":
                    orderBy = "Amount";
                    break;
                case "payTime":
                    orderBy = "PayTime";
                    break;
                default:
                    orderBy = "Id";
                    break;
            }
            IEnumerable<Payment> paymentsList = context.Payments.ToList();
            IEnumerable<Guest> guestsList = context.Guests.ToList();
            var query = from payment in paymentsList
                        join guest in guestsList
                        on (payment.Guest.FirstName, payment.Guest.LastName)
                        equals (guest.FirstName, guest.LastName)
                        orderby orderBy
                        select (payment.GuestId, payment.ReservationId, payment.Amount, payment.PayTime, guest.FirstName,
                       guest.LastName);

            paymentsList.Skip(filter.Skip)
                .Take(filter.Take);
            var count = paymentsList.Count();
            return(paymentsList, count);

        }
    }
}
