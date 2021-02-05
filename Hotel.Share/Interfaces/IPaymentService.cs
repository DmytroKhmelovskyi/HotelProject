using Hotel.Shared.Models;
using System.Collections.Generic;

namespace Hotel.Shared.Interfaces
{
    public interface IPaymentService
    {
        IEnumerable<Payment> ReadPayments();

        Payment AddPayment(Payment guest);
        Payment UpdatePayment(int id, Payment guest);
        void DeletePayment(int id);
        Payment ReadSingle(int? id);
    }
}
