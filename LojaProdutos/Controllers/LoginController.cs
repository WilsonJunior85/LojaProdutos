using LojaProdutos.Dto.Login;
using LojaProdutos.Services.Usuario;
using Microsoft.AspNetCore.Mvc;

namespace LojaProdutos.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioInterface _usuarioInterface;

        public LoginController(IUsuarioInterface usuarioInterface)
        {
            _usuarioInterface = usuarioInterface;
        }

        

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginUsuarioDto loginUsuarioDto)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _usuarioInterface.Login(loginUsuarioDto);
                if (usuario == null) 
                {
                    TempData["MensagemErro"] = "Verifique os dados informados!";
                    return View(loginUsuarioDto);
                }
                //Se der tudo certo entao..
                TempData["MensagemSucesso"] = "Usuário logado com sucesso!";
                return RedirectToAction("Index","Home");
            }
            else
            {
                TempData["MensagemErro"] = "Verifique os dados informados!";
                return View(loginUsuarioDto);
            }
        }

    }
}
