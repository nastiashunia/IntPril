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

    public class OrderStringsController : ControllerBase
    {
        private readonly OrderingContext _context;

        public OrderStringsController(OrderingContext context)
        {
            _context = context;

        }

        [HttpGet]
        public IEnumerable<OrderString> GetAll()
        {
            return _context.OrderString;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderString([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var orderString = await _context.OrderString.SingleOrDefaultAsync(m =>
            m.OrderStringId == id);
            if (orderString == null)
            {
                return NotFound();
            }
            return Ok(orderString);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderString orderString)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.OrderString.Add(orderString);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetOrderString", new { id = orderString.OrderStringId },
            orderString);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id,[FromBody] OrderString orderString)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var item = _context.OrderString.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            item.Cost = orderString.Cost;
            item.Count = orderString.Count;
            _context.OrderString.Update(item);
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
            var item = _context.OrderString.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            _context.OrderString.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }


    }
}
