using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnet_cp4.Persistence;
using dotnet_cp4.Persistence.Models;

namespace dotnet_cp4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly FIAPDbContext _context;

        public EventsController(FIAPDbContext context)
        {
            _context = context;
        }

        // GET: api/events
        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            var events = await _context.Events.ToListAsync();
            return Ok(events);  // Retorna os eventos em formato JSON
        }

        // GET: api/events/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent(int id)
        {
            var @event = await _context.Events
                .FirstOrDefaultAsync(e => e.Id == id);

            if (@event == null)
            {
                return NotFound();  // Retorna 404 se não encontrar o evento
            }

            return Ok(@event);  // Retorna o evento em formato JSON
        }

        // POST: api/events
        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] Event @event)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetEvent), new { id = @event.Id }, @event);
            }

            return BadRequest(ModelState);  // Retorna erro se o modelo for inválido
        }

        // PUT: api/events/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] Event @event)
        {
            if (id != @event.Id)
            {
                return BadRequest();  // Retorna erro se os IDs não coincidirem
            }

            _context.Entry(@event).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
                {
                    return NotFound();  // Retorna 404 se não encontrar o evento
                }
                throw;
            }

            return NoContent();  // Retorna 204 No Content se for sucesso
        }

        // DELETE: api/events/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();  // Retorna 404 se não encontrar o evento
            }

            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();

            return NoContent();  // Retorna 204 No Content se for sucesso
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.Id == id);
        }
    }
}
