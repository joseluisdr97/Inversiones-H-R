using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MADERERA_HRS.DB;
using MADERERA_HRS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace MADERERA_HRS.Controllers
{
    [Authorize]
    public class AdministradorController : Controller
    {
        private AppContextDB context;
        private IConfiguration configuration;
        private IWebHostEnvironment hosting;
        public AdministradorController(AppContextDB context, IConfiguration configuration, IWebHostEnvironment hosting)
        {
            this.context = context;
            this.configuration = configuration;
            this.hosting = hosting;
        }
        [HttpGet]
        public IActionResult Index(string query)
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            List<Usuario> usuarios = null;
            if (query == null || query == "")
            {
                usuarios = context.Usuarios.Where(e => e.Estado_Eliminacion == false && e.Id_Rol==2 && e.Id_Usuario!=getlooged().Id_Usuario).ToList();
            }
            else
            {
                usuarios = context.Usuarios.Where(e => e.Estado_Eliminacion == false && e.Id_Rol == 2 && e.DNI.Contains(query) && e.Id_Usuario != getlooged().Id_Usuario).ToList();
            }
            return View(usuarios);
        }
        [HttpGet]
        public IActionResult Crear()
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            return View(new Usuario());
        }
        [HttpPost]
        public IActionResult Crear(Usuario usuario, IFormFile file, string RepitaContasenia)
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            ValidarUsuario(usuario, file, RepitaContasenia);
            if (ModelState.IsValid)
            {
                usuario.Contrasenia = CreateHash(usuario.Contrasenia);
                usuario.Imagen = SaveFile(file);
                usuario.Id_Rol = 2;
                usuario.Estado_Eliminacion = false;
                context.Usuarios.Add(usuario);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Categorias = context.Categorias.ToList();
            return View(usuario);
        }
        public IActionResult Eliminar(int id)
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            var usuarioDB = context.Usuarios.First(a => a.Id_Usuario == id);
            usuarioDB.Estado_Eliminacion = true;
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
        public void ValidarUsuario(Usuario usuario, IFormFile file, string RepitaContrasenia)
        {
            if (usuario.Nombre == null || usuario.Nombre == "")
                ModelState.AddModelError("Nombre", "El campo nombre es obligatorio");

            if (usuario.DNI == null || usuario.DNI == "")
                ModelState.AddModelError("DNI", "El DNI es obligatorio");
            if (usuario.DNI != null && !Regex.IsMatch(usuario.DNI, "^\\d+$"))
                ModelState.AddModelError("DNI", "El campo DNI solo acepta numeros");
            if (usuario.DNI != null && Regex.IsMatch(usuario.DNI, "^\\d+$") && usuario.DNI.Length != 8)
                ModelState.AddModelError("DNI", "El campo DNI debe de tener 8 numeros");

            if (usuario.Apodo == null || usuario.Apodo == "")
                ModelState.AddModelError("Apodo", "El campo apodo es obligatorio");

            if (usuario.Direccion == null || usuario.Direccion == "")
                ModelState.AddModelError("Direccion", "El campo direccion es obligatorio");

            if (usuario.Telefono == null || usuario.Telefono == "")
                ModelState.AddModelError("Telefono", "El campo telefono es obligatorio");

            if (usuario.Correo == null || usuario.Correo == "")
                ModelState.AddModelError("Correo", "El campo correo es obligatorio");
            if (usuario.Correo != null && !Regex.IsMatch(usuario.Correo, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z"))
                ModelState.AddModelError("Correo", "El formato debe ser de correo");
            if (usuario.Correo != null && Regex.IsMatch(usuario.Correo, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z"))
            {
                bool correoexiste = context.Usuarios.Any(a => a.Correo == usuario.Correo);
                if (correoexiste)
                    ModelState.AddModelError("Correo", "Este correo ya existe");
            }

            if (usuario.Contrasenia == null || usuario.Contrasenia == "")
                ModelState.AddModelError("Contrasenia", "El campo contraseña es obligatorio");
            if (RepitaContrasenia == null || RepitaContrasenia == "")
                ModelState.AddModelError("Contrasenia", "El campo confirme contraseña es obligatorio");
            if (RepitaContrasenia != usuario.Contrasenia && usuario.Contrasenia != null && RepitaContrasenia != null)
                ModelState.AddModelError("Contrasenia", "Las contraseñas no coinciden");

            if (file == null)
                ModelState.AddModelError("Imagen", "El campo imagen es obligatorio");
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
        private string CreateHash(string input)
        {
            input += configuration.GetValue<string>("Key");
            var sha = SHA512.Create();
            var bytes = Encoding.Default.GetBytes(input);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
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

