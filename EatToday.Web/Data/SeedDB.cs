using EatToday.Web.Data.Entities;
using EatToday.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EatToday.Web.Data
{
    public class SeedDB
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDB(
            DataContext context,
            IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckRoles();
            var manager = await CheckUserAsync("Juan", "Martínez", "juanmartinez1712@gmail.com", "310 409 9129", "Calle Luna Calle Sol", "Admin");
            var customer = await CheckUserAsync("Juan", "Martinez", "juan.a.martinez33@hotmail.com", "310 409 9129", "Calle Luna Calle Sol", "Customer");
            await CheckIngredientsAsync();
            await CheckRecipeTypesAsync();
            await CheckCustomerAsync(customer);
            await CheckManagerAsync(manager);
        }

        private async Task CheckRoles()
        {
            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("Customer");
        }

        private async Task<User> CheckUserAsync(string firstName, string lastName, string email, string phone, string address, string role)
        {
            var user = await _userHelper.GetUserByEmailAsync(email);
            if (user == null)
            {
                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, role);
            }

            return user;
        }

        private async Task CheckRecipeTypesAsync()
        {
            if (!_context.RecipeTypes.Any())
            {
                _context.RecipeTypes.Add(new RecipeType { Name = "Sopa" });
                _context.RecipeTypes.Add(new RecipeType { Name = "Pasta" });
                _context.RecipeTypes.Add(new RecipeType { Name = "Ensalada" });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckIngredientsAsync()
        {
            if (!_context.Ingredients.Any())
            {
                _context.Ingredients.Add(new Ingredient { Name = "Arroz" });
                _context.Ingredients.Add(new Ingredient { Name = "Tomate" });
                _context.Ingredients.Add(new Ingredient { Name = "Spaguetti" });
                _context.Ingredients.Add(new Ingredient { Name = "Lechuga" });
                _context.Ingredients.Add(new Ingredient { Name = "Leche" });
                _context.Ingredients.Add(new Ingredient { Name = "Cebolla" });
                _context.Ingredients.Add(new Ingredient { Name = "Crema de leche" });
                _context.Ingredients.Add(new Ingredient { Name = "Queso parmesano" });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckCustomerAsync(User user)
        {
            if (!_context.Customers.Any())
            {
                _context.Customers.Add(new Customer { User = user });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckManagerAsync(User user)
        {
            if (!_context.Managers.Any())
            {
                _context.Managers.Add(new Manager { User = user });
                await _context.SaveChangesAsync();
            }
        }

    }


    //public class SeedDB
    //{
    //    private readonly DataContext _context;
    //    private readonly IUserHelper _userHelper;

    //    public SeedDB(DataContext dataContext, IUserHelper userHelper)
    //    {
    //        _context = dataContext;
    //        _userHelper = userHelper;
    //    }

    //    public async Task SeedAsync()
    //    {
    //        await _context.Database.EnsureCreatedAsync();
    //        await CheckRoles();
    //        var manager = await CheckUserAsync("Juan", "Martinez", "juanmartinez1712@gmail.com", "3104099129", "Calle Luna Calle Sol", "Admin");
    //        var customer = await CheckUserAsync("Juan", "Martinez", "juan.a.martinez33@outlook .com", "3104099129", "Calle Luna Calle Sol", "Customer");
    //        await CheckIngredientsAsync();
    //        await CheckRecipeTypesAsync();
    //        await CheckOwnerAsync(customer);
    //        await CheckManagerAsync(manager);

    //    }

    //    private async Task CheckRecipeTypesAsync()
    //    {
    //        if (!_context.RecipeTypes.Any())
    //        {
    //            _context.RecipeTypes.Add(new RecipeType { Name = "Sopa" });
    //            _context.RecipeTypes.Add(new RecipeType { Name = "Pasta" });
    //            _context.RecipeTypes.Add(new RecipeType { Name = "Ensalada" });
    //            await _context.SaveChangesAsync();
    //        }
    //    }

    //    private async Task CheckIngredientsAsync()
    //    {
    //        if (!_context.Ingredients.Any())
    //        {
    //            _context.Ingredients.Add(new Ingredient { Name = "Arroz" });
    //            _context.Ingredients.Add(new Ingredient { Name = "Tomate" });
    //            _context.Ingredients.Add(new Ingredient { Name = "Spaguetti" });
    //            _context.Ingredients.Add(new Ingredient { Name = "Lechuga" });
    //            _context.Ingredients.Add(new Ingredient { Name = "Leche" });
    //            _context.Ingredients.Add(new Ingredient { Name = "Cebolla" });
    //            _context.Ingredients.Add(new Ingredient { Name = "Crema de leche" });
    //            _context.Ingredients.Add(new Ingredient { Name = "Queso parmesano" });
    //            await _context.SaveChangesAsync();
    //        }
    //    }
    //    private async Task CheckRoles()
    //    {
    //        await _userHelper.CheckRoleAsync("Admin");
    //        await _userHelper.CheckRoleAsync("Customer");
    //    }
    //    private async Task<User> CheckUserAsync(string firstName, string lastName, string email, string phone, string address, string role)
    //    {
    //        var user = await _userHelper.GetUserByEmailAsync(email);
    //        if (user == null)
    //        {
    //            user = new User
    //            {
    //                FirstName = firstName,
    //                LastName = lastName,
    //                Email = email,
    //                UserName = email,
    //                PhoneNumber = phone,
    //                Address = address,
    //            };

    //            await _userHelper.AddUserAsync(user, "123456");
    //            await _userHelper.AddUserToRoleAsync(user, role);
    //        }

    //        return user;
    //    }
    //    private async Task CheckOwnerAsync(User user)
    //    {
    //        if (!_context.Customers.Any())
    //        {
    //            _context.Customers.Add(new Customer { User = user });
    //            await _context.SaveChangesAsync();
    //        }
    //    }

    //    private async Task CheckManagerAsync(User user)
    //    {
    //        if (!_context.Managers.Any())
    //        {
    //            _context.Managers.Add(new Manager { User = user });
    //            await _context.SaveChangesAsync();
    //        }
    //    }
    //}
}
