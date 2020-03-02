using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WingsOn.Api.Providers;
using System.Globalization;
using WingsOn.Dal;
using WingsOn.Domain;
using Moq;
using System.Linq;
using WingsOn.Api.Controllers;
using System.Collections.Generic;
using System.Web.Http;

namespace WingsOn.Tests
{
    [TestClass]
    public class ControllerTests
    {

        /// <summary>
        /// empty flight number test
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(HttpResponseException), "Flight number cannot be empty string. It will throw exception")]
        public void PersonControllerEmptyFlightNumberTest()
        {
            PersonController cnt = new PersonController();
            List<Person> personList = cnt.GetPassengersByFlightNumber(string.Empty).ToList();
        }

        /// <summary>
        /// null flight number test
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(HttpResponseException), "Flight number cannot be empty string. It will throw exception")]
        public void PersonControllerNullFlightNumberTest()
        {
            PersonController cnt = new PersonController();
            List<Person> personList = cnt.GetPassengersByFlightNumber(null).ToList();
        }

        /// <summary>
        /// empty mail address test
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(HttpResponseException), "Flight number cannot be empty string. It will throw exception")]
        public void PersonControllerMailAddressEmptyTest()
        {
            PersonController cnt = new PersonController();
            bool result = cnt.UpdatePersonsMailAddress(10, string.Empty);
        }

        /// <summary>
        /// null mail address test
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(HttpResponseException), "Flight number cannot be empty string. It will throw exception")]
        public void PersonControllernMailAddressNullTest()
        {
            PersonController cnt = new PersonController();
            bool result = cnt.UpdatePersonsMailAddress(10, null);
        }

    }
}
