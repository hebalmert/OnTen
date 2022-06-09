using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnTen.Common.Entities;
using OnTen.Web.Data;

namespace OnTen.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly DataContext _context;

        public CountriesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Countries
        [HttpGet]
        public IActionResult GetCountries()
        {
            return Ok(_context.Countries
                .Include(d => d.Departments)
                .ThenInclude(d=> d.Cities));
        }
    }
}
