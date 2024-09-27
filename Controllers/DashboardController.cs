using FinanceiroWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinanceiroWeb.Controllers
{
    public class DashboardController : Controller
    {
        private readonly FinanceiroContext _context;

        public DashboardController(FinanceiroContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? ano, int? mes)
        {
            // Filtros de ano e mês 
            var notasFiscais = _context.NotasFiscais.AsQueryable();
            if (ano.HasValue)
            {
                notasFiscais = notasFiscais.Where(nf => nf.DataEmissao.Year == ano.Value);
            }
            if (mes.HasValue)
            {
                notasFiscais = notasFiscais.Where(nf => nf.DataEmissao.Month == mes.Value);
            }

            // Indicadores
            var totalEmitidas = await notasFiscais.SumAsync(nf => nf.Valor);

            var totalEmitidasSemCobranca = await notasFiscais
                .Where(nf => nf.Status == Models.StatusNotaFiscal.Emitida)
                .SumAsync(nf => nf.Valor);

            var totalVencidas = await notasFiscais
                .Where(nf => nf.Status == Models.StatusNotaFiscal.PagamentoEmAtraso)
                .SumAsync(nf => nf.Valor);

            var totalAVencer = await notasFiscais
                .Where(nf => nf.DataPagamento == null && nf.DataCobranca >= DateTime.Now)
                .SumAsync(nf => nf.Valor);

            var totalPagas = await notasFiscais
                .Where(nf => nf.Status == Models.StatusNotaFiscal.PagamentoRealizado)
                .SumAsync(nf => nf.Valor);

            // Gráficos - Evolução da inadimplência e receita
            var inadimplenciaPorMes = await notasFiscais
                .GroupBy(nf => nf.DataEmissao.Month)
                .Select(group => new
                {
                    Mes = group.Key,
                    TotalInadimplencia = group.Where(nf => nf.Status == Models.StatusNotaFiscal.PagamentoEmAtraso).Sum(nf => nf.Valor),
                    TotalReceita = group.Where(nf => nf.Status == Models.StatusNotaFiscal.PagamentoRealizado).Sum(nf => nf.Valor)
                }).ToListAsync();

            // Passando os valores para a View
            ViewBag.TotalEmitidas = totalEmitidas;
            ViewBag.TotalEmitidasSemCobranca = totalEmitidasSemCobranca;
            ViewBag.TotalVencidas = totalVencidas;
            ViewBag.TotalAVencer = totalAVencer;
            ViewBag.TotalPagas = totalPagas;
            ViewBag.InadimplenciaPorMes = inadimplenciaPorMes;

            return View();
        }
    }
}