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
    public class BookingController : ApiController
    {

        IBookingProvider _BookingProvider;
    
        public BookingController(IBookingProvider bookingProvider)
        {
            this._BookingProvider = bookingProvider;
        }

        /// <summary>
        ///  book an existing flight     //Assumption: the passenger is already defined in person repository  //problematic
        ///                              // another assumption(stated in the question actually), only one passenger.
        /// </summary>                  
        /// <param name="flightId"></param>
        /// <param name="personId"></param>
        /// <returns></returns>
        [HttpPost]
        public Booking BookAFlight(int flightId, int personId)
        {
            return _BookingProvider.BookAFlight(flightId, personId);
        }
    }
}
