using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab2.Models;

namespace Lab2.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly OrderingContext _context;
        public UsersController(OrderingContext context)
        {
            _context = context;

        }
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<User> GetAll()
        {
            return _context.User.Include(p => p.Order);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
       public async Task<IActionResult> GetUser([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _context.User.SingleOrDefaultAsync(m =>
            m.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }





        // POST api/<controller>
        [HttpPost]
      public async Task<IActionResult> CreateUser([FromBody]User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.User.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody]User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var item = _context.User.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            item.UserName = user.UserName;
            item.Email = user.Email;
            
            _context.User.Update(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var item = _context.User.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            _context.User.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
