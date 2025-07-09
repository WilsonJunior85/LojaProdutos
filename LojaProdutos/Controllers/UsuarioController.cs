using LojaProdutos.Dto.Usuario;
using LojaProdutos.Services.Produto;
using LojaProdutos.Services.Usuario;
using Microsoft.AspNetCore.Mvc;

namespace LojaProdutos.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioInterface _usuarioInterface;

        public UsuarioController(IUsuarioInterface usuarioInterface)
        {
            _usuarioInterface = usuarioInterface;
        }



        public async Task<IActionResult> Index()
        {
            var usuarios = await _usuarioInterface.BuscarUsuarios();
            return View(usuarios);
        }

        public async Task<IActionResult> Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(CriarUsuarioDto criarUsuarioDto)
        {
            if (ModelState.IsValid)
            {
                if (await _usuarioInterface.VerificaSeExisteEmail(criarUsuarioDto))
                {
                    TempData["MensagemError"] = "Já existe usuário cadastrado com esse Email";
                    return View(criarUsuarioDto);
                }

                var usuario = await _usuarioInterface.Cadastrar(criarUsuarioDto);
                TempData["MensagemSucesso"] = "Cadastro realizado com sucesso!";
                //Faz com que volte para a tela de Index
                return RedirectToAction("Index");
            }
            else
            {
                TempData["MensagemError"] = "Verifique os dados informados";
                return View(criarUsuarioDto);
            }
        }


        public async Task<IActionResult> BuscarUsuarioPorId(int id)
        {
            var usuario = await _usuarioInterface.BurcarUsuarioPorId(id);
            return View(usuario);
        }



    }
}
