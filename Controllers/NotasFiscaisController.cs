using FinanceiroWeb.Data;
using FinanceiroWeb.Extensions;
using FinanceiroWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IActionResult> Index(int? mesEmissao, int? mesCobranca, int? mesPagamento, StatusNotaFiscal? status)
        {
            // Inicializando a query base
            var query = _context.NotasFiscais.AsQueryable();

            // Filtrando por mês de emissão
            if (mesEmissao.HasValue)
            {
                query = query.Where(nf => nf.DataEmissao.Month == mesEmissao.Value);
            }

            // Filtrando por mês de cobrança
            if (mesCobranca.HasValue)
            {
                query = query.Where(nf => nf.DataCobranca.Month == mesCobranca.Value);
            }

            // Filtrando por mês de pagamento
            if (mesPagamento.HasValue)
            {
                query = query.Where(nf => nf.DataPagamento.HasValue && nf.DataPagamento.Value.Month == mesPagamento.Value);
            }

            // Filtrando por status (se um status for selecionado)
            if (status.HasValue)
            {
                query = query.Where(nf => nf.Status == status.Value);
            }

            // Executando a query
            var notasFiscais = await query.ToListAsync();

            // Preparando os dados para a View
            var statusOptions = Enum.GetValues(typeof(StatusNotaFiscal))
                .Cast<StatusNotaFiscal>()
                .Select(s => new SelectListItem
                {
                    Value = ((int)s).ToString(),
                    Text = s.GetDisplayName(),
                    Selected = status.HasValue && s == status.Value
                }).ToList();

            ViewBag.StatusOptions = statusOptions;
            ViewBag.MesEmissao = mesEmissao;
            ViewBag.MesCobranca = mesCobranca;
            ViewBag.MesPagamento = mesPagamento;
            ViewBag.Status = status;

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
