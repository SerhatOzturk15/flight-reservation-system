using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WingsOn.Api.Models
{
    public class BookingParametersModel
    {
        /// <summary>
        ///  Booking Id
        /// </summary>
        public int RandomBookingId { get; set; }

        /// <summary>
        /// Booking Number
        /// </summary>
        public string RandomBookingNumber { get; set; }
    }
}