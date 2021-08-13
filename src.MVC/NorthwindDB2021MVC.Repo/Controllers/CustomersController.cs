using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindDB2021.Core.Models;
using NorthwindDB2021.Data.EFData;
using NorthwindDB2021.Data.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindDB2021MVC.Repo.Controllers
{
    public class CustomersController : Controller
    {
        //private readonly NorthwindDB2021DbContext _context;
        private readonly ICustomersRepository _customersRepository;

        public CustomersController(ICustomersRepository customersRepository)
        {
            //_context = context;
            _customersRepository = customersRepository;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            //return View(await _context.Customers.ToListAsync());
            return View(await _customersRepository.GetAllAsync());
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var customer = await _context.Customers.FirstOrDefaultAsync(m => m.CustomerId == id);
            var customer = await _customersRepository.GetByIdAsync(id);

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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,CompanyName,ContactName,ContactTitle,Address,City,Region,PostalCode,Country")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(customer);
                //await _context.SaveChangesAsync();
                await _customersRepository.CreateAsync(customer);

                return RedirectToAction(nameof(Index));
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

            //var customer = await _context.Customers.FindAsync(id);
            var customer = await _customersRepository.GetByIdAsync(id);

            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                    //_context.Update(customer);
                    //await _context.SaveChangesAsync();
                    await _customersRepository.UpdateAsync(customer);
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

            //var customer = await _context.Customers.FirstOrDefaultAsync(m => m.CustomerId == id);
            var customer = await _customersRepository.GetByIdAsync(id);

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
            //var customer = await _context.Customers.FindAsync(id);
            //_context.Customers.Remove(customer);
            //await _context.SaveChangesAsync();
            var customer = await _customersRepository.GetByIdAsync(id);
            await _customersRepository.DeleteAsync(customer);

            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            //return _context.Customers.Any(e => e.CustomerId == id);
            return (_customersRepository.GetById(id) != null);
        }
    }
}
