using Microsoft.Data.SqlClient;
using NorthwindDB2021.Core.Models;

namespace NorthwindDB2021.Data.ORM
{
    public static class RawORM
    {
        public static Customer MapSqlDataReaderRaw(this Customer customer, SqlDataReader reader)
        {
            customer.CustomerId = (int)reader["CustomerId"];
            customer.CompanyName = reader["CompanyName"].ToString();
            customer.ContactName = reader["ContactName"].ToString();
            customer.ContactTitle = reader["ContactTitle"].ToString();
            customer.Address = reader["Address"].ToString();
            customer.City = reader["City"].ToString();
            customer.Region = reader["Region"].ToString();
            customer.PostalCode = reader["PostalCode"].ToString();
            customer.Country = reader["Country"].ToString();

            return customer;
        }
    }
}
