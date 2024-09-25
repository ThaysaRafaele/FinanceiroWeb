using FinanceiroWeb.Data;
using FinanceiroWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinanceiroWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotasFiscaisController : ControllerBase
    {
        private readonly FinanceiroContext _context;

        public NotasFiscaisController(FinanceiroContext context)
        {
            _context = context;
        }

        // GET: api/NotasFiscais
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotaFiscal>>> GetNotasFiscais()
        {
            return await _context.NotasFiscais.ToListAsync();
        }

        // GET: api/NotasFiscais/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NotaFiscal>> GetNotaFiscal(int id)
        {
            var notaFiscal = await _context.NotasFiscais.FindAsync(id);

            if (notaFiscal == null)
            {
                return NotFound();
            }

            return notaFiscal;
        }

        // POST: api/NotasFiscais
        [HttpPost]
        public async Task<ActionResult<NotaFiscal>> PostNotaFiscal(NotaFiscal notaFiscal)
        {
            _context.NotasFiscais.Add(notaFiscal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNotaFiscal", new { id = notaFiscal.NotaId }, notaFiscal);
        }

        // PUT: api/NotasFiscais/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNotaFiscal(int id, NotaFiscal notaFiscal)
        {
            if (id != notaFiscal.NotaId)
            {
                return BadRequest();
            }

            _context.Entry(notaFiscal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotaFiscalExists(id))
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

        // DELETE: api/NotasFiscais/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotaFiscal(int id)
        {
            var notaFiscal = await _context.NotasFiscais.FindAsync(id);
            if (notaFiscal == null)
            {
                return NotFound();
            }

            _context.NotasFiscais.Remove(notaFiscal);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NotaFiscalExists(int id)
        {
            return _context.NotasFiscais.Any(e => e.NotaId == id);
        }
    }
}
