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
using TPFinal.Web.Utils;

namespace TPFinal.Web.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly ProdutosService _produtosService;

        public ProdutosController()
        {
            _produtosService = new ProdutosService();
        }

        // GET: Produtos
        public async Task<IActionResult> Index()
        {
            if (!UsuarioLogado())
            {
                return RedirectToAction("Login", "Usuarios");
            }

            return View(await _produtosService.List());
        }

        // GET: Produtos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!UsuarioLogado())
            {
                return RedirectToAction("Login", "Usuarios");
            }

            if (id == null)
            {
                return NotFound();
            }

            var produto = await _produtosService.Find(id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // GET: Produtos/Create
        public IActionResult Create()
        {
            if (!UsuarioLogado())
            {
                return RedirectToAction("Login", "Usuarios");
            }
            return View();
        }

        // POST: Produtos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Preco,Status")] Produto produto)
        {
            if (!UsuarioLogado())
            {
                return RedirectToAction("Login", "Usuarios");
            }
            if (ModelState.IsValid)
            {
                var usuario = GetUsuarioLogado();
                produto.IdUsuarioCadastro = usuario.Id;

                await _produtosService.Create(produto);
                return RedirectToAction(nameof(Index));
            }
            return View(produto);
        }

        // GET: Produtos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!UsuarioLogado())
            {
                return RedirectToAction("Login", "Usuarios");
            }
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _produtosService.Find(id);
            if (produto == null)
            {
                return NotFound();
            }
            return View(produto);
        }

        // POST: Produtos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Preco,Status,IdUsuarioCadastro")] Produto produto)
        {
            if (!UsuarioLogado())
            {
                return RedirectToAction("Login", "Usuarios");
            }
            if (id != produto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var usuario = GetUsuarioLogado();
                    produto.IdUsuarioUpdate = usuario.Id;

                    await _produtosService.Update(produto);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(produto);
        }

        // GET: Produtos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!UsuarioLogado())
            {
                return RedirectToAction("Login", "Usuarios");
            }
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _produtosService.Find(id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!UsuarioLogado())
            {
                return RedirectToAction("Login", "Usuarios");
            }
            await _produtosService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioLogado()
        {
            Usuario user = GetUsuarioLogado();
            return user != null;

        }

        private Usuario GetUsuarioLogado()
        {
            Usuario user = null;
            using (var storage = new LocalStorage())
            {
                if (storage.Exists("User"))
                    user = storage.Get<Usuario>("User");
            }
            return user;
        }
    }
}
