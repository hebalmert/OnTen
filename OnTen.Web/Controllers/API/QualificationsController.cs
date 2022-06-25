using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnTen.Common.Request;
using OnTen.Web.Data;
using OnTen.Web.Data.Entities;
using OnTen.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnTen.Web.Controllers.API
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class QualificationsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public QualificationsController(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        [HttpPost]
        public async Task<IActionResult> PostQualification([FromBody] QualificationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            string email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            User user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                return NotFound("Error001");
            }

            Product product = await _context.Products
                .Include(p => p.Qualifications)
                .FirstOrDefaultAsync(p => p.ProductId == request.ProductId);
            if (product == null)
            {
                return NotFound("Error002");
            }

            if (product.Qualifications == null)
            {
                product.Qualifications = new List<Qualification>();
            }

            product.Qualifications.Add(new Qualification
            {
                Date = DateTime.UtcNow,
                Product = product,
                Remarks = request.Remarks,
                Score = request.Score,
                User = user
            });

            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return Ok(product);
        }

    }
}
