using System;
using System.Collections.Generic;

namespace NorthwindDB2021.Domain.Models
{
    public partial class QuarterlyOrder
    {
        public int? CustomerId { get; set; }
        public string CompanyName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
