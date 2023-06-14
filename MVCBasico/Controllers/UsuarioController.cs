using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCBasico.Context;
using MVCBasico.Models;
using NuGet.Protocol.Core.Types;

namespace MVCBasico.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly TamagochiDatabaseContext _context;

        public UsuarioController(TamagochiDatabaseContext context)
        {
            _context = context;
        }

        // GET: Usuario
        public async Task<IActionResult> Index()
        {
              return View(await _context.Usuarios.ToListAsync());
        }

        // GET: Usuario/Create
        public IActionResult Registrar()
        {
            return View();
        }

        // POST: Usuario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registrar([Bind("UserID,NombreUsuario,Contrasena")] Usuario usuario)
        {
            if (usuario == null || _context.Usuarios == null)
            {
                TempData["Error"] = "Los datos ingresados son Invalidos.";
                return View();
            }

            var usuarioExists = await _context.Usuarios
                .AnyAsync(u => u.NombreUsuario == usuario.NombreUsuario);

            if (usuarioExists)
            {
                TempData["Error"] = "El nombre de usuario ya está en uso";
                return View();
            }

            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                await Login(usuario.NombreUsuario, usuario.Contrasena);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(String NombreUsuario, String Contrasena)
        {
            var usuario_db = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.NombreUsuario == NombreUsuario);

            if (usuario_db == null)
            {
                TempData["Error"] = "El nombre de usuario no existe.";
                return RedirectToAction(nameof(Login));
            }

            if (!usuario_db.Contrasena.Equals(Contrasena))
            {
                TempData["Error"] = "La contraseña es incorrecta.";
                return RedirectToAction(nameof(Login));
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario_db.NombreUsuario),
                new Claim("IdUsuario", usuario_db.UserID.ToString()),
            };

            var identity = new ClaimsIdentity(claims, "Cookies");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("Cookies", principal);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Profile(Usuario usuario)
        {
            var mascotas = _context.Mascota.Where(m => m.Usuario.UserID == usuario.UserID).ToListAsync();

            if (mascotas == null)
            {

            }
            return View(mascotas);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            return RedirectToAction("Index");
        }

        private bool UsuarioExists(String username)
        {
          return _context.Usuarios.Any(e => e.NombreUsuario == username);
        }
    }
}