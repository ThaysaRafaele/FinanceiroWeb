using FinanceiroWeb.Data;
using FinanceiroWeb.Extensions;
using FinanceiroWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FinanceiroWeb.Controllers
{
    public class NotasFiscaisController : Controller
    {
        private readonly FinanceiroContext _context;

        public NotasFiscaisController(FinanceiroContext context)
        {
            _context = context;
        }

        // GET: NotasFiscais/Index
        //public async Task<IActionResult> Index()
        //{
        //    var notasFiscais = await _context.NotasFiscais.ToListAsync();
        //    return View(notasFiscais);
        //}

        public async Task<IActionResult> Index(int? anoEmissao, int? mesEmissao, int? anoCobranca, int? mesCobranca, int? anoPagamento, int? mesPagamento, StatusNotaFiscal? status)
        {
            var query = _context.NotasFiscais.AsQueryable();

            if (anoEmissao.HasValue && mesEmissao.HasValue)
            {
                query = query.Where(nf => nf.DataEmissao.Year == anoEmissao.Value && nf.DataEmissao.Month == mesEmissao.Value);
            }

            if (anoCobranca.HasValue && mesCobranca.HasValue)
            {
                query = query.Where(nf => nf.DataCobranca.Year == anoCobranca.Value && nf.DataCobranca.Month == mesCobranca.Value);
            }

            if (anoPagamento.HasValue && mesPagamento.HasValue)
            {
                query = query.Where(nf => nf.DataPagamento.HasValue && nf.DataPagamento.Value.Year == anoPagamento.Value && nf.DataPagamento.Value.Month == mesPagamento.Value);
            }

            if (status.HasValue)
            {
                query = query.Where(nf => nf.Status == status.Value);
            }

            var notasFiscais = await query.ToListAsync();

            
            ViewBag.StatusOptions = new SelectList(Enum.GetValues(typeof(StatusNotaFiscal))
                .Cast<StatusNotaFiscal>()
                .Select(s => new { Value = s, Text = s.GetDisplayName() }),
                "Value", "Text");

            return View(notasFiscais);
        }

        // GET: NotasFiscais/AdicionarNF
        public IActionResult AdicionarNF()
        {
            return View();
        }

        // POST: NotasFiscais/AdicionarNF
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdicionarNF([Bind("NomePagador,NumeroNota,DataEmissao,DataCobranca,DataPagamento,Valor,DocumentoNota,DocumentoBoleto,Status")] NotaFiscal notaFiscal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(notaFiscal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(notaFiscal);
        }

        // GET: NotasFiscais/EditarNF/5
        public async Task<IActionResult> EditarNF(int id)
        {
            var notaFiscal = await _context.NotasFiscais.FindAsync(id);
            if (notaFiscal == null)
            {
                return NotFound();
            }
            return View(notaFiscal);
        }

        // POST: NotasFiscais/EditarNF/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarNF(int id, [Bind("NotaId,NomePagador,NumeroNota,DataEmissao,DataCobranca,DataPagamento,Valor,DocumentoNota,DocumentoBoleto,Status")] NotaFiscal notaFiscal)
        {
            if (id != notaFiscal.NotaId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(notaFiscal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotaFiscalExists(notaFiscal.NotaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(notaFiscal);
        }

        // GET: NotasFiscais/ExcluirNF/5
        public async Task<IActionResult> ExcluirNF(int id)
        {
            var notaFiscal = await _context.NotasFiscais
                .FirstOrDefaultAsync(m => m.NotaId == id);
            if (notaFiscal == null)
            {
                return NotFound();
            }

            return View(notaFiscal);
        }

        // POST: NotasFiscais/ExcluirNF/5
        [HttpPost, ActionName("ExcluirNF")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var notaFiscal = await _context.NotasFiscais.FindAsync(id);
            _context.NotasFiscais.Remove(notaFiscal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotaFiscalExists(int id)
        {
            return _context.NotasFiscais.Any(e => e.NotaId == id);
        }
    }
}
