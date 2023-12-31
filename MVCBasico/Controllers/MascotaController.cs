﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Tamagochi.Context;
using Tamagochi.Models;
using Tamagochi.ViewModels;

namespace Tamagochi.Controllers
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
            // cionsigo los distintos tipos de mascotas posibles y las guardo en una lista
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
                return RedirectToAction(nameof(Profile));
            }

            TempData["Error"] = "Ocurrio un error al crear la mascota";
            return View();
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
                TempData["Error"] = "Ocurrio un Error al eliminar tu Mascota";
                RedirectToAction(nameof(Profile));
            }

            _context.Mascota.Remove(mascota);
            await _context.SaveChangesAsync();

            TempData["Error"] = "Mascota Eliminada Exitosamente.";
            return RedirectToAction(nameof(Profile));
        }
        
        private Mascota buscarMascota(List<Mascota> mascotas, int? idMascota)
        {
            //seteo la primer mascota como devolucion default
            Mascota res = mascotas[0];
            int contador = 1;
            if (idMascota != null) 
            {
                while (res.Id != idMascota && contador<mascotas.Count)
                {
                    if (mascotas[contador].Id == idMascota)
                    {
                        res = mascotas[contador];
                    }
                    contador++;
                }
            }

            return res;
        }

        // Recibe el ID de la mascota que quiere seleccionar el susario, si no seleccionó ninguna devuelve la primer mascota de la lista
        public async Task<IActionResult> Profile(int? id)
        {
            // Buscamos el usuario que está iniciado (en la cookie)
            int userId = int.Parse(User.FindFirstValue("IdUsuario"));
            // Buscamos una lista de todas las macotas del usuario
            var mascotas = await _context.Mascota.Where(m => m.UserID == userId).ToListAsync();
            
            if (mascotas.Count == 0)
            {
                TempData["Error"] = "Crea tu primer mascota!";
                return RedirectToAction(nameof(Create));
            }

            Mascota mascotaSelected = buscarMascota(mascotas, id);

            MascotaProfileViewModel vm = new MascotaProfileViewModel()
            {
                mascotaSeleccionada = mascotaSelected,
                listaMascotas = mascotas
            };

            return View(vm);
        }

        public async Task<IActionResult> Alimentar(int id)
        {
            var mascota_db = await _context.Mascota.FirstOrDefaultAsync(m => m.Id == id);

            if (mascota_db.Estado != Estado.SATISFECHO) {
                mascota_db.UltimaVezAlimentado = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                await _context.SaveChangesAsync();
                TempData["Alimentado"] = "ñam ñam delicius!";
            }
            else
            {
                TempData["Error"] = mascota_db.NombreMascota + " no tiene hambre!";
            }
            return RedirectToAction(nameof(Profile), new {id=id});
        }

        [HttpGet]
        public async Task<IActionResult> Renombrar(int id)
        {
            var mascota = await _context.Mascota.FirstOrDefaultAsync(m => m.Id == id);

            return View(mascota);
        }
        [HttpPost]
        public async Task<IActionResult> Renombrar(String nombreMascota, int id)
        {
            var mascota = await _context.Mascota.FirstOrDefaultAsync(m => m.Id == id);
            mascota.NombreMascota = nombreMascota;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Profile), new { id = id });
        }

        private bool MascotaExists(int id)
        {
          return _context.Mascota.Any(e => e.Id == id);
        }
    }
}
