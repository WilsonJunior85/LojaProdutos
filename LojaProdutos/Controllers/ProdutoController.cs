using LojaProdutos.Dto.Produto;
using LojaProdutos.Filtros;
using LojaProdutos.Services.Categoria;
using LojaProdutos.Services.Produto;
using Microsoft.AspNetCore.Mvc;

namespace LojaProdutos.Controllers
{
    [FiltroUsuarioLogado]

    public class ProdutoController : Controller
    {
        private readonly IProdutoInterface _produtoInterface;
        private readonly ICategoriaInterface _categoriaInterface;

        public ProdutoController(IProdutoInterface produtoInterface, ICategoriaInterface categoriaInterface)
        {
            _produtoInterface = produtoInterface;
            _categoriaInterface = categoriaInterface;
        }

        [FiltroUsuarioLogadoAdm]
        public async Task<IActionResult> Index()
        {
            var produtos = await _produtoInterface.BuscarProdutos();

            return View(produtos);
        }

        [FiltroUsuarioLogadoAdm]
        public async Task<IActionResult> Cadastrar() 
        { 
          ViewBag.Categorias =  await _categoriaInterface.BuscarCategorias();

          return View();
        }

        [FiltroUsuarioLogadoAdm]
        public async Task<IActionResult> Editar(int id)
        {
            var produto = await _produtoInterface.BuscarProdutoPorId(id);

            var editarProdutoDto = new EditarProdutoDto
            {
                Nome = produto.Nome,
                Marca = produto.Marca,
                Foto = produto.Foto,
                Valor = produto.Valor,
                QuantidadeEstoque = produto.QuantidadeEstoque,
                CategoriaModelId = produto.CategoriaModelId,
            };

            ViewBag.Categorias = await _categoriaInterface.BuscarCategorias();
            return View(editarProdutoDto);
        }




        
        [HttpPost]
        [FiltroUsuarioLogadoAdm]
        public async Task<IActionResult> Cadastrar(CriarProdutoDto criarProdutoDto, IFormFile foto)
        {
            if (ModelState.IsValid)
            {
                var produto = await _produtoInterface.Cadastrar(criarProdutoDto, foto);
                //Sucess
                TempData["MensagemSucesso"]= "Produto cadastrado com sucesso!";
                return RedirectToAction("Index","Produto");
            }
            else
            {   //Sucess
                TempData["MensagemErro"] = "Ocorreu algum erro no processo!";
                ViewBag.Categorias = await _categoriaInterface.BuscarCategorias();
                return View(criarProdutoDto);
            }
        }


        [HttpPost]
        [FiltroUsuarioLogadoAdm]
        public async Task<IActionResult> Editar(EditarProdutoDto editarProdutoDto, IFormFile? foto)
        {
            if (ModelState.IsValid)
            {
                var produto = await _produtoInterface.Editar(editarProdutoDto, foto);
                //Sucess
                TempData["MensagemSucesso"] = "Produto editado com sucesso!";
                return RedirectToAction("Index","Produto");
            }
            else
            {   //Sucess
                TempData["MensagemErro"] = "Ocorreu algum erro no processo!";
                ViewBag.Categorias = await _categoriaInterface.BuscarCategorias();
                return View(editarProdutoDto);
            }
        }

        [FiltroUsuarioLogadoAdm]
        public async Task<IActionResult> Remover(int id)
        {
            var produto = await _produtoInterface.Remover(id);
            return RedirectToAction("Index","Produto");
        }


        public async Task<IActionResult> Detalhes(int id)
        {
            var produto = await _produtoInterface.BuscarProdutoPorId(id);
            return View(produto);
        }


    }
}
