using Microsoft.AspNetCore.Mvc;
using EventManagementSystem.Data;
using EventManagementSystem.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EventManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly RegistrationDbContext _context;

        public RegistrationController(RegistrationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            var events = await _context.Registrations.ToListAsync();
            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent(int id)
        {
            var @event = await _context.Registrations.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }

            return Ok(@event);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] Registration request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            _context.Registrations.Add(request);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEvent), new { id = request.Id }, request);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEvent([FromBody] Registration request)
        {
            _context.Entry(request).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegistrationExists(request.Id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var @event = await _context.Registrations.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }

            _context.Registrations.Remove(@event);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RegistrationExists(int id)
        {
            return _context.Registrations.Any(e => e.Id == id);
        }
    }

}
