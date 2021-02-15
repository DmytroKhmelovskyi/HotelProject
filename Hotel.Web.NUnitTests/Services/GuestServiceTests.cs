using Hotel.Shared.Interfaces;
using Hotel.Shared.Models;
using Moq;
using NUnit.Framework;
using Hotel.BL.Models;
using AutoMapper;
using Hotel.BL.Services;
using Hotel.AutoMapperLibrary;
using System;
using Hotel.Shared.FilterModels;
using System.Collections.Generic;

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
        public void AddGuest_Success_CallsRepositoryWithCorrectParameters()
        {
            //Arrenge
            var guestViewModel = new GuestViewModel()
            {
                FirstName = "Misha",
                LastName = "Gonchak",
                City = "Rakovets"
            };
            var guestRepoMock = new Mock<IGuestRepository>();
            guestRepoMock.Setup(r => r.AddGuest(It.IsAny<Guest>())).Returns(new Guest());
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            var guestService = new GuestService(mapper, guestRepoMock.Object);

            //Act
            var result = guestService.AddGuest(guestViewModel);

            //Assert
            guestRepoMock.Verify(r => r.AddGuest(It.Is<Guest>(g => g.FirstName == guestViewModel.FirstName)), Times.Once);
        }

        [Test]
        public void AddGuest_IsNotNull()
        {
            //Arrenge
            var guestViewModel = new GuestViewModel();
            var guestRepoMock = new Mock<IGuestRepository>();
            guestRepoMock.Setup(r => r.AddGuest(It.IsAny<Guest>())).Returns(new Guest());
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            var guestService = new GuestService(mapper, guestRepoMock.Object);

            //Act
            var result = guestService.AddGuest(guestViewModel);

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void DeleteGuest_Success_CallsRepositoryWithCorrectParameters()
        {
            //Arrenge
            var guestViewModel = new GuestViewModel()
            {
                Id = 1
            };
            var guestRepoMock = new Mock<IGuestRepository>();
            guestRepoMock.Setup(r => r.DeleteGuests(It.IsAny<int>())).Returns(new Guest());
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            var guestService = new GuestService(mapper, guestRepoMock.Object);

            //Act
            var result = guestService.DeleteGuests(guestViewModel.Id);

            //Assert
            guestRepoMock.Verify(r => r.DeleteGuests(It.Is<int>(id => id == guestViewModel.Id)), Times.Once);
        }
        [Test]
        public void DeleteGuest_IsNotNull()
        {
            //Arrenge
            var guestViewModel = new GuestViewModel();
            var guestRepoMock = new Mock<IGuestRepository>();
            guestRepoMock.Setup(r => r.DeleteGuests(It.IsAny<int>())).Returns(new Guest());
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            var guestService = new GuestService(mapper, guestRepoMock.Object);

            //Act
            var result = guestService.DeleteGuests(guestViewModel.Id);

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void ReadGuests_Success_ResultIsInstanseOfExpectedTuple()
        {
            //Arrenge
            var filter = new GuestFilter()
            {
                Name = "abc",
                Take = 1,
                Skip = 1

            };
            var guests = GetTestsGuests();
            var guestRepoMock = new Mock<IGuestRepository>();
            guestRepoMock.Setup(r => r.ReadGuests(It.Is<GuestFilter>(f => f.Name == filter.Name && f.Take == filter.Take && f.Skip == filter.Skip)))
                .Returns((guests, guests.Count));
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            var guestService = new GuestService(mapper, guestRepoMock.Object);

            //Act
            var result = guestService.ReadGuests(filter);

            //Assert
            Assert.IsInstanceOf<(IEnumerable<GuestViewModel>, int)>(result);
        }

        [Test]
        public void ReadSingleGuest_Success_CallsRepositoryWithCorrectParameters()
        {
            //Arrenge
            var guestViewModel = new GuestViewModel()
            {
                Id = 1
            };
            var guestRepoMock = new Mock<IGuestRepository>();
            guestRepoMock.Setup(r => r.ReadSingle(It.IsAny<int>())).Returns(new Guest());
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            var guestService = new GuestService(mapper, guestRepoMock.Object);

            //Act
            var result = guestService.ReadSingle(guestViewModel.Id);

            //Assert
            guestRepoMock.Verify(r => r.ReadSingle(It.Is<int>(id => id == guestViewModel.Id)), Times.Once);
        }


        [Test]
        public void ReadSingleGuest_Success_IsNotNull()
        {
            //Arrenge
            var guestViewModel = new GuestViewModel()
            {
                Id = 1
            };
            var guestRepoMock = new Mock<IGuestRepository>();
            guestRepoMock.Setup(r => r.ReadSingle(It.IsAny<int>())).Returns(new Guest());
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            var guestService = new GuestService(mapper, guestRepoMock.Object);

            //Act
            var result = guestService.ReadSingle(guestViewModel.Id);

            //Assert
            Assert.IsNotNull(result);

        }


        [Test]
        public void UpdateGuest_Success_CorrectUpdateWithMapper()
        {
            //Arrenge
            var guest = new Guest()
            {
                Id = 1,
                FirstName = "Misha"
            };
            var guestViewModel = new GuestViewModel()
            {
                Id = 1,
                FirstName = "Misha"
            };
            var guestRepoMock = new Mock<IGuestRepository>();
            guestRepoMock.Setup(r => r.UpdateGuests(guest.Id, It.IsAny<Guest>())).Returns(guest);
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            var guestService = new GuestService(mapper, guestRepoMock.Object);

            //Act
            var result = guestService.UpdateGuests(guestViewModel.Id, guestViewModel);

            //Assert
            guestRepoMock.Verify(r => r.UpdateGuests(guest.Id, It.Is<Guest>(g => g.Id == guestViewModel.Id && g.FirstName == guestViewModel.FirstName)), Times.Once);
        }





        private IList<Guest> GetTestsGuests()
        {
            return new List<Guest>()
            {
                new Guest
                {
                    Id = 1,
                    FirstName = "Oleh",
                    LastName = "Vasiliv",
                    Email = "derdt@ukr.net",
                    Phone = "380964445567",
                    City = "London",
                    Country = "Britain"

                },
                 new Guest
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
