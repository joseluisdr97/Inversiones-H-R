using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MADERERA_HRS.DB;
using MADERERA_HRS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace MADERERA_HRS.Controllers
{
    public class UsuarioClienteController : Controller
    {
        private AppContextDB context;
        private IConfiguration configuration;
        public UsuarioClienteController(AppContextDB context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }
        [HttpGet]
        public IActionResult Registrarse()
        {
            return View(new Usuario());
        }
        [HttpPost]
        public IActionResult Registrarse(Usuario usuario, string RepitaContasenia)
        {
            ValidarUsuario(usuario, RepitaContasenia, "CrearUsuario");
            if (ModelState.IsValid)
            {
                usuario.Contrasenia = CreateHash(usuario.Contrasenia);
                usuario.Imagen = "Usuario cliente no tiene imagen";
                usuario.Id_Rol = 1;
                usuario.Estado_Eliminacion = false;
                context.Usuarios.Add(usuario);
                context.SaveChanges();
                return RedirectToAction("Login","Auth", new { nuevoregistro = true });
            }
            ViewBag.RepitaContrasenia = RepitaContasenia;
            return View(usuario);
        }
        [Authorize]
        [HttpGet]
        public IActionResult Cuenta()
        {
            var usuario = context.Usuarios.First(a => a.Id_Usuario == getlooged().Id_Usuario);
            return View(usuario);
        }
        [Authorize]
        [HttpPost]
        //ACTUALIZAR DATOS DE CUENTA - POST
        public IActionResult Cuenta(Usuario usuario)
        {
            ValidarUsuario(usuario, null, "ActualizarDatos");
            if (ModelState.IsValid)
            {
                var usuarioDB= context.Usuarios.First(a => a.Id_Usuario == getlooged().Id_Usuario);
                usuarioDB.Nombre = usuario.Nombre;
                usuarioDB.Apodo = usuario.Apodo;
                usuarioDB.DNI = usuario.DNI;
                usuarioDB.Direccion = usuario.Direccion;
                usuarioDB.Telefono = usuario.Telefono;
                context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(usuario);
        }
        [Authorize]
        [HttpGet]
        public IActionResult CambiarContrasenia()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public IActionResult CambiarContrasenia(string Actual, string Nueva, string RepitaNueva)
        {
            ValidarContrasenia(Actual, Nueva, RepitaNueva);
            if (ModelState.IsValid)
            {
                var usuarioDB = context.Usuarios.First(a => a.Id_Usuario == getlooged().Id_Usuario);
                usuarioDB.Contrasenia = CreateHash(Nueva);
                context.SaveChanges();
                return RedirectToAction("Logaut", "Auth");

            }

            //TempData["AuthMessaje"] = "Correo o contraseña incorrectos";
            return View();
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
                if (usuario.Contrasenia!=CreateHash(Actual)) ModelState.AddModelError("Nueva", "Contraseña actual invalida");
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
                    ModelState.AddModelError("Contrasenia", "El campo repita contraseña es obligatorio");
                if (RepitaContrasenia != usuario.Contrasenia && usuario.Contrasenia != null && RepitaContrasenia != null)
                    ModelState.AddModelError("Contrasenia", "Las contraseñas no coinciden");
            }
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
      
    }
}
