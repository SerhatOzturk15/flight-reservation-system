using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WingsOn.Api.Providers;
using WingsOn.Domain;

namespace WingsOn.Api.Controllers
{
    public class PersonController : ApiController
    {

        IPersonProvider _PersonProvider;

        public PersonController()
        {
        }
        public PersonController(IPersonProvider personProvider)
        {
            _PersonProvider = personProvider;
        }

        /// <summary>
        /// returns person by Id
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        [Route("api/Person/GetPersonById")]
        [HttpGet]
        public Person GetPersonById(int personId)
        {
            Person person = _PersonProvider.GetPersonById(personId);
            return person;
        }

        /// <summary>
        /// get passengers by flight Id
        /// </summary>
        /// <param name="flightNumber"></param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Person> GetPassengersByFlightNumber(string flightNumber)
        {
            if (string.IsNullOrEmpty(flightNumber)){
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            IEnumerable<Person> personList = _PersonProvider.GetPassengersByFlightNumber(flightNumber);
            return personList;
        }

        /// <summary>
        /// update a person's mail address
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="newMailAddress"></param>
        /// <returns></returns>
        [HttpPost]
        public bool UpdatePersonsMailAddress(int personId, string newMailAddress)
        {
            if (string.IsNullOrEmpty(newMailAddress))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            bool result = _PersonProvider.UpdatePersonsMailAddress(personId, newMailAddress);
            return result;
        }

        /// <summary>
        /// gets the all male passengers //careful, not all persons are passengers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Person> GetAllMalePassengers()
        {
            IEnumerable<Person> personList = _PersonProvider.GetAllMalePassengers();
            return personList;
        }
    }
}
