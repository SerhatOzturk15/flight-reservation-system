using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using WingsOn.Dal;
using WingsOn.Domain;

namespace WingsOn.Api.Providers
{
    public class PersonProvider : IPersonProvider
    {
        #region interfaces 
        IRepository<Person> _PersonRepository;
        IRepository<Booking> _BookingRepository;
        #endregion

        #region constructors
        //default constructor
        public PersonProvider()
        {
        }
        
        public PersonProvider(IRepository<Person> personRepository)
        {
            _PersonRepository = personRepository;
        }

        public PersonProvider(IRepository<Person> personRepository, IRepository<Booking> bookingRepository)
        {
            _PersonRepository = personRepository;
            _BookingRepository = bookingRepository;
        }
        #endregion
        /// <summary>
        /// get all male passengers
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Person> GetAllMalePassengers()
        {
            IEnumerable<Booking> bookingList = _BookingRepository.GetAll();
            HashSet<Person> passengerSet = new HashSet<Person>();
            foreach (Booking booking in bookingList)
            {
                foreach(Person x in booking.Passengers)
                {
                    if(x.Gender == GenderType.Male)
                    {
                        passengerSet.Add(x);
                    }
                }
            }
            return passengerSet.ToList();
        }

        /// <summary>
        /// get all passengers with flight number
        /// </summary>
        /// <param name="flightNumber"></param>
        /// <returns></returns>
        public IEnumerable<Person> GetPassengersByFlightNumber(string flightNumber)
        {
            IEnumerable<Booking> bookingList = _BookingRepository.GetAll();
            HashSet<Person> passengerSet = new HashSet<Person>();
            foreach (Booking booking in bookingList)
            {
                if(booking.Flight.Number == flightNumber)
                {
                    foreach (Person x in booking.Passengers)
                    {
                            passengerSet.Add(x);
                    }
                }
            }
            return passengerSet.ToList();
        }

        /// <summary>
        /// get person by Id
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public Person GetPersonById(int personId)
        {
            Person person = _PersonRepository.Get(personId);
            return person;
        }

        /// <summary>
        /// update person's mail address
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="newMailAddress"></param>
        /// <returns></returns>
        public bool UpdatePersonsMailAddress(int personId, string newMailAddress)
        {
            //it is better to check mail format in UI part
            Regex r = new Regex(@"[^@]+@[^\.]+\..+");
            Person person = _PersonRepository.Get(personId);
            if(person == null || !r.IsMatch(newMailAddress))
            {
                return false;      // if person is null or mail format is not validated, return false
            }
            person.Email = newMailAddress;
            _PersonRepository.Save(person);
            return true;
        }
    }
}