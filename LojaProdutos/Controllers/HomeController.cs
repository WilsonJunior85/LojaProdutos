
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LojaProdutos.Controllers
{
    public class HomeController : Controller
    {
        

        public IActionResult Index()
        {
            return View();
        }

     
    }
}
