using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using LojaProdutos.Dto.Endereco;
using LojaProdutos.Dto.Produto;
using LojaProdutos.Dto.Usuario;
using LojaProdutos.Services.Categoria;
using LojaProdutos.Services.Produto;
using LojaProdutos.Services.Usuario;
using Microsoft.AspNetCore.Mvc;

namespace LojaProdutos.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioInterface _usuarioInterface;
        private readonly IMapper _mapper;

        public UsuarioController(IUsuarioInterface usuarioInterface, IMapper mapper)
        {
            _usuarioInterface = usuarioInterface;
            _mapper = mapper;
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
                    TempData["MensagemErro"] = "Já existe usuário cadastrado com esse Email";
                    return View(criarUsuarioDto);
                }

                var usuario = await _usuarioInterface.Cadastrar(criarUsuarioDto);
                TempData["MensagemSucesso"] = "Cadastro realizado com sucesso!";
                //Faz com que volte para a tela de Index
                return RedirectToAction("Index");
            }
            else
            {
                TempData["MensagemErro"] = "Verifique os dados informados";
                return View(criarUsuarioDto);
            }
        }

        public async Task<IActionResult> Editar(int id)
        {
            var usuario = await _usuarioInterface.BurcarUsuarioPorId(id);

            var usuarioEditado = new EditarUsuarioDto
            {
                Nome = usuario.Nome,
                Cargo = usuario.Cargo,
                Email = usuario.Email,
                Id = usuario.Id,
                Endereco = _mapper.Map<EditarEnderecoDto>(usuario.Endereco)
            };

            return View(usuarioEditado);
        }

        public async Task<IActionResult> BuscarUsuarioPorId(int id)
        {
            var usuario = await _usuarioInterface.BurcarUsuarioPorId(id);
            return View(usuario);
        }


        [HttpPost]
        public async Task<IActionResult> Editar(EditarUsuarioDto editarUsuarioDto)
        {

            if (ModelState.IsValid)
            {
                var usuario = await _usuarioInterface.Editar(editarUsuarioDto);
                TempData["MensagemSucesso"] = "Edição realizada com sucesso!";
                //Faz com que volte para a tela de Index
                return RedirectToAction("Index");
            }
            else
            {
                TempData["MensagemErro"] = "Verifique os dados informados";
                return View(editarUsuarioDto);
            }


        }


    }
}
