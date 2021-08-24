using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MADERERA_HRS.DB;
using MADERERA_HRS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MADERERA_HRS.Controllers
{
    [Authorize]
    public class ProductoAdminController : Controller
    {
        private AppContextDB context;
        private IConfiguration configuration;
        private IWebHostEnvironment hosting;
        public ProductoAdminController(AppContextDB context, IConfiguration configuration, IWebHostEnvironment hosting)
        {
            this.context = context;
            this.configuration = configuration;
            this.hosting = hosting;
        }
        [HttpGet]
        public IActionResult Index(string query)
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            List<Producto> productos = null;
            if(query==null || query == "")
            {
                productos = context.Productos.Include(a => a.Categoria).Where(e=>e.Estado_Eliminacion==false).ToList();
            }
            else
            {
                productos = context.Productos.Include(a => a.Categoria).Where(p=>p.Nombre.Contains(query) && p.Estado_Eliminacion==false).ToList();
            }
            ViewBag.Categorias = context.Categorias.ToList();
            return View(productos);
        }
        [HttpGet]
        public IActionResult Crear()
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            ViewBag.Categorias = context.Categorias.ToList();
            return View(new Producto());
        }
        [HttpPost]
        public IActionResult Crear(Producto producto, IFormFile file, IFormFile file1, IFormFile file2)
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            ValidarProducto(producto, file, file1, file2, "CrearProducto");
            if (ModelState.IsValid)
            {
                producto.Imagen = SaveFile(file);
                producto.Imagen1 = SaveFile(file1);
                producto.Imagen2 = SaveFile(file2);
                producto.Estado_Eliminacion = false;
                context.Productos.Add(producto);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Categorias = context.Categorias.ToList();
            return View(producto);
        }
        [HttpGet]
        public IActionResult Editar(int id)
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            ViewBag.Categorias = context.Categorias.ToList();
            var producto = context.Productos.First(a => a.Id_Producto == id);
            ViewBag.Imagen = producto.Imagen;
            return View(producto);
        }
        [HttpPost]
        public IActionResult Editar(Producto producto, int id)
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            ValidarProducto(producto,null,null,null, "EditarProducto");
            if (ModelState.IsValid)
            {
                var productoDB = context.Productos.First(a => a.Id_Producto == id);
                productoDB.Id_Categoria = producto.Id_Categoria;
                productoDB.Nombre = producto.Nombre;
                productoDB.Medidas = producto.Medidas;
                productoDB.Precio = producto.Precio;
                productoDB.Descripcion = producto.Descripcion;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Categorias = context.Categorias.ToList();
            producto.Id_Producto = id;
            ViewBag.Imagen = context.Productos.First(a=>a.Id_Producto==id).Imagen;
            return View(producto);
        }
        [HttpGet]
        public IActionResult EditarImagen(int id)
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            ViewBag.Imagen = context.Productos.First(a => a.Id_Producto == id).Imagen;
            ViewBag.Imagen1 = context.Productos.First(a => a.Id_Producto == id).Imagen1;
            ViewBag.Imagen2 = context.Productos.First(a => a.Id_Producto == id).Imagen2;
            return View(id);
        }
        [HttpPost]
        public IActionResult EditarImagen(int id, IFormFile file, IFormFile file1, IFormFile file2)
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            if (file == null)
                ModelState.AddModelError("Imagen", "El campo imagen peincipal es obligatorio");
            if (file == null)
                ModelState.AddModelError("Imagen1", "El campo imagen secundaria 1 es obligatorio");
            if (file == null)
                ModelState.AddModelError("Imagen2", "El campo imagen secundaria 2 es obligatorio");
            if (ModelState.IsValid)
            {
                var productoDB = context.Productos.First(a => a.Id_Producto == id);
                productoDB.Imagen = SaveFile(file);
                productoDB.Imagen1 = SaveFile(file1);
                productoDB.Imagen2 = SaveFile(file2);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Imagen = context.Productos.First(a => a.Id_Producto == id).Imagen;
            return View(id);
        }
        public IActionResult Eliminar(int id)
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            var productoDB = context.Productos.First(a => a.Id_Producto == id);
            productoDB.Estado_Eliminacion = true;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        private string SaveFile(IFormFile file)
        {
            string relativePaht = null;
            if (file.Length > 0)
            {
                relativePaht = Path.Combine("files", file.FileName);//Nombre de la carpeta donde se guardara las imagenes
                var filePaht = Path.Combine(hosting.WebRootPath, relativePaht);
                var stream = new FileStream(filePaht, FileMode.Create);
                file.CopyTo(stream);
                stream.Close();

            }
            return "/" + relativePaht.Replace('\\', '/');
        }
        public void ValidarProducto(Producto producto, IFormFile file, IFormFile file1, IFormFile file2, string MetodoAValidar)
        {
            if (producto.Nombre == null || producto.Nombre == "")
                ModelState.AddModelError("Nombre", "El campo nombre es obligatorio");
            if (producto.Id_Categoria == 0)
                ModelState.AddModelError("Categoria", "El campo categoría es obligatorio");
            if (producto.Medidas == null || producto.Medidas == "")
                ModelState.AddModelError("Medidas", "El campo medidas es obligatorio");
            if (producto.Precio <= 0)
                ModelState.AddModelError("Precio", "El campo precio debe ser mayor a cero");
            if (MetodoAValidar == "CrearProducto")
            {
                if (file == null)
                    ModelState.AddModelError("Imagen", "El campo imagen pricipal es obligatorio");
                if (file == null)
                    ModelState.AddModelError("Imagen1", "El campo imagen secundaria 1 es obligatorio");
                if (file == null)
                    ModelState.AddModelError("Imagen2", "El campo imagen secundaria 2 es obligatorio");
            }
            if (producto.Descripcion == null || producto.Descripcion == "")
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
