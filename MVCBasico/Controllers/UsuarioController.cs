﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tamagochi.Context;
using Tamagochi.Models;
using NuGet.Protocol.Core.Types;

namespace Tamagochi.Controllers
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
        
        // REGISTRA UN NUEVO USUARIO SI EL NOMBRE NO ESTA EN USO
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

            return RedirectToAction("Profile", "Mascota");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        //  context.Basket.Include(b => b.BasketItems).ThenInclude(bi => bi.Product).FirstOrDefault().Product


        // INICIA SESION DE USUARIO EXISTENTE, VALIDA CONTRASEÑA Y NOMBRE DE USUARIO
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(String NombreUsuario, String Contrasena)
        {
            var usuario_db = _context.Usuarios.Include(m=> m.Mascotas).ThenInclude(e => e.Estadisticas).FirstOrDefault(u => u.NombreUsuario == NombreUsuario);

            if (usuario_db == null || !usuario_db.Contrasena.Equals(Contrasena))
            {
                TempData["Error"] = "El nombre de usuario o la contraseña son incorrectos";
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
            
            usuario_db.actualizarEstadisticas();
            _context.Entry(usuario_db).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return RedirectToAction("Profile", "Mascota");
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            return RedirectToAction("Index", "Home");
        }
    }
}