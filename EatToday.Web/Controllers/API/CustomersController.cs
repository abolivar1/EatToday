﻿using EatToday.Common.Models;
using EatToday.Web.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EatToday.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CustomersController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public CustomersController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpPost]
        [Route("GetCustomerByEmail")]
        public async Task<IActionResult> GetCustomer(EmailRequest emailRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var customer =  await _dataContext.Customers
                .Include(c => c.User)
                .Include(c => c.Comments)
                .Include(c => c.RateRecipes)
                .Include(c => c.FavouriteRecipes)
                .FirstOrDefaultAsync(c => c.User.Email == emailRequest.Email);

            if (customer == null)
            {
                return NotFound();
            }
            var response = new CustomerResponse
            {
                Id = customer.Id,
                FirstName = customer.User.FirstName,
                LastName = customer.User.LastName,
                Address = customer.User.Address,
                Email = customer.User.Email,
                PhoneNumber = customer.User.PhoneNumber
                
            };
            return Ok(response);
        }
    }
}
