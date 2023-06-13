using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

            Console.WriteLine(UsuarioExists(usuario.NombreUsuario));

            if (usuario == null || _context.Usuarios == null)
            {
                return RedirectToAction(nameof(Registrar));
            }

            var usuario_db = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.NombreUsuario == usuario.NombreUsuario);


            if (usuario_db != null)
            {
                return RedirectToAction(nameof(Registrar));
            }

            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
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
                return RedirectToAction(nameof(Registrar));
            }

            if (!usuario_db.Contrasena.Equals(Contrasena))
            {
                return RedirectToAction(nameof(Registrar));
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Profile(Usuario usuario)
        {
            var mascotas = _context.Mascota.Where(m => m.UserID == usuario.UserID);

            if (mascotas == null) 
            { 

            }
            return View(mascotas);
        }

        private bool UsuarioExists(String username)
        {
          return _context.Usuarios.Any(e => e.NombreUsuario == username);
        }
    }
}