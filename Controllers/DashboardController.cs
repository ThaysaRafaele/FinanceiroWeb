using Microsoft.AspNetCore.Mvc;

namespace FinanceiroWeb.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
