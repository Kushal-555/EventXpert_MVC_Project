using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventXpert.Data;
using EventXpert.Models;

namespace EventXpert.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public EventsApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/eventsapi
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var events = await _context.Events.ToListAsync();
            return Ok(events);
        }

        // GET: api/eventsapi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ev = await _context.Events.FindAsync(id);
            if (ev == null) return NotFound();
            return Ok(ev);
        }

        // POST: api/eventsapi
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Event ev)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Events.Add(ev);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = ev.Id }, ev);
        }

        // PUT: api/eventsapi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Event ev)
        {
            if (id != ev.Id) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _context.Entry(ev).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/eventsapi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ev = await _context.Events.FindAsync(id);
            if (ev == null) return NotFound();

            _context.Events.Remove(ev);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
