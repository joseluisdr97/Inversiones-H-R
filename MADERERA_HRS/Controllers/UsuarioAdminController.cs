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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MADERERA_HRS.Controllers
{
    [Authorize]
    public class UsuarioAdminController : Controller
    {
        private AppContextDB context;
        private IConfiguration configuration;
        private IWebHostEnvironment hosting;
        public UsuarioAdminController(AppContextDB context, IConfiguration configuration, IWebHostEnvironment hosting)
        {
            this.context = context;
            this.configuration = configuration;
            this.hosting = hosting;
        }

        //ACTUALIZAR DATOS DE CUENTA - POST Y DE LA CONTRASEÑA
        [HttpGet]
        public IActionResult Cuenta()
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            var usuario = context.Usuarios.First(a => a.Id_Usuario == getlooged().Id_Usuario);
            return View(usuario);
        }
        [HttpPost]
        public IActionResult Cuenta(Usuario usuario)
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            ValidarUsuario(usuario, null, "ActualizarDatos");
            if (ModelState.IsValid)
            {
                var usuarioDB = context.Usuarios.First(a => a.Id_Usuario == getlooged().Id_Usuario);
                usuarioDB.Nombre = usuario.Nombre;
                usuarioDB.Apodo = usuario.Apodo;
                usuarioDB.DNI = usuario.DNI;
                usuarioDB.Direccion = usuario.Direccion;
                usuarioDB.Telefono = usuario.Telefono;
                context.SaveChanges();
                return RedirectToAction("EnProceso", "PedidoAdmin");
            }
            return View(usuario);
        }
        [HttpGet]
        public IActionResult CambiarContrasenia()
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            return View();
        }
        [HttpPost]
        public IActionResult CambiarContrasenia(string Actual, string Nueva, string RepitaNueva)
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            ValidarContrasenia(Actual, Nueva, RepitaNueva);
            if (ModelState.IsValid)
            {
                var usuarioDB = context.Usuarios.First(a => a.Id_Usuario == getlooged().Id_Usuario);
                usuarioDB.Contrasenia = CreateHash(Nueva);
                context.SaveChanges();
                return RedirectToAction("Logaut", "Auth");

            }
            return View();
        }
        //DE AQUÍ ABAJO LA GESTION DEL USUARIO
        [HttpGet]
        public IActionResult Index(string query)
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            List<Usuario> usuarios = null;
            if(query==null || query == "")
            {
                usuarios = context.Usuarios.Include(a => a.Rol).ToList();
            }
            else
            {
                usuarios = context.Usuarios.Include(a => a.Rol).Where(a=>a.DNI.Contains(query)).ToList();
            }
            return View(usuarios);
        }
        [HttpGet]
        public IActionResult EditarImagen()
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            ViewBag.Imagen = context.Usuarios.First(a => a.Id_Usuario == getlooged().Id_Usuario).Imagen;
            return View();
        }
        [HttpPost]
        public IActionResult EditarImagen(IFormFile file)
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            if (file == null)
                ModelState.AddModelError("Imagen", "El campo imagen es obligatorio");
            if (ModelState.IsValid)
            {
                var UsuarioDB = context.Usuarios.First(a => a.Id_Usuario == getlooged().Id_Usuario);
                UsuarioDB.Imagen = SaveFile(file);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Imagen = context.Usuarios.First(a => a.Id_Usuario == getlooged().Id_Usuario).Imagen;
            return View();
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
        public void ValidarContrasenia(string Actual, string Nueva, string RepitaNueva)
        {
            if (Actual == null || Actual == "")
                ModelState.AddModelError("Actual", "La contaseña actual es obligatorio");
            if (Nueva == null || Nueva == "")
                ModelState.AddModelError("Nueva", "La contaseña nueva es obligatorio");
            if (RepitaNueva == null || RepitaNueva == "")
                ModelState.AddModelError("RepitaNueva", "La confirmación es obligatorio");
            if (Actual != null && Actual != "")
            {
                Usuario usuario = context.Usuarios.First(a => a.Id_Usuario == getlooged().Id_Usuario);
                if (usuario.Contrasenia != CreateHash(Actual)) ModelState.AddModelError("Nueva", "Contraseña actual invalida");
            }
            if (Nueva != RepitaNueva) ModelState.AddModelError("Nueva", "Las contraseñas no coinciden");
        }
        public void ValidarUsuario(Usuario usuario, string RepitaContrasenia, string MetodoAValidar)
        {
            if (usuario.Nombre == null || usuario.Nombre == "")
                ModelState.AddModelError("Nombre", "El campo nombre es obligatorio");
            //if (usuario.Nombre != null && !Regex.IsMatch(usuario.Nombre, @"^[a-zA-Z ]*$"))
            //    ModelState.AddModelError("Nombre", "El campo nombre solo acepta letras");

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

            if (MetodoAValidar == "CrearUsuario")
            {
                if (usuario.Correo == null || usuario.Correo == "")
                    ModelState.AddModelError("Correo", "El campo nombre es obligatorio");
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
                    ModelState.AddModelError("Contrasenia", "El campo repita contraseña es obligatorio");
                if (RepitaContrasenia != usuario.Contrasenia && usuario.Contrasenia != null && RepitaContrasenia != null)
                    ModelState.AddModelError("Contrasenia", "Las contraseñas no coinciden");
            }
        }
        private string CreateHash(string input)
        {
            input += configuration.GetValue<string>("Key");
            var sha = SHA512.Create();
            var bytes = Encoding.Default.GetBytes(input);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
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
