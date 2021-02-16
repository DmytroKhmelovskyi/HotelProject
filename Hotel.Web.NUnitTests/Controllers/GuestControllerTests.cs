using Hotel.BL.Interfaces;
using Hotel.BL.Models;
using Hotel.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Hotel.Shared.FilterModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Hotel.BL.Services;

namespace Hotel.Web.NUnitTests
{
    [TestFixture]
    public class GuestControllerTests
    {
        [SetUp]
        public void Setup()
        {

        }
        [Test]
        public void ModelValidation_ModelIsValid_ReturnFalse()
        {
            var guestModel = new GuestViewModel()
            {
                FirstName = "a",

            };
            var context = new ValidationContext(guestModel);
            var res = new List<ValidationResult>();
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(GuestViewModel), typeof(GuestViewModel)), typeof(GuestViewModel));

            var isModelStateValid = Validator.TryValidateObject(guestModel, context, res);
            Assert.IsFalse(isModelStateValid);
        }
        [Test]
        public void ModelValidation_ModelIsValid_ReturnTrue()
        {
            var guestModel = new GuestViewModel()
            {
                FirstName = "Aytda",
                LastName = "Sdfienr"
            };
            var context = new ValidationContext(guestModel);
            var res = new List<ValidationResult>();
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(GuestViewModel), typeof(GuestViewModel)), typeof(GuestViewModel));

            var isModelStateValid = Validator.TryValidateObject(guestModel, context, res);
            Assert.IsTrue(isModelStateValid);
        }
        [Test]
        public void Index_IsNotNull_ViewResultIsNotNull()
        {
            // arrange
            int pageNumber = 1;
            string searchString = "";
            var filter = new GuestFilter();
            var mock = new Mock<IGuestService>();
            GuestController controller = new GuestController(mock.Object);
            // act
            var result = controller.Index(searchString, pageNumber, filter);
            //assert
            var res = result as ViewResult;
            Assert.IsNotNull(res);
        }

        [Test]
        public void Index_CallsReadGuestService_WithFilter()
        {
            //Arrange
            var filter = new GuestFilter()
            {
                Name = "abc",
                Take = 1,
                Skip = 1

            };
            var guests = GetTestsGuests();
            var guestService = new Mock<IGuestService>();
            guestService.Setup(s => s.ReadGuests(It.Is<GuestFilter>(f => f.Name == filter.Name && f.Take == filter.Take && f.Skip == filter.Skip)))
                .Returns((guests, guests.Count));
            var controller = new GuestController(guestService.Object);

            // Act
            var result = controller.Index("", null, filter);

            //Assert

            guestService.Verify(s => s.ReadGuests(filter), Times.Once);
        }

        [Test]
        public void Create__NotNull_ViewResultIsNotNull()
        {
            //Arrange
            var guest = new GuestViewModel();
            var mock = new Mock<IGuestService>();
            mock.Setup(g => g.AddGuest(guest));
            var controller = new GuestController(mock.Object);
            //Act
            ViewResult result = controller.Create() as ViewResult;


            Assert.IsNotNull(result);
        }


        [Test]
        public void Create_Success_ReturnsARedirectToActionResut()
        {
            // Arrange
            var testGuest = new GuestViewModel();
            var guestsRepo = new Mock<IGuestService>();
            guestsRepo.Setup(g => g.AddGuest(It.IsAny<GuestViewModel>()));
            var controller = new GuestController(guestsRepo.Object);

            // Act
            var result = controller.Create(testGuest);

            // Assert
            var redirectToActionResult = result as RedirectToActionResult;
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }

        [Test]
        public void Create_Error_ReturnsAViewResut()
        {
            // Arrange
            var testGuest = new GuestViewModel();
            var guestsRepo = new Mock<IGuestService>();
            guestsRepo.Setup(g => g.AddGuest(It.IsAny<GuestViewModel>()));
            var controller = new GuestController(guestsRepo.Object);
            controller.ModelState.AddModelError("", "");

            // Act
            var result = controller.Create(testGuest);

            // Assert

            Assert.That(result, Is.InstanceOf<ViewResult>());
        }


        [Test]
        public void Details_GuestExists_ReturnsAViewResultWithGuest()
        {
            //Arrange
            var guestId = 1;
            var testGuest = new GuestViewModel() { Id = guestId };
            var guestsRepo = new Mock<IGuestService>();
            guestsRepo.Setup(g => g.ReadSingle(guestId)).Returns(testGuest);
            var controller = new GuestController(guestsRepo.Object);

            // Act
            var result = controller.Details(guestId);

            // Assert
            var viewResult = result as ViewResult;
            var model = viewResult.ViewData.Model as GuestViewModel;
            Assert.AreEqual(guestId, model.Id);
        }

        [Test]
        public void Details_GuestDoesNotExist_ReturnsNotFoundResults()
        {
            // Arrange
            var guestsRepo = new Mock<IGuestService>();
            guestsRepo.Setup(g => g.ReadSingle(It.IsAny<int>())).Throws(It.IsAny<Exception>());
            var controller = new GuestController(guestsRepo.Object);

            // Act
            var result = controller.Details(123);
            // Assert
            Assert.That(result, Is.TypeOf<BadRequestResult>());

        }

        [Test]
        public void Details__NotNull_ViewResultIsNotNull()
        {
            //Arrange
            var guest = new GuestViewModel();
            var mock = new Mock<IGuestService>();
            mock.Setup(g => g.ReadSingle(guest.Id));
            var controller = new GuestController(mock.Object);
            //Act
            ViewResult result = controller.Details(guest.Id) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void Edit_GuestExists_ReturnsAViewResultWithGuest()
        {
            // Arrange
            var guestId = 1;
            var testGuest = new GuestViewModel() { Id = guestId };
            var guestsRepo = new Mock<IGuestService>();
            guestsRepo.Setup(g => g.ReadSingle(guestId)).Returns(testGuest);
            var controller = new GuestController(guestsRepo.Object);

            // Act
            var result = controller.Edit(guestId);

            // Assert
            var viewResult = result as ViewResult;
            var model = viewResult.ViewData.Model as GuestViewModel;
            Assert.AreEqual(guestId, model.Id);
        }

        [Test]
        public void Edit_Success_ReturnsARedirectToActionResut()
        {
            // Arrange
            var guestId = 1;
            var testGuest = new GuestViewModel() { Id = guestId };
            var guestsRepo = new Mock<IGuestService>();
            guestsRepo.Setup(g => g.UpdateGuests(guestId, It.IsAny<GuestViewModel>()));
            var controller = new GuestController(guestsRepo.Object);

            // Act
            var result = controller.Edit(guestId, testGuest);

            // Assert
            var redirectToActionResult = result as RedirectToActionResult;
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }

        [Test]
        public void Edit_GuestDoesNotExistPost_ReturnsABadRequestResults()
        {
            // Arrange
            var guestId = 1;
            var testGuest = new GuestViewModel() { Id = guestId };
            var guestsRepo = new Mock<IGuestService>();
            guestsRepo.Setup(g => g.ReadSingle(It.IsAny<int>())).Throws(It.IsAny<Exception>());
            var controller = new GuestController(guestsRepo.Object);

            // Act
            var result = controller.Edit(guestId);

            // Assert
            Assert.That(result, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public void Edit_NotNull_ViewResultIsNotNull()
        {
            //Arrange
            var guest = new GuestViewModel()
            {
                Id = 1,
                FirstName = "Petro",
                LastName = "Petrov"
            };
            var mock = new Mock<IGuestService>();
            mock.Setup(g => g.UpdateGuests(guest.Id, guest));
            var controller = new GuestController(mock.Object);
            //Act
            var result = controller.Edit(guest.Id, guest);

            //Assert
            Assert.IsNotNull(result);
        }


        [Test]
        public void Delete_Success_ReturnsARedirectToActionResut()
        {
            // Arrange
            var guestId = 1;
            var testGuest = new GuestViewModel() { Id = guestId };
            var guestsRepo = new Mock<IGuestService>();
            guestsRepo.Setup(g => g.DeleteGuests(guestId));
            var controller = new GuestController(guestsRepo.Object);

            // Act
            var result = controller.DeleteConfirmed(guestId);

            // Assert
            var redirectToActionResult = result as RedirectToActionResult;
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }

        [Test]
        public void Delete_Error_ReturnsBadRequest()
        {
            // Arrange
            var guestId = 1;
            var testGuest = new GuestViewModel() { Id = guestId };
            var guestsRepo = new Mock<IGuestService>();
            guestsRepo.Setup(g => g.ReadSingle(guestId)).Throws(It.IsAny<Exception>());
            var controller = new GuestController(guestsRepo.Object);

            // Act
            var result = controller.Delete(guestId);

            // Assert
            Assert.That(result, Is.InstanceOf<BadRequestResult>());

        }
        [Test]
        public void Delete_Success_ReturnsViewResult()
        {
            // Arrange
            var guestId = 1;
            var testGuest = new GuestViewModel() { Id = guestId };
            var guestsRepo = new Mock<IGuestService>();
            guestsRepo.Setup(g => g.ReadSingle(guestId)).Returns(It.IsAny<GuestViewModel>());
            var controller = new GuestController(guestsRepo.Object);

            // Act
            var result = controller.Delete(guestId);

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());

        }

        [Test]
        public void Delete_NotNull_ResultIsNotNull()
        {
            //Arrange
            var guest = new GuestViewModel();
            var mock = new Mock<IGuestService>();
            mock.Setup(g => g.DeleteGuests(guest.Id));
            var controller = new GuestController(mock.Object);
            //Act
            ViewResult result = controller.Delete(guest.Id) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }



        private IList<GuestViewModel> GetTestsGuests()
        {
            return new List<GuestViewModel>()
            {
                new GuestViewModel
                {
                    Id = 1,
                    FirstName = "Oleh",
                    LastName = "Vasiliv",
                    Email = "derdt@ukr.net",
                    Phone = "380964445567",
                    City = "London",
                    Country = "Britain"

                },
                 new GuestViewModel
                {
                    Id = 1,
                    FirstName = "Petro",
                    LastName = "Semaniv",
                    Email = "seman@ukr.net",
                    Phone = "380964567667",
                    City = "Minsk",
                    Country = "Belarus"

                }
            };
        }
    }
}