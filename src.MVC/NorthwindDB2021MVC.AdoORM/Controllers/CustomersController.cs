using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindDB2021.Core.Models;
using NorthwindDB2021.Data.AdoData;
using System.Threading.Tasks;

namespace NorthwindDB2021MVC.AdoORM.Controllers
{
    public class CustomersController : Controller
    {
        private readonly NorthwindDB2021AdoORMDbContext _dataContext;

        public CustomersController(NorthwindDB2021AdoORMDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            //return View(_dataContext.GetAll());
            return View(await _dataContext.GetAllAsync());
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var customer = _dataContext.GetById(id);
            var customer = await _dataContext.GetByIdAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,CompanyName,ContactName,ContactTitle,Address,City,Region,PostalCode,Country")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                //_dataContext.Create(customer);
                var result = await _dataContext.CreateAsync(customer);

                if (result == 1)
                {
                    return RedirectToAction(nameof(Index));
                }                
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _dataContext.GetByIdAsync(id);

            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,CompanyName,ContactName,ContactTitle,Address,City,Region,PostalCode,Country")] Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_dataContext.Update(customer);
                    await _dataContext.UpdateAsync(customer);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var customer = _dataContext.GetById(id);
            var customer = await _dataContext.GetByIdAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _dataContext.GetByIdAsync(id);

            if (customer != null)
            {
                //_dataContext.Delete(id);
                await _dataContext.DeleteAsync(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return ((_dataContext.GetByIdAsync(id)) != null);
        }

    }
}
