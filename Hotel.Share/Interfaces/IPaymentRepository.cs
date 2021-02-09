using Hotel.Shared.FilterModels;
using Hotel.Shared.Models;
using System.Collections.Generic;

namespace Hotel.Shared.Interfaces
{
    public interface IPaymentRepository
    {
        IEnumerable<Payment> ReadPayments();

        Payment AddPayment(Payment guest);
        Payment UpdatePayment(int id, Payment guest);
        Payment DeletePayment(int id);
        Payment ReadSingle(int? id);
        (IEnumerable<Payment> payments, int count) ReadPayments(PaymentFilter filter);
    }
}
