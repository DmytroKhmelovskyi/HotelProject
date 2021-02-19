using Hotel.BL.Interfaces;
using Hotel.BL.Models;
using Hotel.Shared.FilterModels;
using Hotel.Shared.Models;
using Hotel.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Hotel.NUnitTests.Controllers
{
    [TestFixture]
    public class PaymentControllerTests
    {
        [SetUp]
        public void Setup()
        {

        }
        [Test]
        public void ModelValidation_ModelIsValid_ReturnFalse()
        {
            //Arrenge
            var paymentModel = new PaymentViewModel()
            {
                Amount = 1000000000000
            };
            var context = new ValidationContext(paymentModel);
            var res = new List<ValidationResult>();
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(PaymentViewModel), typeof(PaymentViewModel)), typeof(PaymentViewModel));

            //Act
            var isModelStateValid = Validator.TryValidateObject(paymentModel, context, res);

            //Assert
            Assert.IsFalse(isModelStateValid);
        }
        [Test]
        public void ModelValidation_ModelIsValid_ReturnTrue()
        {
            //Arrenge
            var paymentModel = new PaymentViewModel()
            {
                Id = 25,
                GuestId = 7,
                Amount = 100

            };
            var context = new ValidationContext(paymentModel);
            var res = new List<ValidationResult>();
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(PaymentViewModel), typeof(PaymentViewModel)), typeof(PaymentViewModel));

            //Act
            var isModelStateValid = Validator.TryValidateObject(paymentModel, context, res);

            //Assert
            Assert.IsTrue(isModelStateValid);
        }

        [Test]
        public void Index_IsNotNull_ViewResultIsNotNull()
        {
            // arrange
            int pageNumber = 1;
            var filter = new PaymentFilter();
            var paymentsRepo = new Mock<IPaymentService>();
            var guestsRepo = new Mock<IGuestService>();
            var reservationsRepo = new Mock<IReservationService>();
            var controller = new PaymentController(paymentsRepo.Object, guestsRepo.Object, reservationsRepo.Object);
            // act
            var result = controller.Index(pageNumber, filter);
            //assert
            var res = result as ViewResult;
            Assert.IsNotNull(res);
        }

        [Test]
        public void Index_CallsReadPaymentService_WithFilter()
        {
            //Arrange
            var filter = new PaymentFilter()
            {
                Amount = 324,
                Take = 1,
                Skip = 1

            };
            var payments = GetTestsPayments();
            var paymentsRepo = new Mock<IPaymentService>();
            var guestsRepo = new Mock<IGuestService>();
            var reservationsRepo = new Mock<IReservationService>();
            paymentsRepo.Setup(s => s.ReadPayments(It.Is<PaymentFilter>(f => f.Amount == filter.Amount && f.Take == filter.Take && f.Skip == filter.Skip && f.PayTime == filter.PayTime)))
                .Returns((payments, payments.Count));
            var controller = new PaymentController(paymentsRepo.Object, guestsRepo.Object, reservationsRepo.Object);

            // Act
            var result = controller.Index(null, filter);

            //Assert

            paymentsRepo.Verify(s => s.ReadPayments(filter), Times.Once);




        }


        [Test]
        public void Create__NotNull_ViewResultIsNotNull()
        {
            //Arrange
            var payment = new PaymentViewModel();
            var paymentsRepo = new Mock<IPaymentService>();
            var guestsRepo = new Mock<IGuestService>();
            var reservationsRepo = new Mock<IReservationService>();
            paymentsRepo.Setup(g => g.AddPayment(payment));
            var controller = new PaymentController(paymentsRepo.Object, guestsRepo.Object, reservationsRepo.Object);
            //Act
            ViewResult result = controller.Create() as ViewResult;


            Assert.IsNotNull(result);
        }


        [Test]
        public void Create_Success_ReturnsARedirectToActionResut()
        {
            // Arrange
            var testPayment = new PaymentViewModel();
            var paymentsRepo = new Mock<IPaymentService>();
            var guestsRepo = new Mock<IGuestService>();
            var reservationsRepo = new Mock<IReservationService>();
            paymentsRepo.Setup(g => g.AddPayment(It.IsAny<PaymentViewModel>()));
            var controller = new PaymentController(paymentsRepo.Object, guestsRepo.Object, reservationsRepo.Object);

            // Act
            var result = controller.Create(testPayment);

            // Assert
            var redirectToActionResult = result as RedirectToActionResult;
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }

        [Test]
        public void Create_Error_ReturnsAViewResut()
        {
            // Arrange
            var testPayment = new PaymentViewModel();
            var paymentsRepo = new Mock<IPaymentService>();
            var guestsRepo = new Mock<IGuestService>();
            var reservationsRepo = new Mock<IReservationService>();
            paymentsRepo.Setup(g => g.AddPayment(It.IsAny<PaymentViewModel>()));
            var controller = new PaymentController(paymentsRepo.Object, guestsRepo.Object, reservationsRepo.Object);
            controller.ModelState.AddModelError("", "");

            // Act
            var result = controller.Create(testPayment);

            // Assert

            Assert.That(result, Is.InstanceOf<ViewResult>());
        }


        [Test]
        public void Details_PaymentExists_ReturnsAViewResultWithPayment()
        {
            //Arrange
            var paymentId = 1;
            var testPayment = new PaymentViewModel() { Id = paymentId };
            var paymentsRepo = new Mock<IPaymentService>();
            var guestsRepo = new Mock<IGuestService>();
            var reservationsRepo = new Mock<IReservationService>();
            paymentsRepo.Setup(g => g.ReadSingle(paymentId)).Returns(testPayment);
            var controller = new PaymentController(paymentsRepo.Object, guestsRepo.Object, reservationsRepo.Object);

            // Act
            var result = controller.Details(paymentId);

            // Assert
            var viewResult = result as ViewResult;
            var model = viewResult.ViewData.Model as PaymentViewModel;
            Assert.AreEqual(paymentId, model.Id);
        }

        [Test]
        public void Details_PaymentDoesNotExist_ReturnsNotFoundResults()
        {
            // Arrange
            var paymentsRepo = new Mock<IPaymentService>();
            var guestsRepo = new Mock<IGuestService>();
            var reservationsRepo = new Mock<IReservationService>();
            paymentsRepo.Setup(g => g.ReadSingle(It.IsAny<int>())).Throws(It.IsAny<Exception>());
            var controller = new PaymentController(paymentsRepo.Object, guestsRepo.Object, reservationsRepo.Object);

            // Act
            var result = controller.Details(123);
            // Assert
            Assert.That(result, Is.TypeOf<BadRequestResult>());

        }

        [Test]
        public void Details__NotNull_ViewResultIsNotNull()
        {
            //Arrange
            var payment = new PaymentViewModel();
            var paymentsRepo = new Mock<IPaymentService>();
            var guestsRepo = new Mock<IGuestService>();
            var reservationsRepo = new Mock<IReservationService>();
            paymentsRepo.Setup(g => g.ReadSingle(payment.Id));
            var controller = new PaymentController(paymentsRepo.Object, guestsRepo.Object, reservationsRepo.Object);
            //Act
            ViewResult result = controller.Details(payment.Id) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void Edit_PaymentExists_ReturnsAViewResultWithPayment()
        {
            // Arrange
            var paymentId = 1;
            var testPayment = new PaymentViewModel() { Id = paymentId };
            var paymentsRepo = new Mock<IPaymentService>();
            var guestsRepo = new Mock<IGuestService>();
            var reservationsRepo = new Mock<IReservationService>();
            paymentsRepo.Setup(g => g.ReadSingle(paymentId)).Returns(testPayment);
            var controller = new PaymentController(paymentsRepo.Object, guestsRepo.Object, reservationsRepo.Object);

            // Act
            var result = controller.Edit(paymentId);

            // Assert
            var viewResult = result as ViewResult;
            var model = viewResult.ViewData.Model as PaymentViewModel;
            Assert.AreEqual(paymentId, model.Id);
        }

        [Test]
        public void Edit_Success_ReturnsARedirectToActionResut()
        {
            // Arrange
            var paymentId = 1;
            var testPayment = new PaymentViewModel() { Id = paymentId };
            var paymentsRepo = new Mock<IPaymentService>();
            var guestsRepo = new Mock<IGuestService>();
            var reservationsRepo = new Mock<IReservationService>();
            paymentsRepo.Setup(g => g.UpdatePayment(paymentId, It.IsAny<PaymentViewModel>()));
            var controller = new PaymentController(paymentsRepo.Object, guestsRepo.Object, reservationsRepo.Object);

            // Act
            var result = controller.Edit(paymentId, testPayment);

            // Assert
            var redirectToActionResult = result as RedirectToActionResult;
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }

        [Test]
        public void Edit_PaymentDoesNotExistPost_ReturnsABadRequestResults()
        {
            // Arrange
            var paymentId = 1;
            var testPayment = new PaymentViewModel() { Id = paymentId };
            var paymentsRepo = new Mock<IPaymentService>();
            var guestsRepo = new Mock<IGuestService>();
            var reservationsRepo = new Mock<IReservationService>();
            paymentsRepo.Setup(g => g.ReadSingle(It.IsAny<int>())).Throws(It.IsAny<Exception>());
            var controller = new PaymentController(paymentsRepo.Object, guestsRepo.Object, reservationsRepo.Object);

            // Act
            var result = controller.Edit(paymentId);

            // Assert
            Assert.That(result, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public void Edit_NotNull_ViewResultIsNotNull()
        {
            //Arrange
            var payment = new PaymentViewModel();
            var paymentsRepo = new Mock<IPaymentService>();
            var guestsRepo = new Mock<IGuestService>();
            var reservationsRepo = new Mock<IReservationService>();
            paymentsRepo.Setup(g => g.UpdatePayment(payment.Id, payment));
            var controller = new PaymentController(paymentsRepo.Object, guestsRepo.Object, reservationsRepo.Object);
            //Act
            var result = controller.Edit(payment.Id, payment);

            //Assert
            Assert.IsNotNull(result);
        }


        [Test]
        public void Delete_Success_ReturnsARedirectToActionResut()
        {
            // Arrange
            var paymentId = 1;
            var testPayment = new PaymentViewModel() { Id = paymentId };
            var paymentsRepo = new Mock<IPaymentService>();
            var guestsRepo = new Mock<IGuestService>();
            var reservationsRepo = new Mock<IReservationService>();
            paymentsRepo.Setup(g => g.DeletePayment(paymentId));
            var controller = new PaymentController(paymentsRepo.Object, guestsRepo.Object, reservationsRepo.Object);

            // Act
            var result = controller.DeleteConfirmed(paymentId);

            // Assert
            var redirectToActionResult = result as RedirectToActionResult;
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }

        [Test]
        public void Delete_Error_ReturnsBadRequest()
        {
            // Arrange
            var paymentId = 1;
            var testPayment = new PaymentViewModel() { Id = paymentId };
            var paymentsRepo = new Mock<IPaymentService>();
            var guestsRepo = new Mock<IGuestService>();
            var reservationsRepo = new Mock<IReservationService>();
            paymentsRepo.Setup(g => g.ReadSingle(paymentId)).Throws(It.IsAny<Exception>());
            var controller = new PaymentController(paymentsRepo.Object, guestsRepo.Object, reservationsRepo.Object);

            // Act
            var result = controller.Delete(paymentId);

            // Assert
            Assert.That(result, Is.InstanceOf<BadRequestResult>());

        }
        [Test]
        public void Delete_Success_ReturnsViewResult()
        {
            // Arrange
            var paymentId = 1;
            var testPayment = new PaymentViewModel() { Id = paymentId };
            var paymentsRepo = new Mock<IPaymentService>();
            var guestsRepo = new Mock<IGuestService>();
            var reservationsRepo = new Mock<IReservationService>();
            paymentsRepo.Setup(g => g.ReadSingle(paymentId)).Returns(It.IsAny<PaymentViewModel>());
            var controller = new PaymentController(paymentsRepo.Object, guestsRepo.Object, reservationsRepo.Object);

            // Act
            var result = controller.Delete(paymentId);

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());

        }

        [Test]
        public void Delete_NotNull_ResultIsNotNull()
        {
            //Arrange
            var payment = new PaymentViewModel();
            var paymentsRepo = new Mock<IPaymentService>();
            var guestsRepo = new Mock<IGuestService>();
            var reservationsRepo = new Mock<IReservationService>();
            paymentsRepo.Setup(g => g.DeletePayment(payment.Id));
            var controller = new PaymentController(paymentsRepo.Object, guestsRepo.Object, reservationsRepo.Object);
            //Act
            ViewResult result = controller.Delete(payment.Id) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }



        private IList<PaymentViewModel> GetTestsPayments()
        {
            return new List<PaymentViewModel>()
            {
                new PaymentViewModel
                {
                    Id = 1,
                     Amount = 4456,
                    GuestId = 3

                },
                 new PaymentViewModel
                {
                    Id = 2,
                     Amount = 3456,
                    GuestId = 4

                }

             };
        }
    }
}
