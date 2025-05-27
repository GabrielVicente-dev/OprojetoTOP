using Microsoft.AspNetCore.Mvc;

namespace OprojetoTop.Controllers
{
    public class ProdutoController : Controller
    {
        public IActionResult Produto()
        {
            return View();
        }
    }
}
