using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TPFinal.API.Data;
using TPFinal.API.Models;

namespace TPFinal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly Context _context;

        public UsuarioController(Context context)
        {
            _context = context;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // PUT: api/Usuarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Usuarios
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuarios", new { id = usuario.Id }, usuario);
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Usuario>> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }

        // POST: api/Usuarios/login
        [HttpPost("login")]
        public async Task<ActionResult<Usuario>> LoginUsuario(Usuario user)
        {
            var usuario = _context.Usuarios.Where(u => u.Nome == user.Nome && u.Status).Single();

            if (usuario == null)
            {
                return Unauthorized();
            }

            if (usuario.Senha == user.Senha)
            {
                return Ok(usuario);
            }
            else
            {
                return Unauthorized();
            }
        }
        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
    }
}