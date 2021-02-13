using AutoMapper;
using Hotel.Shared.Interfaces;
using Hotel.Shared.Models;
using Hotel.Web.VIewModel;
using Moq;
using NUnit.Framework;

namespace Hotel.Web.NUnitTests
{
    [TestFixture]
    public class GuestServiceTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
             public void AddGuest_Success_ReturnGuestViewModelCorrectly()
        {
            var guest = new Guest();
            var guestViewModel = new GuestViewModel();
            var mock = new Mock<IGuestRepository>();
        }


    }
}
