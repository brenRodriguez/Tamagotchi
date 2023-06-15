using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCBasico.Context;
using MVCBasico.Models;

namespace MVCBasico.Controllers
{
    public class MascotaController : Controller
    {
        private readonly TamagochiDatabaseContext _context;

        public MascotaController(TamagochiDatabaseContext context)
        {
            _context = context;
        }

        // GET: Mascota
        public async Task<IActionResult> Index()
        {
              return View(await _context.Mascota.ToListAsync());
        }

        // GET: Mascota/Create
        public IActionResult Create()
        {
            ViewBag.TipoDeMascotaOptions = Enum.GetValues(typeof(TipoMascota))
            .Cast<TipoMascota>()
            .Select(e => new SelectListItem
            {
                Value = e.ToString(),
                Text = e.ToString()
            }).ToList();


            return View();
        }

        // string nombreUsuario = User.Identity.Name;
        // POST: Mascota/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(String nombreMascota, TipoMascota tipoDeMascota) 
        {
            int userId = int.Parse(User.FindFirstValue("IdUsuario"));
            Usuario usuario = await _context.Usuarios.FindAsync(userId);

            Mascota mascota = new Mascota(nombreMascota, tipoDeMascota, userId);
            mascota.Usuario = usuario;

            if (ModelState.IsValid)
            {
                _context.Add(mascota);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mascota);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Mascota == null)
            {
                return NotFound();
            }

            var mascota = await _context.Mascota
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mascota == null)
            {
                return NotFound();
            }

            return View(mascota);
        }

        // POST: Mascota/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Mascota == null)
            {
                return Problem("Entity set 'TamagochiDatabaseContext.Mascota'  is null.");
            }
            var mascota = await _context.Mascota.FindAsync(id);
            if (mascota != null)
            {
                _context.Mascota.Remove(mascota);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Profile()
        {
            int userId = int.Parse(User.FindFirstValue("IdUsuario"));
            var mascotas = await _context.Mascota.Where(m => m.Usuario.UserID == userId).ToListAsync();

            if (mascotas.Count == 0)
            {
                TempData["Error"] = "Crea tu primer mascota!";
                return RedirectToAction(nameof(Create));
            }
            return View(mascotas);
        }

        private bool MascotaExists(int id)
        {
          return _context.Mascota.Any(e => e.Id == id);
        }
    }
}
