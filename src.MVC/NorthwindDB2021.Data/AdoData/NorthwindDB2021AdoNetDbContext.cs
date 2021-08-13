using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NorthwindDB2021.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindDB2021.Data.AdoData
{
    public class NorthwindDB2021AdoNetDbContext
    {
        private readonly string _connectionString;

        public NorthwindDB2021AdoNetDbContext(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("NorthwindDB2021");
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            var customers = new List<Customer>();
            string sql = "Select * From dbo.Customers";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;
                        await connection.OpenAsync();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (await reader.ReadAsync())
                            {
                                var customer = new Customer();
                                customer.CustomerId = (int)reader["CustomerId"];
                                customer.CompanyName = reader["CompanyName"].ToString();
                                customer.ContactName = reader["ContactName"].ToString();
                                customer.ContactTitle = reader["ContactTitle"].ToString();
                                customer.Address = reader["Address"].ToString();
                                customer.City = reader["City"].ToString();
                                customer.Region = reader["Region"].ToString();
                                customer.PostalCode = reader["PostalCode"].ToString();
                                customer.Country = reader["Country"].ToString();

                                customers.Add(customer);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return customers;
        }

        public async Task<Customer> GetByIdAsync(int? Id)
        {
            var customer = new Customer();
            //string sql = "Select * From dbo.Customers Where CustomerId = @Id";
            string sql = "dbo.spNorthwindDB2021_Customers_GetById";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        //command.CommandType = CommandType.Text;
                        //command.Parameters.AddWithValue("Id", Id);

                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("CustomerId", Id);

                        await connection.OpenAsync();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (await reader.ReadAsync())
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
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return customer;
        }

        public async Task<bool> CreateAsync(Customer customer)
        {
            string sql = "dbo.spNorthwindDB2021_Customers_Create";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        //command.Parameters.AddWithValue("CustomerId", customer.CustomerId);
                        command.Parameters.AddWithValue("CompanyName", customer.CompanyName);
                        command.Parameters.AddWithValue("ContactName", customer.ContactName);
                        command.Parameters.AddWithValue("ContactTitle", customer.ContactTitle);
                        command.Parameters.AddWithValue("Address", customer.Address);
                        command.Parameters.AddWithValue("City", customer.City);
                        command.Parameters.AddWithValue("Region", customer.Region);
                        command.Parameters.AddWithValue("PostalCode", customer.PostalCode);
                        command.Parameters.AddWithValue("Country", customer.Country);

                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return true;
        }

        public async Task<bool> DeleteAsync(int? Id)
        {
            string sql = "dbo.spNorthwindDB2021_Customers_Delete";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("CustomerId", Id);

                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
            return true;
        }

        public async Task<bool> UpdateAsync(Customer customer)
        {
            string sql = "dbo.spNorthwindDB2021_Customers_Update";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("CustomerId", customer.CustomerId);
                        command.Parameters.AddWithValue("CompanyName", customer.CompanyName);
                        command.Parameters.AddWithValue("ContactName", customer.ContactName);
                        command.Parameters.AddWithValue("ContactTitle", customer.ContactTitle);
                        command.Parameters.AddWithValue("Address", customer.Address);
                        command.Parameters.AddWithValue("City", customer.City);
                        command.Parameters.AddWithValue("Region", customer.Region);
                        command.Parameters.AddWithValue("PostalCode", customer.PostalCode);
                        command.Parameters.AddWithValue("Country", customer.Country);

                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return true;
        }

    }
}
