using Microsoft.AspNetCore.Mvc;

namespace LojaProdutos.Controllers
{
    public class UsuarioController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
