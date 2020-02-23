using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WingsOn.Domain;

namespace WingsOn.Api.Providers
{
    /// <summary>
    /// Booking provider
    /// </summary>
    public interface IBookingProvider
    {
        Booking BookAFlight(int flightId, int personId);
    }
}
