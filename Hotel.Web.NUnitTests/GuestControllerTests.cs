using Hotel.Shared.FilterModels;
using Hotel.Web.Controllers;
using Hotel.Web.Interfaces;
using Hotel.Web.VIewModel;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

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
        public void Index_IsNotNull_ViewResultIsNotNull()
        {
            // arrange
            string sortOrder = "";
            int pageNumber = 1;
            string searchString = "";
            var mock = new Mock<IGuestService>();
            GuestController controller = new GuestController(mock.Object);
            // act
            var result = controller.Index(sortOrder, pageNumber, searchString);
            //assert
            var res = result as ViewResult;
            Assert.IsNotNull(res);
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
        public void Edit_GuestExists_ReturnsAViewResultWithGuest()
        {
            // Arrange
            var guestId = 1;
            var testGuest = new GuestViewModel() { Id = guestId };
            var guestsRepo = new Mock<IGuestService>();
            guestsRepo.Setup(g => g.UpdateGuests(guestId, testGuest));
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
            guestsRepo.Setup(g => g.UpdateGuests(guestId, It.IsAny<GuestViewModel>())).Throws(It.IsAny<Exception>());
            var controller = new GuestController(guestsRepo.Object);

            // Act
            var result = controller.Edit(guestId, testGuest);

            // Assert
            Assert.That(result, Is.TypeOf<BadRequestResult>());
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