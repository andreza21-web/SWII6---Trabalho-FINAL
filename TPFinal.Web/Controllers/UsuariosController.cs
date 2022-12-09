using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hanssens.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TPFinal.Web.Models;
using TPFinal.Web.Service;

namespace TPFinal.Web.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly UsuarioService _usuarioService;


        public UsuariosController()
        {
            _usuarioService = new UsuarioService();
        }

        // GET: Usuarios/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Usuarios/Logar
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logar([Bind("Nome,Senha")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                var user = await _usuarioService.logar(usuario);

                if (user == null)
                    return View("Login");

                using (var storage = new LocalStorage())
                {
                    storage.Store("User", user);
                }
                return RedirectToAction("Index", "Produtos");
            }
            return View("Login");
        }

        // POST: Usuarios/Logar
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        public IActionResult Logout()
        {
            using (var storage = new LocalStorage())
            {
                storage.Remove("User");
            }

            return View("Login");
        }

    }
}
