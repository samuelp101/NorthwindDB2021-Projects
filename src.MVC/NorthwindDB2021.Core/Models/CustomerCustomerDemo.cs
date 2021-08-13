namespace NorthwindDB2021.Core.Models
{
    public partial class CustomerCustomerDemo
    {
        public int CustomerId { get; set; }
        public string CustomerTypeId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual CustomerDemographic CustomerType { get; set; }
    }
}
