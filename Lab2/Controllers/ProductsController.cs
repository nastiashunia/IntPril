using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab2.Models;
using Microsoft.AspNetCore.Authorization;

namespace Lab2.Controllers
{
    [Route("api/[controller]")]

    public class ProductsController : ControllerBase
    {
        private readonly OrderingContext _context;

        public ProductsController(OrderingContext context)
        {
            _context = context;

        }

        [HttpGet]
        public IEnumerable<Product> GetAll()
        {
            return _context.Product.Include(p => p.OrderString);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var product = await _context.Product.SingleOrDefaultAsync(m =>
            m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Product.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetProduct", new { id = product.ProductId },
            product);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id,[FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var item = _context.Product.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            item.Name = product.Name;
            item.Costs = product.Costs;

            _context.Product.Update(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var item = _context.Product.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            _context.Product.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }


    }
}