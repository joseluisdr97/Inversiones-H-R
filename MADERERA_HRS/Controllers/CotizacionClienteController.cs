using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MADERERA_HRS.DB;
using MADERERA_HRS.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MADERERA_HRS.Controllers
{
    public class CotizacionClienteController : Controller
    {
        private AppContextDB context;
        private IWebHostEnvironment hosting;
        public CotizacionClienteController(AppContextDB context, IWebHostEnvironment hosting)
        {
            this.context = context;
            this.hosting = hosting;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        //INICIO AGREGAR UN PRODUCTO A MI CARRITO DE COTIZACION
        [HttpGet]
        public IActionResult Crear()
        {
            if (getlooged() != null)
            {
                ViewBag.carritodecotizaciones = context.Carrito_Cotizaciones.Where(a => a.Id_Usuario == getlooged().Id_Usuario).ToList();
                ViewBag.cantidadProdductosenCarrito = context.Carrito_Cotizaciones.Count(a => a.Id_Usuario == getlooged().Id_Usuario);
            }
            return View();
        }
        [HttpPost]
        public IActionResult Crear(Carrito_Cotizacion carrito_Cotizacion, IFormFile file)
        {
            if (!VerificarSiUsuarioEsCliente()) { return RedirectToAction("Logaut", "Auth"); }
            ValidarRegistrarProductoACarrito(carrito_Cotizacion, file);
            if (ModelState.IsValid)
            {
                carrito_Cotizacion.Id_Usuario = getlooged().Id_Usuario;
                carrito_Cotizacion.Imagen = SaveFile(file);
                context.Carrito_Cotizaciones.Add(carrito_Cotizacion);
                context.SaveChanges();
                return RedirectToAction("Crear");
            }
            ViewBag.carritodecotizaciones = context.Carrito_Cotizaciones.Where(a => a.Id_Usuario == getlooged().Id_Usuario).ToList();
            ViewBag.cantidadProdductosenCarrito = context.Carrito_Cotizaciones.Count(a => a.Id_Usuario == getlooged().Id_Usuario);
            return View(carrito_Cotizacion);
        }
        [HttpPost]
        public IActionResult ConfirmarCotizacionComoPedido(Cotizacion cotizacion, int idcotizacion)
        {
            if (!VerificarSiUsuarioEsCliente()) { return RedirectToAction("Logaut", "Auth"); }
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
                return RedirectToAction("MisCotizaciones", "Home");
            }
            return RedirectToAction("MisCotizaciones", "Home");

        }
        [HttpPost]
        public IActionResult AgregarProductoACotizacionCliente(Carrito_Cotizacion carrito_Cotizacion, IFormFile file, int idcotizacion)
        {
            if (!VerificarSiUsuarioEsCliente()) { return RedirectToAction("Logaut", "Auth"); }
            ValidarRegistrarProductoACarrito(carrito_Cotizacion, file);
            if (ModelState.IsValid && idcotizacion>0)
            {
                var CotizacionDB = context.Cotizaciones.First(a => a.Id_Cotizacion == idcotizacion);
                Detalle_Cotizacion detalle_Cotizacion = new Detalle_Cotizacion();
                detalle_Cotizacion.Id_Cotizacion = idcotizacion;
                detalle_Cotizacion.Producto = carrito_Cotizacion.Producto;
                detalle_Cotizacion.Cantidad = carrito_Cotizacion.Cantidad;
                detalle_Cotizacion.Precio = 0;
                detalle_Cotizacion.Sub_Total = 0;
                detalle_Cotizacion.Imagen = SaveFile(file);
                detalle_Cotizacion.Medidas = carrito_Cotizacion.Medidas;
                detalle_Cotizacion.Descripcion = carrito_Cotizacion.Descripcion;
                detalle_Cotizacion.Estado_Eliminacion = false;
                detalle_Cotizacion.Id_Estado = 3;
                context.Detalle_Cotizaciones.Add(detalle_Cotizacion);
                context.SaveChanges();
                return RedirectToAction("MisCotizaciones", "Home");
            }
            return RedirectToAction("MisCotizaciones", "Home");
        }
        public void ValidarRegistrarProductoACarrito(Carrito_Cotizacion carrito_Cotizacion, IFormFile file)
        {
            if (file == null)
                ModelState.AddModelError("Imagen", "El campo imagen es obligatorio");
            if (carrito_Cotizacion.Producto == null || carrito_Cotizacion.Producto == "")
                ModelState.AddModelError("Producto", "El campo nombre del producto es obligatorio");
            if (carrito_Cotizacion.Medidas == null || carrito_Cotizacion.Medidas == "")
                ModelState.AddModelError("Medidas", "El campo medidas del producto es obligatorio");
            if (carrito_Cotizacion.Descripcion == null || carrito_Cotizacion.Descripcion == "")
                ModelState.AddModelError("Descripcion", "El campo descripcion es obligatorio");
            if (carrito_Cotizacion.Cantidad < 1)
                ModelState.AddModelError("Cantidad", "El campo cantidad debe ser mayor a 1");
            if (carrito_Cotizacion.Cantidad < 1)
                ModelState.AddModelError("Cantidad", "El campo cantidad debe ser mayor a 1");
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
        //FIN DE AGREGAR AL CARRITO DE COTIZACIONES

        //REGISTRO DE COTIZACIÓN
        public IActionResult RegistrarCotizacion()
        {
            if (!VerificarSiUsuarioEsCliente()) { return RedirectToAction("Logaut", "Auth"); }
            var contieneproductosregistrados = context.Carrito_Cotizaciones.Count(a => a.Id_Usuario == getlooged().Id_Usuario);
            if (contieneproductosregistrados > 0)
            {
                Cotizacion cotizacion = new Cotizacion();
                cotizacion.Id_Estado = 3;
                cotizacion.Id_Usuario = getlooged().Id_Usuario;
                cotizacion.Fecha = DateTime.Now;
                cotizacion.Total = 0;
                cotizacion.Avance = 0;
                cotizacion.Estado_Eliminacion = false;
                cotizacion.Fecha_Entrega_Solicitada = DateTime.Now;
                cotizacion.Telefono_Comunicacion = "000000000";
                cotizacion.Direccion_Entrega = "Aún sin dirección";
                context.Cotizaciones.Add(cotizacion);
                context.SaveChanges();

                var carritodecotizaciones = context.Carrito_Cotizaciones.Where(a => a.Id_Usuario == getlooged().Id_Usuario).ToList();
                for (int i = 0; i < carritodecotizaciones.Count; i++)
                {
                    Detalle_Cotizacion detalle_Cotizacion = new Detalle_Cotizacion();
                    detalle_Cotizacion.Id_Cotizacion = cotizacion.Id_Cotizacion;
                    detalle_Cotizacion.Producto = carritodecotizaciones[i].Producto;
                    detalle_Cotizacion.Cantidad = carritodecotizaciones[i].Cantidad;
                    detalle_Cotizacion.Precio = 0;
                    detalle_Cotizacion.Sub_Total = 0;
                    detalle_Cotizacion.Imagen = carritodecotizaciones[i].Imagen;
                    detalle_Cotizacion.Medidas = carritodecotizaciones[i].Medidas;
                    detalle_Cotizacion.Descripcion = carritodecotizaciones[i].Descripcion;
                    detalle_Cotizacion.Estado_Eliminacion = false;
                    detalle_Cotizacion.Id_Estado = 3;
                    context.Detalle_Cotizaciones.Add(detalle_Cotizacion);
                    context.SaveChanges();
                    //Eliminar producto del carritocotización
                    context.Carrito_Cotizaciones.Remove(carritodecotizaciones[i]);
                    context.SaveChanges();
                }

                //CREAR UNA NOTIFICACIÓN LUEGO DE GUARDAR EL PEDIDO
                Notificacion notificacion = new Notificacion();
                notificacion.Id_Usuario = getlooged().Id_Usuario;
                notificacion.Fecha_Hora = DateTime.Now;
                notificacion.Mensaje = "Tienes una nueva cotización virtual del cliente " + context.Usuarios.First(a => a.Id_Usuario == getlooged().Id_Usuario).Nombre + ", comunicate de inmediato";
                context.Notificaciones.Add(notificacion);
                context.SaveChanges();

                return RedirectToAction("MisCotizaciones", "Home", new { nuevoregistro = true });

            }
            return RedirectToAction("Crear");
        }
        public IActionResult Eliminar(int idcotizacion)
        {
            if (!VerificarSiUsuarioEsCliente()) { return RedirectToAction("Logaut", "Auth"); }
            if (idcotizacion > 0)
            {
                var cotizacionDB = context.Cotizaciones.First(a => a.Id_Cotizacion == idcotizacion);
                cotizacionDB.Estado_Eliminacion = true;
                context.SaveChanges();
                return RedirectToAction("MisCotizaciones", "Home");
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
