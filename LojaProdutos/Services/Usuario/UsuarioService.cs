using LojaProdutos.Data;
using LojaProdutos.Dto.Usuario;
using LojaProdutos.Models;
using LojaProdutos.Services.Autenticacao;
using Microsoft.EntityFrameworkCore;

namespace LojaProdutos.Services.Usuario
{
    public class UsuarioService : IUsuarioInterface
    {
        private readonly DataContext _context;
        private readonly IAutenticacaoInterface _autenticacaoInterface;

        public UsuarioService(DataContext context, IAutenticacaoInterface autenticacaoInterface)
        {
            _context = context;
            _autenticacaoInterface = autenticacaoInterface;
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

        public async Task<CriarUsuarioDto> Cadastrar(CriarUsuarioDto criarUsuarioDto)
        {

            try
            {
                //Burcar por um serviço que cria a SenhaSalt
                _autenticacaoInterface.CriarSenhaHash(criarUsuarioDto.Senha, out byte[] senhaHash, out byte[] senhaSalt);

                var usuario = new UsuarioModel
                {
                    Nome = criarUsuarioDto.Nome,
                    Email = criarUsuarioDto.Email,
                    Cargo = criarUsuarioDto.Cargo,
                    SenhaHash = senhaHash,
                    SenhaSalt = senhaSalt

                };

                var endereco = new EnderecoModel
                {
                    Logradouro = criarUsuarioDto.Logradouro,
                    Numero = criarUsuarioDto.Numero,
                    Bairro = criarUsuarioDto.Bairro,
                    Estado = criarUsuarioDto.Estado,
                    Complemento = criarUsuarioDto.Complemento,
                    CEP = criarUsuarioDto.CEP,
                    Usuario = usuario
                };
                //Dentro do usuário, colocamos o endereço
                usuario.Endereco = endereco;

                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return criarUsuarioDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<bool> VerificaSeExisteEmail(CriarUsuarioDto criarUsuarioDto)
        {
            try
            {
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(e => e.Email == criarUsuarioDto.Email);

                if (usuario == null)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
