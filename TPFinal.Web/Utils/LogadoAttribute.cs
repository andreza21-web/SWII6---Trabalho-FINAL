using Hanssens.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using TPFinal.Web.Models;

namespace TPFinal.Web.Utils
{
    public class LogadoAttribute : TypeFilterAttribute
    {
        public LogadoAttribute()
            : base(typeof(LogadoFilter))
        {

        }
    }
    public class LogadoFilter : Controller, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!UsuarioLogado())
            {
                RedirectToAction("Login", "Usuarios");
            }
        }



        private bool UsuarioLogado()
        {
            Usuario user = null;
            using (var storage = new LocalStorage())
            {
                if (storage.Exists("User"))
                    user = storage.Get<Usuario>("User");
            }
            return user != null;

        }
    }
}