using LojaProdutos.Data;
using LojaProdutos.Migrations;
using LojaProdutos.Models;
using LojaProdutos.Services.Produto;

namespace LojaProdutos.Services.Estoque
{
    public class EstoqueService : IEstoqueInterface
    {
        private readonly DataContext _context;
        private readonly IProdutoInterface _produtoInterface;

        public EstoqueService(DataContext context, IProdutoInterface produtoInterface)
        {
            _context = context;
            _produtoInterface = produtoInterface;
        }

        

        public async Task<ProdutosBaixadosModel> CriarRegistro(int id)
        {
            try
            {
                var produto = await _produtoInterface.BuscarProdutoPorId(id);
                var produtoBaixado = new ProdutosBaixadosModel()
                {
                    Produto = produto,
                    ProdutoModelId = id
                };
                _context.Add(produtoBaixado);
                await _context.SaveChangesAsync();

                //Baixar Estoque
                BaixarEstoque(produto);

                _context.Update(produto);
                
                await _context.SaveChangesAsync();
                return produtoBaixado;
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public void BaixarEstoque(ProdutoModel produto) 
        {
            produto.QuantidadeEstoque--;
        }

    }
}
