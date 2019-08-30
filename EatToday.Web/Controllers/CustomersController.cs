using EatToday.Web.Data;
using EatToday.Web.Data.Entities;
using EatToday.Web.Helpers;
using EatToday.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EatToday.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CustomersController : Controller
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public CustomersController(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        // GET: Customers
        public IActionResult Index()
        {
            return View(_context.Customers
                .Include(c => c.User)
                .Include(c => c.FavouriteRecipes)
                .ThenInclude(c => c.Recipe));
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.User)
                .Include(c => c.FavouriteRecipes)
                .ThenInclude(c => c.Recipe)
                .FirstOrDefaultAsync(m => m.Id == id);
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
        public async Task<IActionResult> Create(AddUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Address = model.Address,
                    Email = model.Username,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    UserName = model.Username

                };
                var response = await _userHelper.AddUserAsync(user, model.Password);
                if (response.Succeeded)
                {
                    var userInDB = await _userHelper.GetUserByEmailAsync(model.Username);
                    await _userHelper.AddUserToRoleAsync(userInDB, "Customer");

                    var customer = new Customer
                    {
                        Comments = new List<Comment>(),
                        RateRecipes = new List<RateRecipe>(),
                        FavouriteRecipes = new List<FavouriteRecipe>(),
                        User = userInDB

                    };
                    _context.Customers.Add(customer);
                    try
                    {
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception ex)
                    {

                        ModelState.AddModelError(string.Empty, ex.ToString());
                        return View(model);
                    }
                }

                ModelState.AddModelError(string.Empty, response.Errors.FirstOrDefault().Description);

            }
            return View(model);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(o => o.User)
                .FirstOrDefaultAsync(o => o.Id == id.Value);
            if (customer == null)
            {
                return NotFound();
            }

            var model = new EditUserViewModel
            {
                Address = customer.User.Address,
                FirstName = customer.User.FirstName,
                Id = customer.Id,
                LastName = customer.User.LastName,
                PhoneNumber = customer.User.PhoneNumber
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = await _context.Customers
                    .Include(o => o.User)
                    .FirstOrDefaultAsync(o => o.Id == model.Id);

                customer.User.FirstName = model.FirstName;
                customer.User.LastName = model.LastName;
                customer.User.Address = model.Address;
                customer.User.PhoneNumber = model.PhoneNumber;

                await _userHelper.UpdateUserAsync(customer.User);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(r => r.User)
                .Include(r => r.Comments)
                .Include(r => r.RateRecipes)
                .Include(r => r.FavouriteRecipes)
                .FirstOrDefaultAsync(r => r.Id == id.Value);
            if (customer == null)
            {
                return NotFound();
            }

            await _userHelper.DeleteUserAsync(customer.User.Email);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
