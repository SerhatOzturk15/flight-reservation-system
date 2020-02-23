using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WingsOn.Domain;

namespace WingsOn.Api.Providers
{
    /// <summary>
    /// Person provider
    /// </summary>
    public interface IPersonProvider
    {
        Person GetPersonById(int personId);
        IEnumerable<Person> GetPassengersByFlightNumber(string flightNumber);
        bool UpdatePersonsMailAddress(int personId, string newMailAddress);
        IEnumerable<Person> GetAllMalePassengers();
    }
}
