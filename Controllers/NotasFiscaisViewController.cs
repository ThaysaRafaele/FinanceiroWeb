using FinanceiroWeb.Data;
using FinanceiroWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FinanceiroWeb.Controllers
{
    public class NotasFiscaisViewController : Controller
    {
        private readonly FinanceiroContext _context;

        public NotasFiscaisViewController(FinanceiroContext context)
        {
            _context = context;
        }

        // GET: NotasFiscaisView/Index
        public async Task<IActionResult> Index()
        {
            var notasFiscais = await _context.NotasFiscais.ToListAsync();
            return View(notasFiscais); // Retorna a view e passa as notas fiscais como model
        }
    }
}
