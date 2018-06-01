using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceFuncApp
{
    class VehicleQuoteRequest
    {
        public String QuoteType { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public int Age { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}
