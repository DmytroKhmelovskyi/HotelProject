using Hotel.Shared.FilterModels;
using System.Collections.Generic;
using Hotel.BL.Models;

namespace Hotel.BL.Interfaces
{
    public interface IPaymentService
    {
        IEnumerable<PaymentViewModel> ReadPayments();
        (IEnumerable<PaymentViewModel> models, int count) ReadPayments(PaymentFilter filter);

        PaymentViewModel AddPayment(PaymentViewModel model);
        PaymentViewModel UpdatePayment(int id, PaymentViewModel model);
        PaymentViewModel DeletePayment(int id);
        PaymentViewModel ReadSingle(int? id);
    }
}
