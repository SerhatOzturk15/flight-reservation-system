using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WingsOn.Api.Models;
using WingsOn.Dal;
using WingsOn.Domain;

namespace WingsOn.Api.Providers
{
    public class BookingProvider : IBookingProvider
    {
        const int MINUMUM_FLIGHT_ID = 10;
        const int MAXIMUM_FLIGHT_ID = 99;
        const int MINUMUM_FLIGHT_NUMBER = 100000;
        const int MAXIMUM_FLIGHT_NUMBER = 999999;

        IRepository<Booking> _BookingRepository;
        IRepository<Person> _PersonRepository;

        //default constructor
        public BookingProvider()
        {
        }
        public BookingProvider(IRepository<Booking> bookingRepository, IRepository<Person> personRepository)
        {
            _BookingRepository = bookingRepository;
            _PersonRepository = personRepository;
        }

        /// <summary>
        /// book a flight for an existing flight.
        /// </summary>
        /// <param name="flightId"></param>
        /// <param name="personId"></param>
        /// <returns></returns>
        public Booking BookAFlight(int flightId, int personId)
        {
            BookingParametersModel bookingParametersModel = GetBookingNumberAndId();
            Booking booking = new Booking()
            {
                Id = bookingParametersModel.RandomBookingId,
                Number = "WO-" + bookingParametersModel.RandomBookingNumber,
                DateBooking = DateTime.Now,
                Flight = new FlightRepository().Get(flightId),
                Customer = _PersonRepository.Get(personId),
                Passengers = new List<Person>() { _PersonRepository.Get(personId) }
            };
            _BookingRepository.Save(booking);
            return booking;
        }

        /// <summary>
        /// this method produces random numbers for both bookingId and bookingNumber.
        /// </summary>
        /// <returns></returns>
        private BookingParametersModel GetBookingNumberAndId()
        {
            BookingParametersModel bookingParametersModel = new BookingParametersModel();
            HashSet<string> bookingNumberSet = new HashSet<string>();   // set is created to prevent duplicate creation of booking number.
            HashSet<int> bookingIdSet = new HashSet<int>();             // set is created to prevent duplicate creation of booking Id.
            IEnumerable<Booking> bookingList = _BookingRepository.GetAll();
            foreach (Booking aBooking in bookingList)
            {
                bookingNumberSet.Add(aBooking.Number);
                bookingIdSet.Add(aBooking.Id);
            }
            //get random number for booking Id    
            Random generator = new Random();
            bookingParametersModel.RandomBookingNumber = generator.Next(MINUMUM_FLIGHT_NUMBER, MAXIMUM_FLIGHT_NUMBER).ToString(); //assumption: 6 characters
            while (bookingNumberSet.Contains(bookingParametersModel.RandomBookingNumber))
            {
                bookingParametersModel.RandomBookingNumber = generator.Next(MINUMUM_FLIGHT_NUMBER, MAXIMUM_FLIGHT_NUMBER).ToString();
            }

            //get random number for booking number
            bookingParametersModel.RandomBookingId = generator.Next(MINUMUM_FLIGHT_ID, MAXIMUM_FLIGHT_ID);  //assumption: 2 characters and they are unique
            while (bookingIdSet.Contains(bookingParametersModel.RandomBookingId))
            {
                bookingParametersModel.RandomBookingId = generator.Next(MINUMUM_FLIGHT_ID, MAXIMUM_FLIGHT_ID);
            }
            return bookingParametersModel;
        }
    }
}