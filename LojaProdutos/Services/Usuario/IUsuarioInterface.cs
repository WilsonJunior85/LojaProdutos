using LojaProdutos.Dto.Usuario;
using LojaProdutos.Models;

namespace LojaProdutos.Services.Usuario
{
    public interface IUsuarioInterface
    {
        Task<List<UsuarioModel>> BuscarUsuarios();
        Task<UsuarioModel> BurcarUsuarioPorId(int id);
        Task<bool> VerificaSeExisteEmail(CriarUsuarioDto criarUsuarioDto);
        Task<CriarUsuarioDto> Cadastrar(CriarUsuarioDto criarUsuarioDto);
    }
}
