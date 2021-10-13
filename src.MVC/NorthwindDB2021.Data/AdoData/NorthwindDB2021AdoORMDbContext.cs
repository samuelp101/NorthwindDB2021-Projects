using Microsoft.Extensions.Configuration;
using NorthwindDB2021.Core.Models;
using NorthwindDB2021.Data.ORM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace NorthwindDB2021.Data.AdoData
{
  public class NorthwindDB2021AdoORMDbContext
  {
    private readonly string _connectionString;

    public NorthwindDB2021AdoORMDbContext(IConfiguration config)
    {
      _connectionString = config.GetConnectionString("NorthwindDB2021");
    }

    public List<Customer> GetAll()
    {
      var customers = new List<Customer>();
      string sql = "dbo.spNorthwindDB2021_Customers_GetAll";

      try
      {
        customers = customers.MapSqlQueryList<Customer>(_connectionString, sql, CommandType.StoredProcedure);
      }
      catch (Exception)
      {
        throw;
      }
      return customers;
    }

    public async Task<List<Customer>> GetAllAsync()
    {
      var customers = new List<Customer>();
      string sql = "dbo.spNorthwindDB2021_Customers_GetAll";

      try
      {
        customers = await customers.MapSqlQueryListAsync<Customer>(_connectionString, sql, CommandType.StoredProcedure);
      }
      catch (Exception)
      {
        throw;
      }
      return customers;
    }

    public Customer GetById(int? Id)
    {
      var customer = new Customer();
      string sql = "dbo.spNorthwindDB2021_Customers_GetById";

      try
      {
        customer.CustomerId = (int)Id;
        customer = customer.MapSqlQuery<Customer>(_connectionString, sql, CommandType.StoredProcedure);
      }
      catch (Exception)
      {
        throw;
      }
      return customer;
    }

    public async Task<Customer> GetByIdAsync(int? Id)
    {
      var customer = new Customer();
      string sql = "dbo.spNorthwindDB2021_Customers_GetById";

      try
      {
        customer.CustomerId = (int)Id;
        customer = await customer.MapSqlQueryAsync<Customer>(_connectionString, sql, CommandType.StoredProcedure);
      }
      catch (Exception)
      {
        throw;
      }
      return customer;
    }

    public int Create(Customer customer)
    {
      string sql = "dbo.spNorthwindDB2021_Customers_Create";
      int result = 0;

      try
      {
        result = customer.MapSqlQuery<Customer>(_connectionString, sql);
      }
      catch (Exception)
      {
        throw;
      }
      return result;
    }

    public async Task<int> CreateAsync(Customer customer)
    {
      string sql = "dbo.spNorthwindDB2021_Customers_Create";
      int result = 0;

      try
      {
        result = await customer.MapSqlQueryAsync<Customer>(_connectionString, sql);
      }
      catch (Exception)
      {
        throw;
      }
      return result;
    }

    public int Delete(int? Id)
    {
      var customer = new Customer();
      customer.CustomerId = (int)Id;
      string sql = "dbo.spNorthwindDB2021_Customers_Delete";
      int result = 0;

      try
      {
        result = customer.MapSqlQuery<Customer>(_connectionString, sql);
      }
      catch (Exception)
      {
        throw;
      }
      return result;
    }

    public async Task<int> DeleteAsync(int? Id)
    {
      var customer = new Customer();
      customer.CustomerId = (int)Id;
      string sql = "dbo.spNorthwindDB2021_Customers_Delete";
      int result = 0;

      try
      {
        result = await customer.MapSqlQueryAsync<Customer>(_connectionString, sql);
      }
      catch (Exception)
      {
        throw;
      }
      return result;
    }

    public int Update(Customer customer)
    {
      string sql = "dbo.spNorthwindDB2021_Customers_Update";
      int result = 0;

      try
      {
        result = customer.MapSqlQuery<Customer>(_connectionString, sql);
      }
      catch (Exception)
      {
        throw;
      }
      return result;
    }

    public async Task<int> UpdateAsync(Customer customer)
    {
      string sql = "dbo.spNorthwindDB2021_Customers_Update";
      int result = 0;

      try
      {
        result = await customer.MapSqlQueryAsync<Customer>(_connectionString, sql);
      }
      catch (Exception)
      {
        throw;
      }
      return result;
    }

  }
}
