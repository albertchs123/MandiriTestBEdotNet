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
    public class PaymentController : ControllerBase
    {
        private readonly PaymentDbContext _context;
        private readonly RegistrationDbContext _contextR;

        public PaymentController(PaymentDbContext context, RegistrationDbContext contextR)
        {
            _context = context;
            _contextR = contextR;
        }

        [HttpGet]
        public async Task<IActionResult> GetPayments()
        {
            var payments = await _context.Payments
                .ToListAsync();
            return Ok(payments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPayment(int id)
        {
            var payment = await _context.Payments
                .SingleOrDefaultAsync(p => p.Id == id);

            if (payment == null)
            {
                return NotFound();
            }

            return Ok(payment);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment([FromBody] Payment request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            var RegistrationExists = await _contextR.Registrations.AnyAsync(e => e.Id == request.RegistrationId);
            if (!RegistrationExists)
            {
                return BadRequest("Event not found.");
            }

            _context.Payments.Add(request);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPayment), new { id = request.Id }, request);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePayment([FromBody] Payment request)
        {
            var RegistrationExists = await _contextR.Registrations.AnyAsync(e => e.Id == request.RegistrationId);
            if (!RegistrationExists)
            {
                return BadRequest("Event not found.");
            }

            _context.Entry(request).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentExists(request.Id))
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
        public async Task<IActionResult> DeletePayment(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }

            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PaymentExists(int id)
        {
            return _context.Payments.Any(p => p.Id == id);
        }
    }

}
