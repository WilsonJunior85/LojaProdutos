//using AspNetCore;
using LojaProdutos.Dto.Produto;
using LojaProdutos.Models;

namespace LojaProdutos.Services.Produto
{
    public interface IProdutoInterface
    {
        //retorno do metodo - nome - parametros

        Task<List<ProdutoModel>> BuscarProdutos();

        Task<ProdutoModel> Cadastrar(CriarProdutoDto criarProdutoDto, IFormFile foto);

        Task<ProdutoModel> BuscarProdutoPorId(int id);

        Task<ProdutoModel> Editar(EditarProdutoDto editarProdutoDto, IFormFile? foto);

        Task<ProdutoModel> Remover(int id);

    }
}
