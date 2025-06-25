using LojaProdutos.Models;

namespace LojaProdutos.Services.Produto
{
    public interface IProdutoInterface
    {
        //retorno do metodo - nome - parametros

        Task<List<ProdutoModel>> BuscarProdutos();
        
    }
}
