using LojaProdutos.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LojaProdutos.Filtros
{
    public class FiltroUsuarioLogado : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            //Criando uma string sessão
            string sessao = context.HttpContext.Session.GetString("usuarioSessao");
            if (string.IsNullOrEmpty(sessao))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    {"controller","Login"},
                    {"action","Login"}
                });
            }
            else
            {
                UsuarioModel usuarioModel = JsonConvert.DeserializeObject<UsuarioModel>(sessao);
                if (usuarioModel == null)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary
                    {
                       {"controller","Login"},
                       {"action","Login"}
                    });
                }
                base.OnActionExecuted(context);
            }

        }
    }
}
