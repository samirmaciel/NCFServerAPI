using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerAPI_NCF.Models;

namespace ServerAPI_NCF.Controllers
{
    [Route("api/[User]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DBContext _context;

        public UserController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Auth
        [HttpGet("/GetAllUsers")]
        public async Task<ActionResult<IEnumerable<UserItem>>> GetUserItem()
        {
            return await _context.UserItem.ToListAsync();
        }

        // GET: api/Auth/5
        [HttpGet("/GetUserByID={id}")]
        public async Task<ActionResult<UserItem>> GetUserItem(string id)
        {
            var userItem = await _context.UserItem.FindAsync(id);

            if (userItem == null)
            {
                return NotFound();
            }

            return userItem;
        }

        // PUT: api/Auth/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("/UpdateUser={id}")]
        public async Task<IActionResult> PutUserItem(string id, UserItem userItem)
        {
            if (id != userItem.id)
            {
                return BadRequest();
            }

            _context.Entry(userItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Auth
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("/RegisterUser")]
        public async Task<ActionResult<UserItem>> PostUserItem(UserItem userItem)
        {
            _context.UserItem.Add(userItem);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserItemExists(userItem.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserItem", new { id = userItem.id }, userItem);
        }

        // DELETE: api/Auth/5
        [HttpDelete("/DeleteUser={id}")]
        public async Task<IActionResult> DeleteUserItem(string id)
        {
            var userItem = await _context.UserItem.FindAsync(id);
            if (userItem == null)
            {
                return NotFound();
            }

            _context.UserItem.Remove(userItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserItemExists(string? id)
        {
            return _context.UserItem.Any(e => e.id == id);
        }
    }
}
