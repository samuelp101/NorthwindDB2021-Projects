using Microsoft.EntityFrameworkCore;
using NorthwindDB2021.Data.EFData;
using NorthwindDB2021.Data.Interfaces;
using NorthwindDB2021.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindDB2021.Data.DataServices
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly NorthwindDB2021DbContext _dataContext;

        public CustomersRepository(NorthwindDB2021DbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool Delete(Customer obj)
        {
            try
            {
                _dataContext.Customers.Remove(obj);
                _dataContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Customer obj)
        {
            try
            {
                _dataContext.Customers.Remove(obj);
                await _dataContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Customer> GetAll()
        {
            List<Customer> customers = new List<Customer>();
            try
            {
                customers = _dataContext.Customers.ToList();
                return customers;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            List<Customer> customers = new List<Customer>();
            try
            {
                customers = await _dataContext.Customers.ToListAsync();
                return customers;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Customer GetById(int? id)
        {
            try
            {
                var customer = _dataContext.Customers.FirstOrDefault(c => c.CustomerId == id);
                return customer;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Customer> GetByIdAsync(int? id)
        {
            try
            {
                var customer = await _dataContext.Customers.FirstOrDefaultAsync(c => c.CustomerId == id);
                return customer;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Insert(Customer obj)
        {
            try
            {
                _dataContext.Customers.Add(obj);
                _dataContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> InsertAsync(Customer obj)
        {
            try
            {
                await _dataContext.Customers.AddAsync(obj);
                await _dataContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Update(Customer obj)
        {
            try
            {
                _dataContext.Customers.Update(obj);
                _dataContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateAsync(Customer obj)
        {
            try
            {
                _dataContext.Customers.Update(obj);
                await _dataContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
