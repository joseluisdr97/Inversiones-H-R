using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MADERERA_HRS.DB;
using MADERERA_HRS.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace MADERERA_HRS.Controllers
{
    public class AuthController : Controller
    {
        private AppContextDB context;
        private IConfiguration configuration;
        public AuthController(AppContextDB context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }
        [HttpGet]
        public IActionResult Login(bool nuevoregistro)
        {
            if (nuevoregistro) { TempData["NuevoRegistro"] = "Registro con exito, ahora inicia sesión"; }
            return View();
        }
        [HttpPost]
        public IActionResult Login(string Correo, string Contrasenia)
        {
            var usuario = context.Usuarios
               .FirstOrDefault(o => o.Correo == Correo && o.Contrasenia == CreateHash(Contrasenia));
            if (usuario == null)
            {
                TempData["AuthMessaje"] = "Correo o contraseña incorrectos";
                return RedirectToAction("Login");
            }
            else
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuario.Apodo, usuario.Correo),
                    new Claim(ClaimTypes.Email, usuario.Correo),
                    new Claim(ClaimTypes.Role, usuario.Id_Rol.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, usuario.Id_Usuario.ToString()),
                    new Claim(ClaimTypes.Rsa, usuario.Imagen)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                HttpContext.SignInAsync(claimsPrincipal);

                if (usuario.Id_Rol == 1)
                {
                    return RedirectToAction("Index", "Home");
                }
                if (usuario.Id_Rol == 2)
                {
                    return RedirectToAction("EnConstruccion", "PedidoPresencial");
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult Logaut()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Login");
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
