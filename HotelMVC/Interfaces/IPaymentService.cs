using Hotel.Shared.FilterModels;
using Hotel.Web.VIewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Web.Interfaces
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
