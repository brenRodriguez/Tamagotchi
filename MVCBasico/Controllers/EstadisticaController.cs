﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tamagochi.Context;
using Tamagochi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Tamagochi.Controllers
{
    public class EstadisticaController : Controller
    {

        private readonly TamagochiDatabaseContext _context;

        public EstadisticaController(TamagochiDatabaseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Estadisticas()
        {
            int idUsuario = int.Parse(User.FindFirstValue("IdUsuario"));
            Usuario user = await _context.Usuarios.FirstOrDefaultAsync(u => u.UserID == idUsuario);

            if (user == null)
            {
                return RedirectToAction("Profile", "Mascota");
            }


            var mascotas = await _context.Mascota.Where(m => m.UserID == idUsuario).ToListAsync();
            List<Estadistica> estadisticas = new List<Estadistica>();

            foreach(Mascota m in mascotas)
            {
                estadisticas.Add(m.Estadisticas);
            }

            return View(estadisticas);
        }
    }
}
