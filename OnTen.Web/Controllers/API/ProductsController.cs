using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnTen.Web.Data;
using OnTen.Web.Data.Entities;

namespace OnTen.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DataContext _context;

        public ProductsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Countries
        [HttpGet]
        public IActionResult GetCountries()
        {
            return Ok(_context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .Where(p => p.IsActive));

        }
    }
}
