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
    public class CotizacionAdminController : Controller
    {
        private AppContextDB context;
        private IConfiguration configuration;
        private IWebHostEnvironment hosting;
        public CotizacionAdminController(AppContextDB context, IConfiguration configuration, IWebHostEnvironment hosting)
        {
            this.context = context;
            this.configuration = configuration;
            this.hosting = hosting;
        }
        [HttpGet]
        public IActionResult NuevasCotizaciones(string query)
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            List<Cotizacion> cotizaciones = new List<Cotizacion>();
            if(query==null || query == "")
            {
                cotizaciones = context.Cotizaciones.Include(a => a.Usuario).Include(a => a.Estado).Where(a => a.Estado_Eliminacion == false && a.Id_Estado==3).ToList();
            }
            else
            {
                cotizaciones = context.Cotizaciones.Include(a => a.Usuario).Include(a => a.Estado).Where(a => a.Estado_Eliminacion == false && a.Usuario.DNI.Contains(query) && a.Id_Estado == 3).ToList();
            }
            return View(cotizaciones);
        }
        [HttpGet]
        public IActionResult CotizacionesFinalizadas(string query)
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            List<Cotizacion> cotizaciones = new List<Cotizacion>();
            if (query == null || query == "")
            {
                cotizaciones = context.Cotizaciones.Include(a => a.Usuario).Include(a => a.Estado).Where(a => a.Estado_Eliminacion == false && a.Id_Estado == 6).ToList();
            }
            else
            {
                cotizaciones = context.Cotizaciones.Include(a => a.Usuario).Include(a => a.Estado).Where(a => a.Estado_Eliminacion == false && a.Usuario.DNI.Contains(query) && a.Id_Estado == 6).ToList();
            }
            return View(cotizaciones);
        }
        [HttpGet]
        public IActionResult FinalizarCotizacion(int idcotizacion)
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            if (idcotizacion > 0)
            {
                var CotizacionDB = context.Cotizaciones.First(a => a.Id_Cotizacion == idcotizacion);
                CotizacionDB.Id_Estado = 6;
                context.SaveChanges();
                var detalles_Cotizacion = context.Detalle_Cotizaciones.Where(a => a.Id_Cotizacion == idcotizacion && a.Estado_Eliminacion == false).ToList();
                for (int i = 0; i < detalles_Cotizacion.Count; i++)
                {
                    detalles_Cotizacion[i].Id_Estado = 6;
                    context.SaveChanges();
                }
                return RedirectToAction("NuevasCotizaciones");
            }
            return View();
        }
        [HttpGet]
        public IActionResult NuevosPedidosCotizados(string query)
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            List<Cotizacion> cotizaciones = new List<Cotizacion>();
            if (query == null || query == "")
            {
                cotizaciones = context.Cotizaciones.Include(a => a.Usuario).Include(a => a.Estado).Where(a => a.Estado_Eliminacion == false && a.Id_Estado == 1).ToList();
            }
            else
            {
                cotizaciones = context.Cotizaciones.Include(a => a.Usuario).Include(a => a.Estado).Where(a => a.Estado_Eliminacion == false && a.Usuario.DNI.Contains(query) && a.Id_Estado == 1).ToList();
            }
            return View(cotizaciones);
        }
        [HttpGet]
        public IActionResult EnConstruccion(string query)
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            List<Cotizacion> cotizaciones = new List<Cotizacion>();
            if (query == null || query == "")
            {
                cotizaciones = context.Cotizaciones.Include(a => a.Usuario).Include(a => a.Estado).Where(a => a.Estado_Eliminacion == false && a.Id_Estado == 2).ToList();
            }
            else
            {
                cotizaciones = context.Cotizaciones.Include(a => a.Usuario).Include(a => a.Estado).Where(a => a.Estado_Eliminacion == false && a.Usuario.DNI.Contains(query) && a.Id_Estado == 2).ToList();
            }
            return View(cotizaciones);
        }
        [HttpGet]
        public IActionResult Terminados(string query)
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            List<Cotizacion> cotizaciones = new List<Cotizacion>();
            if (query == null || query == "")
            {
                cotizaciones = context.Cotizaciones.Include(a => a.Usuario).Include(a => a.Estado).Where(a => a.Estado_Eliminacion == false && a.Id_Estado == 4).ToList();
            }
            else
            {
                cotizaciones = context.Cotizaciones.Include(a => a.Usuario).Include(a => a.Estado).Where(a => a.Estado_Eliminacion == false && a.Usuario.DNI.Contains(query) && a.Id_Estado == 4).ToList();
            }
            return View(cotizaciones);
        }
        [HttpGet]
        public IActionResult Entregados(string query)
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            List<Cotizacion> cotizaciones = new List<Cotizacion>();
            if (query == null || query == "")
            {
                cotizaciones = context.Cotizaciones.Include(a => a.Usuario).Include(a => a.Estado).Where(a => a.Estado_Eliminacion == false && a.Id_Estado == 5).ToList();
            }
            else
            {
                cotizaciones = context.Cotizaciones.Include(a => a.Usuario).Include(a => a.Estado).Where(a => a.Estado_Eliminacion == false && a.Usuario.DNI.Contains(query) && a.Id_Estado == 5).ToList();
            }
            cotizaciones = cotizaciones.OrderByDescending(a => a.Fecha_Entrega_Solicitada).ToList();
            return View(cotizaciones);
        }
        [HttpGet]
        public IActionResult Eliminar( int idcotizacion, string Vista)
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            if (idcotizacion > 0)
            {
                var cotizacioDB = context.Cotizaciones.First(a => a.Id_Cotizacion == idcotizacion);
                cotizacioDB.Estado_Eliminacion = true;
                context.SaveChanges();
                return RedirectToAction(Vista);
            }
            return View();
        }
        [HttpPost]
        public IActionResult ConfirmarCotizacionComoPedido(Cotizacion cotizacion, int idcotizacion)
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            if (idcotizacion > 0)
            {
                if (cotizacion.Fecha_Entrega_Solicitada != null && cotizacion.Telefono_Comunicacion != null && cotizacion.Direccion_Entrega != null)
                {
                    var cotizacionDB = context.Cotizaciones.First(a => a.Id_Cotizacion == idcotizacion);
                    cotizacionDB.Fecha_Entrega_Solicitada = cotizacion.Fecha_Entrega_Solicitada;
                    cotizacionDB.Telefono_Comunicacion = cotizacion.Telefono_Comunicacion;
                    cotizacionDB.Direccion_Entrega = cotizacion.Direccion_Entrega;
                    cotizacionDB.Id_Estado = 1;
                    context.SaveChanges();
                    var detalles_Cotizacion = context.Detalle_Cotizaciones.Where(a => a.Id_Cotizacion == idcotizacion && a.Estado_Eliminacion == false).ToList();
                    for (int i = 0; i < detalles_Cotizacion.Count; i++)
                    {
                        detalles_Cotizacion[i].Id_Estado = 1;
                        context.SaveChanges();
                    }
                }
                return RedirectToAction("CotizacionesFinalizadas", "CotizacionAdmin");
            }
            return RedirectToAction("CotizacionesFinalizadas", "CotizacionAdmin");

        }
        [HttpPost]
        public IActionResult AgregarProductoAPedido(Detalle_Cotizacion detalle_Cotizacion, IFormFile file, int idcotizacion, string vista)
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            if (idcotizacion > 0)
            {
                ValidarProductoAlAgregarACotizacion(detalle_Cotizacion, file);
                if (ModelState.IsValid)
                {
                    detalle_Cotizacion.Id_Cotizacion = idcotizacion;
                    detalle_Cotizacion.Sub_Total = detalle_Cotizacion.Cantidad * detalle_Cotizacion.Precio;
                    detalle_Cotizacion.Imagen = SaveFile(file);
                    detalle_Cotizacion.Estado_Eliminacion = false;
                    detalle_Cotizacion.Id_Estado = 1;
                    context.Detalle_Cotizaciones.Add(detalle_Cotizacion);
                    context.SaveChanges();
                    var detalles_Cotizacion = context.Detalle_Cotizaciones.Where(a => a.Id_Cotizacion == idcotizacion && a.Estado_Eliminacion == false).ToList();
                    if (detalles_Cotizacion.Count > 0)
                    {
                        decimal Total = 0;
                        for(int i = 0; i < detalles_Cotizacion.Count; i++)
                        {
                            Total = Total + detalles_Cotizacion[i].Sub_Total;
                        }
                        var cotizacionDB = context.Cotizaciones.First(a => a.Id_Cotizacion == idcotizacion);
                        cotizacionDB.Total = Total;
                        context.SaveChanges();
                    }
                    return RedirectToAction(vista);
                }
            }
            return RedirectToAction(vista);
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
        public void ValidarProductoAlAgregarACotizacion(Detalle_Cotizacion detalle_Cotizacion, IFormFile file)
        {
            if (detalle_Cotizacion.Producto == null || detalle_Cotizacion.Producto == "")
                ModelState.AddModelError("Producto", "El campo producto es obligatorio");
            if (detalle_Cotizacion.Cantidad <= 0)
                ModelState.AddModelError("Cantidad", "El campo cantidad es obligatorio");
            if (detalle_Cotizacion.Medidas == null || detalle_Cotizacion.Medidas == "")
                ModelState.AddModelError("Medidas", "El campo medidas es obligatorio");
            if (detalle_Cotizacion.Precio <= 0)
                ModelState.AddModelError("Precio", "El campo precio debe ser mayor a cero");
            if (file == null)
                ModelState.AddModelError("Imagen", "El campo imagen es obligatorio");
            if (detalle_Cotizacion.Descripcion == null || detalle_Cotizacion.Descripcion == "")
                ModelState.AddModelError("Descripcion", "El campo descripción es obligatorio");
        }
        public IActionResult IniciarConstruccion(int idcotizacion)
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            if (idcotizacion > 0)
            {
                var cotizacionDB = context.Cotizaciones.First(a => a.Id_Cotizacion == idcotizacion);
                cotizacionDB.Id_Estado = 2;
                context.SaveChanges();
                var detalles_Cotizacion = context.Detalle_Cotizaciones.Where(a => a.Id_Cotizacion == idcotizacion && a.Estado_Eliminacion==false).ToList();
                for(int i = 0; i < detalles_Cotizacion.Count; i++)
                {
                    detalles_Cotizacion[i].Id_Estado = 2;
                    context.SaveChanges();
                }
            }
            return RedirectToAction("NuevosPedidosCotizados");
        }
        [HttpGet]
        public IActionResult CambiarPorcentajeConstruccion(int idcotizacion, int porcentaje)
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            if (idcotizacion > 0 && porcentaje >= 0 && porcentaje <= 100)
            {
                Cotizacion cotizacionDB = context.Cotizaciones.First(a => a.Id_Cotizacion == idcotizacion);
                cotizacionDB.Avance = porcentaje;
                context.SaveChanges();
            }
            return RedirectToAction("EnConstruccion");
        }
        [HttpGet]
        public IActionResult FinalizarConstruccion(int idcotizacion)
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            if (idcotizacion > 0)
            {
                var cotizacionDB = context.Cotizaciones.First(a => a.Id_Cotizacion == idcotizacion);
                cotizacionDB.Id_Estado = 4;
                cotizacionDB.Avance = 100;
                context.SaveChanges();
                var detalles_Cotizacion = context.Detalle_Cotizaciones.Where(a => a.Id_Cotizacion == idcotizacion && a.Estado_Eliminacion == false).ToList();
                for (int i = 0; i < detalles_Cotizacion.Count; i++)
                {
                    detalles_Cotizacion[i].Id_Estado = 4;
                    context.SaveChanges();
                }
            }
            return RedirectToAction("EnConstruccion");
        }
        [HttpGet]
        public IActionResult EntregarPedido(int idcotizacion)
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            if (idcotizacion > 0)
            {
                var cotizacionDB = context.Cotizaciones.First(a => a.Id_Cotizacion == idcotizacion);
                cotizacionDB.Id_Estado = 5;
                cotizacionDB.Fecha_Entrega_Solicitada = DateTime.Now;
                context.SaveChanges();
                var detalles_Cotizacion = context.Detalle_Cotizaciones.Where(a => a.Id_Cotizacion == idcotizacion && a.Estado_Eliminacion == false).ToList();
                for (int i = 0; i < detalles_Cotizacion.Count; i++)
                {
                    detalles_Cotizacion[i].Id_Estado = 5;
                    context.SaveChanges();
                }
            }
            return RedirectToAction("Terminados");
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
