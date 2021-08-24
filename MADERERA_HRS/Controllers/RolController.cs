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
    public class RolController : Controller
    {
        private AppContextDB context;
        private IConfiguration configuration;
        public RolController(AppContextDB context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }
        [HttpGet]
        public IActionResult Index()
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            return View(context.Roles.Where(r=>r.Estado_Eliminacion==false).ToList());
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
