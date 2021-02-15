using AutoMapper;
using Hotel.AutoMapperLibrary;
using Hotel.BL.Models;
using Hotel.BL.Services;
using Hotel.Shared.FilterModels;
using Hotel.Shared.Interfaces;
using Hotel.Shared.Models;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace Hotel.NUnitTests.Services
{
    [TestFixture]
    class PaymentServiceTests
    {
        [Test]
        public void AddPayment_Success_CallsRepositoryWithCorrectParameters()
        {
            //Arrenge
            var paymentViewModel = new PaymentViewModel()
            {
                GuestId = 3,
                Amount = 243,
            };
            var paymentRepoMock = new Mock<IPaymentRepository>();
            paymentRepoMock.Setup(r => r.AddPayment(It.IsAny<Payment>())).Returns(new Payment());
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            var paymentService = new PaymentService(paymentRepoMock.Object, mapper);

            //Act
            var result = paymentService.AddPayment(paymentViewModel);

            //Assert
            paymentRepoMock.Verify(r => r.AddPayment(It.Is<Payment>(g => g.Amount == paymentViewModel.Amount)), Times.Once);
        }

        [Test]
        public void AddPayment_IsNotNull()
        {
            //Arrenge
            var paymentViewMidel = new PaymentViewModel();
            var paymentRepoMock = new Mock<IPaymentRepository>();
            paymentRepoMock.Setup(r => r.AddPayment(It.IsAny<Payment>())).Returns(new Payment());
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            var paymentService = new PaymentService(paymentRepoMock.Object, mapper);

            //Act
            var result = paymentService.AddPayment(paymentViewMidel);

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void DeletePayment_Success_CallsRepositoryWithCorrectParameters()
        {
            //Arrenge
            var paymentViewModel = new PaymentViewModel()
            {
                Id = 1
            };
            var paymentRepoMock = new Mock<IPaymentRepository>();
            paymentRepoMock.Setup(r => r.DeletePayment(It.IsAny<int>())).Returns(new Payment());
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            var paymentService = new PaymentService(paymentRepoMock.Object, mapper);

            //Act
            var result = paymentService.DeletePayment(paymentViewModel.Id);

            //Assert
            paymentRepoMock.Verify(r => r.DeletePayment(It.Is<int>(id => id == paymentViewModel.Id)), Times.Once);
        }
        [Test]
        public void DeletePayment_IsNotNull()
        {
            //Arrenge
            var paymentViewModel = new PaymentViewModel();
            var paymentRepoMock = new Mock<IPaymentRepository>();
            paymentRepoMock.Setup(r => r.DeletePayment(It.IsAny<int>())).Returns(new Payment());
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            var paymentService = new PaymentService(paymentRepoMock.Object, mapper);

            //Act
            var result = paymentService.DeletePayment(paymentViewModel.Id);

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void ReadGuests_Success_ResultIsInstanseOfExpectedTuple()
        {
            //Arrenge
            var filter = new PaymentFilter();
            var payments = GetTestsPayments();
            var paymentRepoMock = new Mock<IPaymentRepository>();
            paymentRepoMock.Setup(r => r.ReadPayments(It.Is<PaymentFilter>(f => f.Amount == filter.Amount && f.Take == filter.Take && f.Skip == filter.Skip && f.PayTime == filter.PayTime)))
                .Returns((payments, payments.Count));
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            var paymentService = new PaymentService(paymentRepoMock.Object, mapper);

            //Act
            var result = paymentService.ReadPayments(filter);

            //Assert
            Assert.IsInstanceOf<(IEnumerable<PaymentViewModel>, int)>(result);
        }

        [Test]
        public void ReadSingleGuest_Success_CallsRepositoryWithCorrectParameters()
        {
            //Arrenge
            var paymentViewModel = new PaymentViewModel()
            {
                Id = 1
            };
            var paymentRepoMock = new Mock<IPaymentRepository>();
            paymentRepoMock.Setup(r => r.ReadSingle(It.IsAny<int>())).Returns(new Payment());
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            var paymentService = new PaymentService(paymentRepoMock.Object, mapper);

            //Act
            var result = paymentService.ReadSingle(paymentViewModel.Id);


            //Assert
            paymentRepoMock.Verify(r => r.ReadSingle(It.Is<int>(id => id == paymentViewModel.Id)), Times.Once);
        }


        [Test]
        public void ReadSingleGuest_Success_IsNotNull()
        {
            var paymentViewModel = new PaymentViewModel()
            {
                Id = 1
            };
            var paymentRepoMock = new Mock<IPaymentRepository>();
            paymentRepoMock.Setup(r => r.ReadSingle(It.IsAny<int>())).Returns(new Payment());
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            var paymentService = new PaymentService(paymentRepoMock.Object, mapper);

            //Act
            var result = paymentService.ReadSingle(paymentViewModel.Id);

            //Assert
            Assert.IsNotNull(result);

        }


        [Test]
        public void UpdateGuest_Success_CorrectUpdateWithMapper()
        {
            //Arrenge
            var payment = new Payment()
            {
                Id = 1,
                Amount = 33
            };
            var paymentViewModel = new PaymentViewModel()
            {
                Id = 1,
                Amount = 33
            };
            var paymentRepoMock = new Mock<IPaymentRepository>();
            paymentRepoMock.Setup(r => r.UpdatePayment(payment.Id, It.IsAny<Payment>())).Returns(payment);
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            var paymentService = new PaymentService(paymentRepoMock.Object, mapper);

            //Act
            var result = paymentService.UpdatePayment(paymentViewModel.Id, paymentViewModel);

            //Assert
            paymentRepoMock.Verify(r => r.UpdatePayment(payment.Id, It.Is<Payment>(g => g.Id == paymentViewModel.Id && g.Amount == paymentViewModel.Amount)), Times.Once);
        }





        private IList<Payment> GetTestsPayments()
        {
            return new List<Payment>()
            {
                new Payment
                {
                    Id = 1,
                     Amount = 4456,
                    GuestId = 3

                },
                 new Payment
                {
                    Id = 2,
                     Amount = 3456,
                    GuestId = 4

                }

             };
        }

    }
}
