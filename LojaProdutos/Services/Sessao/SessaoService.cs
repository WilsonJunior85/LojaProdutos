using LojaProdutos.Models;
using Newtonsoft.Json;

namespace LojaProdutos.Services.Sessao
{
    public class SessaoService : ISessaoInterface
    {
        private readonly IHttpContextAccessor _ctx;

        public SessaoService(IHttpContextAccessor ctx)
        {
            _ctx = ctx;
        }

        

        public UsuarioModel BuscarSessao()
        {
            string sessaoUsuario = _ctx.HttpContext.Session.GetString("usuarioSessao");

            //Essa sessaoUsuario depois que eu busquei, ela ta vazia ou nula?
            if (string.IsNullOrEmpty(sessaoUsuario))
            {
                return null;
            }
            //Se não tiver nula entao..
            return JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);
        }


        public void CriarSessao(UsuarioModel usuarioModel)
        {
            //Convertento para Json o usuario
            string usuarioJson = JsonConvert.SerializeObject(usuarioModel);
            //Criando a Sessão
            _ctx.HttpContext.Session.SetString("usuarioSessao", usuarioJson);
        }


        public void RemoverSessao()
        {
            _ctx.HttpContext.Session.Remove("usuarioSessao");
        }
    }
}
