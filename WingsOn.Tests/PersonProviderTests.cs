using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WingsOn.Api.Providers;
using WingsOn.Domain;
using WingsOn.Dal;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace WingsOn.Tests
{


    [TestClass]
    public class PersonProviderTests
    {
        #region global test variables
        PersonRepository personRepo = new PersonRepository();
        BookingRepository bookingRepo = new BookingRepository();
        FlightRepository flights = new FlightRepository();
        IPersonProvider personProvider;
        CultureInfo cultureInfo = new CultureInfo("nl-NL"); 
        List<Booking> bookingList;
        Person person;
        public PersonProviderTests()
        {
            PersonRepository personRepo = new PersonRepository();
            bookingList = new List<Booking>();
            bookingList.AddRange(new[]
           {
                new Booking
                {
                    Id = 55,
                    Number = "WO-291470",
                    Customer = personRepo.GetAll().Single(p => p.Name == "Branden Johnston"),
                    DateBooking = DateTime.Parse("03/03/2006 14:30", cultureInfo),
                    Flight = flights.GetAll().Single(f => f.Number == "BB768"),
                    Passengers = new []
                    {
                        personRepo.GetAll().Single(p => p.Name == "Branden Johnston")
                    }
                },
                new Booking
                {
                    Id = 83,
                    Number = "WO-151277",
                    Customer = personRepo.GetAll().Single(p => p.Name == "Debra Lang"),
                    DateBooking = DateTime.Parse("12/02/2000 12:55", cultureInfo),
                    Flight = flights.GetAll().Single(f => f.Number == "PZ696"),
                    Passengers = new []
                    {
                        personRepo.GetAll().Single(p => p.Name == "Claire Stephens"),
                        personRepo.GetAll().Single(p => p.Name == "Kendall Velazquez"),
                        personRepo.GetAll().Single(p => p.Name == "Zenia Stout")
                    }
                }
            });
            person = new Person()
            {
                Id = 12,
                Name = "SERHAT",
                Gender = GenderType.Male,
                Address = "adsad",
                DateBirth = DateTime.Parse("24/09/1980"),
                Email = "adasd@hotmail.com"
            };
            //mockings

            var mockPersonRepo = new Mock<IRepository<Person>>();
            mockPersonRepo.Setup(foo => foo.Get(It.IsAny<int>())).Returns(person);
            mockPersonRepo.Setup(foo => foo.Get(100)).Returns((Person)null);
            var mockBookingRepo = new Mock<IRepository<Booking>>();
            mockBookingRepo.Setup(foo => foo.GetAll()).Returns(bookingList);

            personProvider = new PersonProvider(mockPersonRepo.Object, mockBookingRepo.Object);
        }

        #endregion
        /// <summary>
        /// GetPersonById // there are not many cases to test
        /// </summary>
        [TestMethod]
        public void GetPersonByIdTest()
        {
            Person personResult1 = personProvider.GetPersonById(1);
            Assert.AreEqual(person.Name, personResult1.Name);
        }

        /// <summary>
        /// male passengers test
        /// </summary>
        [TestMethod]
        public void GetMalePassengersTest()
        {
            var expectedResult = 2;
            List<Person> resultList = personProvider.GetAllMalePassengers().ToList();
            Assert.AreEqual(expectedResult, resultList.Count);
        }

        /// <summary>
        /// test for getting passengers by flight number
        /// </summary>
        [TestMethod]
        public void GetPassengersByFlightNumberTest()
        {
            var expectedResult = 3;
            List<Person> personList = personProvider.GetPassengersByFlightNumber("PZ696").ToList();
            Assert.AreEqual(expectedResult, personList.Count);
        }

        /// <summary>
        /// wrong mail address provided 
        /// </summary>
        [TestMethod]
        public void UpdatePersonsMailAddressTest1()
        {
            bool expected = false;
            bool result = personProvider.UpdatePersonsMailAddress(20, "abcdasdsad");
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// true mail address is provided
        /// </summary>
        [TestMethod]
        public void UpdatePersonsMailAddressTest2()
        {
            bool expected = true;
            bool result = personProvider.UpdatePersonsMailAddress(20, "abcdasdsad@hotmail.com");
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// true mail address is provided but null will return from person repo
        /// </summary>
        [TestMethod]
        public void UpdatePersonsMailAddressTest3()
        {
            bool expected = false;
            bool result = personProvider.UpdatePersonsMailAddress(100, "abcdasdsad@hotmail.com");
            Assert.AreEqual(expected, result);
        }
    }
}
