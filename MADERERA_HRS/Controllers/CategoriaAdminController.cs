using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MADERERA_HRS.DB;
using MADERERA_HRS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace MADERERA_HRS.Controllers
{
    [Authorize]
    public class CategoriaAdminController : Controller
    {
        private AppContextDB context;
        public CategoriaAdminController(AppContextDB context)
        {
            this.context = context;
        }
        [HttpGet]
        public IActionResult Index(string query)
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            List<Categoria> categorias = null;
            if (query == null || query == "")
            {
                categorias = context.Categorias.Where(e => e.Estado_Eliminacion == false).ToList();
            }
            else
            {
                categorias = context.Categorias.Where(p => p.Nombre.Contains(query) && p.Estado_Eliminacion == false).ToList();
            }
            return View(categorias);
        }
        [HttpGet]
        public IActionResult Crear()
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            return View(new Categoria());
        }
        [HttpPost]
        public IActionResult Crear(Categoria categoria)
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            ValidarCategoria(categoria);
            if (ModelState.IsValid)
            {
                categoria.Estado_Eliminacion = false;
                context.Categorias.Add(categoria);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoria);
        }
        [HttpGet]
        public IActionResult Editar(int id)
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            var categoriaDB = context.Categorias.First(a => a.Id_Categoria == id);
            return View(categoriaDB);
        }
        [HttpPost]
        public IActionResult Editar(Categoria categoria, int id)
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            ValidarCategoria(categoria);
            if (ModelState.IsValid)
            {
                var categoriaDB = context.Categorias.First(a => a.Id_Categoria == id);
                categoriaDB.Nombre = categoria.Nombre;
                categoriaDB.Descripcion = categoria.Descripcion;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            categoria.Id_Categoria = id;
            return View(categoria);
        }
        public IActionResult Eliminar(int id)
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            var categoriaDB = context.Categorias.First(a => a.Id_Categoria == id);
            categoriaDB.Estado_Eliminacion = true;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public void ValidarCategoria(Categoria categoria)
        {
            if (categoria.Nombre == null || categoria.Nombre == "")
                ModelState.AddModelError("Nombre", "El campo nombre es obligatorio");
            if (categoria.Descripcion == null || categoria.Descripcion == "")
                ModelState.AddModelError("Descripcion", "El campo descripción es obligatorio");
        }
        private Usuario getlooged()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return null;
            }
            var claims = HttpContext.User.Claims.First();
            var listaClaims = claims.Subject.Claims.ToList();
            var id_usuario = listaClaims[3].Value;
            var usuario = context.Usuarios.Find(Convert.ToInt32(id_usuario));
            return usuario;
        }
        private bool VerificarSiUsuarioEsAdministrador()
        {
            if (getlooged() != null)
            {
                if (getlooged().Id_Rol == 2) { return true; }
                else { return false; }
            }
            return false;
        }
    }
}