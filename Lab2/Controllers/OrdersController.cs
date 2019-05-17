using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Lab2.Models;

namespace Lab2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderingContext _context;

        public OrdersController(OrderingContext context)
        {
            _context = context;

        }

        [HttpGet]
        public IEnumerable<Order> GetAll()
        {
            return _context.Order.Include(p => p.OrderString);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var order = await _context.Order.SingleOrDefaultAsync(m =>
            m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            order.Date = DateTime.Now;
            _context.Order.Add(order);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetOrder", new { id = order.OrderId },
            order);
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id,[FromBody] Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var item = _context.Order.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            item.Date = order.Date;
            item.Act = order.Act;
            item.Sum = order.Sum;
            _context.Order.Update(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [Authorize(Roles = "user")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var item = _context.Order.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            _context.Order.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        
    }
}