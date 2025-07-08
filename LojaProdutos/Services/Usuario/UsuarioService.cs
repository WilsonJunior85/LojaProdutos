using LojaProdutos.Data;
using LojaProdutos.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaProdutos.Services.Usuario
{
    public class UsuarioService : IUsuarioInterface
    {
        private readonly DataContext _context;

        public UsuarioService(DataContext context)
        {
            _context = context;
        }


        public async Task<UsuarioModel> BurcarUsuarioPorId(int id)
        {
            try
            {
                var usuario = await _context.Usuarios.Include(e => e.Endereco)
                                                     .FirstOrDefaultAsync(u => u.Id == id);
                return usuario;
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }

            
        }




        public async Task<List<UsuarioModel>> BuscarUsuarios()
        {
            try
            {
                return await _context.Usuarios.Include(e => e.Endereco).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            
        }
    }
}
