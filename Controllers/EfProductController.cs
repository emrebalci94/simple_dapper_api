using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dapper_rest_api.Data.EfContext;
using Microsoft.EntityFrameworkCore;

namespace dapper_rest_api.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EfProductController : ControllerBase
    {
        private readonly NorthwindContext _context;

        public EfProductController()
        {
            _context = new NorthwindContext();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //var list = await _context.Products.ToListAsync();
           var list = await _context.Products.Include("Category").Include("Supplier").ToListAsync();

            return Ok(list);

        }
    }
}
