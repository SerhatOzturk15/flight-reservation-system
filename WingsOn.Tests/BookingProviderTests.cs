using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WingsOn.Api.Providers;
using System.Globalization;
using WingsOn.Dal;
using WingsOn.Domain;
using Moq;
using System.Linq;

namespace WingsOn.Tests
{
    [TestClass]
    public class BookingProviderTests
    {
        IPersonProvider personProvider;
        IBookingProvider bookingProvider;
        CultureInfo cultureInfo = new CultureInfo("nl-NL");
        public BookingProviderTests()
        {
            Person person = new Person()
            {
                Id = 12,
                Name = "SERHAT",
                Gender = GenderType.Male,
                Address = "adsad",
                DateBirth = DateTime.Parse("24/09/1980"),
                Email = "adasd@hotmail.com"
            };
            var mockPersonRepo = new Mock<IRepository<Person>>();
            var mockbookingRepo = new Mock<IRepository<Booking>>();
            mockPersonRepo.Setup(foo => foo.Get(It.IsAny<int>())).Returns(person);
            mockPersonRepo.Setup(foo => foo.Get(250)).Returns((Person)null);
            bookingProvider = new BookingProvider(mockbookingRepo.Object,mockPersonRepo.Object);
        }


        /// <summary>
        /// true flight and passenger
        /// </summary>
        [TestMethod]
        public void BookAFlightTest()
        {
            PersonRepository personRepo = new PersonRepository();
            FlightRepository flights = new FlightRepository();
            Booking booking = new Booking()
            {
                Id = 60,
                Number = "WO-291470",
                Customer = personRepo.GetAll().Single(p => p.Name == "Branden Johnston"),
                DateBooking = DateTime.Parse("03/03/2006 14:30", cultureInfo),
                Flight = flights.GetAll().Single(f => f.Number == "BB768"),
                Passengers = new[]
                    {
                        personRepo.GetAll().Single(p => p.Name == "Branden Johnston")
                    }
            };
            Booking result = bookingProvider.BookAFlight(30, 77);
            Assert.AreEqual(result.Customer.Id, 12);
            Assert.AreEqual(result.Flight.Id, 30);
        }

        /// <summary>
        /// true flight and wrong passenger
        /// </summary>
        [TestMethod]
        public void BookAFlightWrongPassengerTest()
        {
            PersonRepository personRepo = new PersonRepository();
            FlightRepository flights = new FlightRepository();
            Booking booking = new Booking()
            {
                Id = 60,
                Number = "WO-291470",
                Customer = personRepo.GetAll().Single(p => p.Name == "Branden Johnston"),
                DateBooking = DateTime.Parse("03/03/2006 14:30", cultureInfo),
                Flight = flights.GetAll().Single(f => f.Number == "BB768"),
                Passengers = new[]
                    {
                        personRepo.GetAll().Single(p => p.Name == "Branden Johnston")
                    }
            };
            Booking result = bookingProvider.BookAFlight(30, 250);
            Assert.IsNull(result.Customer);
            Assert.AreEqual(result.Flight.Id, 30);
        }

        /// <summary>
        /// wrong flight and true passenger
        /// </summary>
        [TestMethod]
        public void BookAFlightWrongFlightTest()
        {
            PersonRepository personRepo = new PersonRepository();
            FlightRepository flights = new FlightRepository();
            Booking booking = new Booking()
            {
                Id = 60,
                Number = "WO-291470",
                Customer = personRepo.GetAll().Single(p => p.Name == "Branden Johnston"),
                DateBooking = DateTime.Parse("03/03/2006 14:30", cultureInfo),
                Flight = flights.GetAll().Single(f => f.Number == "BB768"),
                Passengers = new[]
                    {
                        personRepo.GetAll().Single(p => p.Name == "Branden Johnston")
                    }
            };
            Booking result = bookingProvider.BookAFlight(100, 77);
            Assert.AreEqual(result.Customer.Id, 12);
            Assert.IsNull(result.Flight);
        }
    }
}
