using LojaProdutos.Data;
using LojaProdutos.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaProdutos.Services.Produto
{
    public class ProdutoService : IProdutoInterface
    {
        
        private readonly DataContext _context;

        public ProdutoService(DataContext context)
        {
            
            _context = context;
        }




        public async Task<List<ProdutoModel>> BuscarProdutos()
        {


            try
            {
                return await _context.Produtos.Include(c => c.Categoria).ToListAsync();
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
     
               
        
        }





    }

}
