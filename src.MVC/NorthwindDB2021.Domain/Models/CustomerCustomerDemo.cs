using System;
using System.Collections.Generic;

namespace NorthwindDB2021.Domain.Models
{
    public partial class CustomerCustomerDemo
    {
        public int CustomerId { get; set; }
        public string CustomerTypeId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual CustomerDemographic CustomerType { get; set; }
    }
}
