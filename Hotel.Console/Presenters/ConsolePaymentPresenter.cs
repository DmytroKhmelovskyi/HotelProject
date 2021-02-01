using Hotel.Shared.Models;
using System;
using System.Collections.Generic;

namespace Hotel.ConsoleApp.Presenters
{
    public static class ConsolePaymentPresenter
    {
        public static void Present(Payment p)
        {
            Console.WriteLine($"{p.Id} \t{p.GuestId} \t{p.ReservationId} \t{p.Amount} \t{p.PayTime} ");

        }


        public static void Present(IEnumerable<Payment> payments)
        {
            foreach (var p in payments)
            {
                Present(p);
            }
        }
    }
}
