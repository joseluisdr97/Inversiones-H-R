using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MADERERA_HRS.Models;
using MADERERA_HRS.DB;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace MADERERA_HRS.Controllers
{
    public class HomeController : Controller
    {
        private AppContextDB context;
        private IConfiguration configuration;
        public HomeController(AppContextDB context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult IndexAdmin()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Productos(string query, int idcategoria)
        {
            ViewBag.Categorias = context.Categorias.ToList();
            List<Producto> productos = new List<Producto>();
            if (idcategoria > 0)
            {
                return View(context.Productos.Where(a => a.Estado_Eliminacion == false && a.Id_Categoria==idcategoria).ToList());
            }
            if(query!="" && query != null)
            {
                productos= context.Productos.Include(a=>a.Categoria).Where(a => a.Estado_Eliminacion == false && a.Nombre.Contains(query)).ToList();
                if (productos.Count == 0)
                {
                    productos = context.Productos.Include(a => a.Categoria).Where(a => a.Estado_Eliminacion == false && a.Categoria.Nombre.Contains(query)).ToList();
                    return View(productos);
                }
                return View(productos);
            }
            productos = context.Productos.Include(a => a.Categoria).Where(a => a.Estado_Eliminacion == false).ToList();
            return View(productos);
        }
        public IActionResult DetalleProducto(int idproducto)
        {
            if (idproducto > 0)
            {
                //OBTENER TOTAL DE CALIFICACIONES
                var calificaciones = context.Calificaciones.Where(a => a.Id_Producto == idproducto).ToList();
                int cont1estrella = 0, cont2estrella = 0, cont3estrella = 0, cont4estrella = 0, cont5estrella = 0;
                for(int i = 0; i < calificaciones.Count; i++)
                {
                    if (calificaciones[i].Numero_Calificacion == 1) { cont1estrella++; }
                    if (calificaciones[i].Numero_Calificacion == 2) { cont2estrella++; }
                    if (calificaciones[i].Numero_Calificacion == 3) { cont3estrella++; }
                    if (calificaciones[i].Numero_Calificacion == 4) { cont4estrella++; }
                    if (calificaciones[i].Numero_Calificacion == 5) { cont5estrella++; }
                }
                if (calificaciones.Count > 0)
                {
                    ViewBag.unaestrella = Convert.ToString(Convert.ToInt32((cont1estrella / calificaciones.Count) * 100)) + "%";
                    ViewBag.dosestrellas = Convert.ToString(Convert.ToInt32((cont2estrella / calificaciones.Count) * 100)) + "%";
                    ViewBag.tresestrellas = Convert.ToString(Convert.ToInt32((cont3estrella / calificaciones.Count) * 100)) + "%";
                    ViewBag.cuatroestrellas = Convert.ToString(Convert.ToInt32((cont4estrella / calificaciones.Count) * 100)) + "%";
                    ViewBag.cincoestrellas = Convert.ToString(Convert.ToInt32((cont5estrella / calificaciones.Count) * 100)) + "%";
                }
                else
                {
                    ViewBag.unaestrella = "0%";
                    ViewBag.dosestrellas = "0%";
                    ViewBag.tresestrellas = "0%";
                    ViewBag.cuatroestrellas = "0%";
                    ViewBag.cincoestrellas = "0%";
                }
                List<int> ncalificaciones = new List<int>();
                ncalificaciones.Add(cont1estrella); ncalificaciones.Add(cont2estrella); ncalificaciones.Add(cont3estrella); ncalificaciones.Add(cont4estrella); ncalificaciones.Add(cont5estrella);
                ViewBag.Calificaciones = calificaciones.Count;
                int calificaciontotal = 0;
                int valorcont = 0;
                if (calificaciones.Count > 0)
                {
                 for(int i = 0; i < ncalificaciones.Count; i++)
                    {
                        if (ncalificaciones[i] > calificaciontotal)
                        {
                            calificaciontotal = ncalificaciones[i];
                            valorcont = i + 1;
                        }
                    }
                    ViewBag.CalificacionTotal = valorcont;
                }
                else
                {
                    ViewBag.CalificacionTotal = 0;
                }

                var producto = context.Productos.First(a => a.Id_Producto == idproducto);
                return View(producto);
            }
            return RedirectToAction("Productos");
        }
        public IActionResult Contacto()
        {
            return View();
        }
        public IActionResult Prueba()
        {
            return View();
        }
        public IActionResult MisPedidos(bool nuevoregistro, int idestado)
        {
            if (!VerificarSiUsuarioEsCliente()) { return RedirectToAction("Logaut", "Auth"); }
            List<Pedido> pedidos = new List<Pedido>();
            if (idestado > 0)
            {
                pedidos = context.Pedidos.Include(a => a.Estado).Where(a => a.Id_Usuario == getlooged().Id_Usuario && a.Estado_Eliminacion == false && a.Id_Estado==idestado).ToList();
            }
            else
            {
                pedidos = context.Pedidos.Include(a => a.Estado).Where(a => a.Id_Usuario == getlooged().Id_Usuario && a.Estado_Eliminacion == false).ToList();
            }
            if (nuevoregistro) { TempData["NuevoRegistro"] = "Tu pedido se registró con exito"; }
            ViewBag.Estados = context.Estados.ToList();
            pedidos= pedidos.OrderByDescending(x => x.Fecha).ToList();
            return View(pedidos);
        }
        public IActionResult MisCotizaciones(bool nuevoregistro, int idestado)
        {
            if (!VerificarSiUsuarioEsCliente()) { return RedirectToAction("Logaut", "Auth"); }
            List<Cotizacion> cotizaciones = new List<Cotizacion>();
            if (idestado > 0)
            {
                cotizaciones = context.Cotizaciones.Include(a => a.Estado).Where(a => a.Id_Usuario == getlooged().Id_Usuario && a.Estado_Eliminacion == false && a.Id_Estado==idestado).ToList();
            }
            else
            {
                cotizaciones = context.Cotizaciones.Include(a => a.Estado).Where(a => a.Id_Usuario == getlooged().Id_Usuario && a.Estado_Eliminacion == false).ToList();
            }
            if (nuevoregistro) { TempData["NuevoRegistro"] = "Tu cotización se registró con exito, en instantes nos comunicaremos contigo"; }
            ViewBag.Estados = context.Estados.ToList();
            cotizaciones = cotizaciones.OrderByDescending(x => x.Fecha).ToList();
            return View(cotizaciones);
        }
        public IActionResult Consultas(string query)
        {
            if (query != null && query != "")
            {
                if(context.Pedidos_Presenciales.Include(a => a.Estado).Where(a => a.DNI == query && a.Estado_Eliminacion == false && a.Id_Estado!=5).Count() > 0)
                {
                    ViewBag.PedidosPresenciales = context.Pedidos_Presenciales.Include(a => a.Estado).Where(a => a.DNI == query && a.Estado_Eliminacion == false && a.Id_Estado != 5).ToList();
                }
            }
            return View();
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
        private bool VerificarSiUsuarioEsCliente()
        {
            if (getlooged() != null)
            {
                if (getlooged().Id_Rol == 1) { return true; }
                else { return false; }
            }
            return false;
        }
    }
}
