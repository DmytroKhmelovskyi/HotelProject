using AutoMapper;
using Hotel.BL.Interfaces;
using Hotel.Shared.FilterModels;
using Hotel.Shared.Interfaces;
using Hotel.Shared.Models;
using System.Collections.Generic;
using Hotel.BL.Models;
namespace Hotel.BL.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository paymentRepository;
        private readonly IMapper mapper;
        public PaymentService(IPaymentRepository paymentRepository, IMapper mapper)
        {
            this.paymentRepository = paymentRepository;
            this.mapper = mapper;
        }
        public PaymentViewModel AddPayment(PaymentViewModel model)
        {
            var paymentModel = mapper.Map<Payment>(model);
            var payment = paymentRepository.AddPayment(paymentModel);
            return mapper.Map<PaymentViewModel>(payment);
        }

        public PaymentViewModel DeletePayment(int id)
        {
            var payment = paymentRepository.DeletePayment(id);
            return mapper.Map<PaymentViewModel>(payment);
        }

        public IEnumerable<PaymentViewModel> ReadPayments()
        {
            var payment = paymentRepository.ReadPayments();
            return mapper.Map<IEnumerable<PaymentViewModel>>(payment);
        }
        public (IEnumerable<PaymentViewModel> models, int count) ReadPayments(PaymentFilter filter)
        {
            var (payments, count) = paymentRepository.ReadPayments(filter);
            var paymentModel = mapper.Map<IEnumerable<PaymentViewModel>>(payments);
            return (paymentModel, count);
        }

        public PaymentViewModel ReadSingle(int? id)
        {
            var payment = paymentRepository.ReadSingle(id);
            return mapper.Map<PaymentViewModel>(payment);
        }

        public PaymentViewModel UpdatePayment(int id, PaymentViewModel model)
        {
            var paymentModel = mapper.Map<Payment>(model);
            var payment = paymentRepository.UpdatePayment(id, paymentModel);
            return mapper.Map<PaymentViewModel>(payment);
        }
    }
}
