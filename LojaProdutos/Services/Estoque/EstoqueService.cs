using LojaProdutos.Data;
using LojaProdutos.Migrations;
using LojaProdutos.Models;
using LojaProdutos.Services.Produto;
using Microsoft.EntityFrameworkCore;

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




        public List<RegistroProdutosModel> listagemRegistros()
        {
            try
            {
                var result = from c in _context.ProdutosBaixados.Include(x => x.Produto)
                                                                .Include(y => y.Produto.Categoria)
                                                                .ToList()
                             group c by new { c.Produto.CategoriaModelId, c.DataDaBaixa } into total
                             select new
                             {
                                 ProdutoId = total.First().Produto.Categoria.Id,
                                 CategoriaNome = total.First().Produto.Categoria.Nome,
                                 DataCompra = total.First().DataDaBaixa,
                                 Total = total.Sum(c => c.Produto.Valor)
                             };


                //Total geral
                var totalGeral = _context.ProdutosBaixados.Include(x => x.Produto)
                                                          .Include(y => y.Produto.Categoria)
                                                          .Sum(c => c.Produto.Valor);


                List<RegistroProdutosModel> lista = new List<RegistroProdutosModel>();

                foreach (var resultado in result)
                {
                    var registro = new RegistroProdutosModel()
                    {
                        ProdutoId = resultado.ProdutoId,
                        CategoriaNome = resultado.CategoriaNome,
                        DataCompra = resultado.DataCompra,
                        Total = resultado.Total,
                        TotalGeral = totalGeral
                    };

                    //Adicionando dentro da lista
                    lista.Add(registro);
                }
                                                          
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
    }
}
