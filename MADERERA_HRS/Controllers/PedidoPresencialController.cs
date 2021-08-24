using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MADERERA_HRS.DB;
using MADERERA_HRS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MADERERA_HRS.Controllers
{
    [Authorize]
    public class PedidoPresencialController : Controller
    {
        private AppContextDB context;
        public PedidoPresencialController(AppContextDB context)
        {
            this.context = context;
        }
        [HttpGet]
        public IActionResult AgregarProductoACarrito(int secreopedidoconexito)
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            ViewBag.Carrito_Pedidos = context.Carrito_Pedidos_Presenciales.ToList();
            ViewBag.cantidadProdductosenCarrito = context.Carrito_Pedidos_Presenciales.Count();
            return View(new Carrito_Pedido_Presencial());
        }
        [HttpPost]
        public IActionResult AgregarProductoACarrito(Carrito_Pedido_Presencial carrito_Pedido_Presencial)
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            ValidarCarritoPedido(carrito_Pedido_Presencial);
            if (ModelState.IsValid)
            {
                carrito_Pedido_Presencial.Sub_Total = carrito_Pedido_Presencial.Precio * carrito_Pedido_Presencial.Cantidad;
                context.Carrito_Pedidos_Presenciales.Add(carrito_Pedido_Presencial);
                context.SaveChanges();
                TempData["siseregistro"] = "Tu producto se registró con exito";
                return RedirectToAction("AgregarProductoACarrito");
            }
            ViewBag.Carrito_Pedidos = context.Carrito_Pedidos_Presenciales.ToList();
            ViewBag.cantidadProdductosenCarrito = context.Carrito_Pedidos_Presenciales.Count();
            return View(carrito_Pedido_Presencial);
        }
        public void ValidarCarritoPedido(Carrito_Pedido_Presencial carrito_Pedido_Presencial)
        {
            if (carrito_Pedido_Presencial.Producto == null || carrito_Pedido_Presencial.Producto == "")
                ModelState.AddModelError("Producto", "El campo nombre del producto es obligatorio");
            if (carrito_Pedido_Presencial.Medidas == null || carrito_Pedido_Presencial.Medidas == "")
                ModelState.AddModelError("Medidas", "El campo medidas del producto es obligatorio");
            if (carrito_Pedido_Presencial.Descripcion == null || carrito_Pedido_Presencial.Descripcion == "")
                ModelState.AddModelError("Descripcion", "El campo descripcion es obligatorio");
            if (carrito_Pedido_Presencial.Cantidad < 1)
                ModelState.AddModelError("Cantidad", "El campo cantidad debe ser mayor a 1");
            if (carrito_Pedido_Presencial.Precio < 1)
                ModelState.AddModelError("Precio", "El campo precio debe ser mayor a 1");
        }
        [HttpGet]
        public IActionResult CrearPedido()
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            ViewBag.Carrito_Pedidos = context.Carrito_Pedidos_Presenciales.ToList();
            return View(new Pedido_Presencial());
        }
        [HttpPost]
        public IActionResult CrearPedido( Pedido_Presencial pedido_Presencial)
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            ValidarPedido(pedido_Presencial);
            if (ModelState.IsValid)
            {
                pedido_Presencial.Id_Estado = 1;
                pedido_Presencial.Estado_Eliminacion = false;
                var productos_carrito = context.Carrito_Pedidos_Presenciales.ToList();
                decimal Total = 0;
                for(int i = 0; i < productos_carrito.Count(); i++)
                {
                    Total = Total + productos_carrito[i].Sub_Total;
                }
                pedido_Presencial.Total = Total;
                pedido_Presencial.Avance = 0;
                pedido_Presencial.Fecha = DateTime.Now;
                context.Pedidos_Presenciales.Add(pedido_Presencial);
                context.SaveChanges();

                for (int i = 0; i < productos_carrito.Count(); i++)
                {
                    Detalle_Pedido_Presencial detalle_pedido = new Detalle_Pedido_Presencial();
                    detalle_pedido.Id_Pedido_Presencial = pedido_Presencial.Id_Pedido_Presencial;
                    detalle_pedido.Producto = productos_carrito[i].Producto;
                    detalle_pedido.Cantidad = productos_carrito[i].Cantidad;
                    detalle_pedido.Precio = productos_carrito[i].Precio;
                    detalle_pedido.Sub_Total = productos_carrito[i].Sub_Total;
                    detalle_pedido.Estado_Eliminacion = false;
                    detalle_pedido.Id_Estado = 1;
                    detalle_pedido.Descripcion = productos_carrito[i].Descripcion;
                    detalle_pedido.Medidas = productos_carrito[i].Medidas;
                    context.Detalle_PedidosPresenciales.Add(detalle_pedido);
                    context.SaveChanges();
                    context.Carrito_Pedidos_Presenciales.Remove(productos_carrito[i]);
                    context.SaveChanges();
                }
                return RedirectToAction("Nuevos");
            }
            ViewBag.Carrito_Pedidos = context.Carrito_Pedidos_Presenciales.ToList();
            return View(pedido_Presencial);
        }
        public void ValidarPedido(Pedido_Presencial pedido_Presencial)
        {
            if (pedido_Presencial.Cliente == null || pedido_Presencial.Cliente == "")
                ModelState.AddModelError("Cliente", "El campo nombre del cliente es obligatorio");
            if (pedido_Presencial.DNI == null || pedido_Presencial.DNI == "")
                ModelState.AddModelError("DNI", "El campo DNI del cliente es obligatorio");
            if (pedido_Presencial.Fecha_Entrega_Solicitada == null || pedido_Presencial.Fecha_Entrega_Solicitada < DateTime.Now)
                ModelState.AddModelError("Fecha_Entrega_Solicitada", "El campo fecha de entrega es obligatorio");
            if (pedido_Presencial.Direccion_Entrega == null || pedido_Presencial.Direccion_Entrega == "")
                ModelState.AddModelError("Direccion_Entrega", "El campo dirección de entrega del pedido es obligatorio");
            if (pedido_Presencial.Telefono_Comunicacion == null || pedido_Presencial.Telefono_Comunicacion == "")
                ModelState.AddModelError("Telefono_Comunicacion", "El campo teléfono es obligatorio");
        }
        public IActionResult Nuevos(string query, bool mensaje)
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            if (mensaje)
            {
                TempData["Seagregoconexito"] = "Tu producto se agregó con exito a tu pedido - revisalo";
            }
            List<Pedido_Presencial> pedidos = new List<Pedido_Presencial>();
            if (query != null && query != "")
            {
                pedidos = context.Pedidos_Presenciales.Where(a => a.Estado_Eliminacion == false && a.Id_Estado == 1 && a.DNI.Contains(query)).ToList();
            }
            else
            {
                pedidos = context.Pedidos_Presenciales.Where(a => a.Estado_Eliminacion == false && a.Id_Estado == 1).ToList();
            }
            return View(pedidos);
        }
        public IActionResult EnConstruccion(string query, bool mensaje)
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            if (mensaje)
            {
                TempData["Seagregoconexito"] = "Tu producto se agregó con exito a tu pedido - revisalo";
            }
            List<Pedido_Presencial> pedidos = new List<Pedido_Presencial>();
            if (query != null && query != "")
            {
                pedidos = context.Pedidos_Presenciales.Where(a => a.Estado_Eliminacion == false && a.Id_Estado == 2 && a.DNI.Contains(query)).ToList();
            }
            else
            {
                pedidos = context.Pedidos_Presenciales.Where(a => a.Estado_Eliminacion == false && a.Id_Estado == 2).ToList();
            }
            return View(pedidos);
        }
        public IActionResult Terminados(string query, bool mensaje)
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            if (mensaje)
            {
                TempData["Seagregoconexito"] = "Tu producto se agregó con exito a tu pedido - revisalo";
            }
            List<Pedido_Presencial> pedidos = new List<Pedido_Presencial>();
            if (query != null && query != "")
            {
                pedidos = context.Pedidos_Presenciales.Where(a => a.Estado_Eliminacion == false && a.Id_Estado == 4 && a.DNI.Contains(query)).ToList();
            }
            else
            {
                pedidos = context.Pedidos_Presenciales.Where(a => a.Estado_Eliminacion == false && a.Id_Estado == 4).ToList();
            }
            return View(pedidos);
        }
        public IActionResult Entregados(string query, bool mensaje)
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            if (mensaje)
            {
                TempData["Seagregoconexito"] = "Tu producto se agregó con exito a tu pedido - revisalo";
            }
            List<Pedido_Presencial> pedidos = new List<Pedido_Presencial>();
            if (query != null && query != "")
            {
                pedidos = context.Pedidos_Presenciales.Where(a => a.Estado_Eliminacion == false && a.Id_Estado == 5 && a.DNI.Contains(query)).ToList();
            }
            else
            {
                pedidos = context.Pedidos_Presenciales.Where(a => a.Estado_Eliminacion == false && a.Id_Estado == 5).ToList();
            }
            pedidos = pedidos.OrderByDescending(x => x.Fecha_Entrega_Solicitada).ToList();
            return View(pedidos);
        }
        [HttpGet]
        public IActionResult Eliminar(int idpedido, string Vista)
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            if (idpedido > 0)
            {
                var pedidoDB = context.Pedidos_Presenciales.First(a => a.Id_Pedido_Presencial == idpedido);
                pedidoDB.Estado_Eliminacion = true;
                context.SaveChanges();
            }
            return RedirectToAction(Vista);
        }
        [HttpGet]
        public IActionResult IniciarConstruccion(int idpedido)
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            if (idpedido > 0)
            {
                var pedidoDB = context.Pedidos_Presenciales.First(a => a.Id_Pedido_Presencial == idpedido);
                pedidoDB.Id_Estado = 2;
                context.SaveChanges();
                var detalles_Pedido = context.Detalle_PedidosPresenciales.Where(a => a.Id_Pedido_Presencial == idpedido && a.Estado_Eliminacion == false).ToList();
                for (int i = 0; i < detalles_Pedido.Count; i++)
                {
                    detalles_Pedido[i].Id_Estado = 2;
                    context.SaveChanges();
                }
            }
            return RedirectToAction("Nuevos");
        }
        [HttpGet]
        public IActionResult FinalizarConstruccion(int idpedido)
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            if (idpedido > 0)
            {
                var pedidoDB = context.Pedidos_Presenciales.First(a => a.Id_Pedido_Presencial == idpedido);
                pedidoDB.Id_Estado = 4;
                pedidoDB.Avance = 100;
                context.SaveChanges();
                var detalles_Pedido = context.Detalle_PedidosPresenciales.Where(a => a.Id_Pedido_Presencial == idpedido && a.Estado_Eliminacion == false).ToList();
                for (int i = 0; i < detalles_Pedido.Count; i++)
                {
                    detalles_Pedido[i].Id_Estado = 4;
                    context.SaveChanges();
                }
            }
            return RedirectToAction("EnConstruccion");
        }
        [HttpGet]
        public IActionResult EntregarPedido(int idpedido)
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            if (idpedido > 0)
            {
                var pedidoDB = context.Pedidos_Presenciales.First(a => a.Id_Pedido_Presencial == idpedido);
                pedidoDB.Id_Estado = 5;
                pedidoDB.Fecha_Entrega_Solicitada = DateTime.Now;
                context.SaveChanges();
                var detalles_Pedido = context.Detalle_PedidosPresenciales.Where(a => a.Id_Pedido_Presencial == idpedido && a.Estado_Eliminacion == false).ToList();
                for (int i = 0; i < detalles_Pedido.Count; i++)
                {
                    detalles_Pedido[i].Id_Estado = 5;
                    context.SaveChanges();
                }
            }
            return RedirectToAction("Terminados");
        }
        [HttpPost]
        public IActionResult AgregarProductoAPedido(Detalle_Pedido_Presencial detalle_Pedido_Presencial, int idpedido, string vista)
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            if (idpedido > 0)
            {
                ValidarProductoAlAgregarAPedido(detalle_Pedido_Presencial);
                if (ModelState.IsValid)
                {
                    var pedidoDB = context.Pedidos_Presenciales.First(a => a.Id_Pedido_Presencial == idpedido);
                    detalle_Pedido_Presencial.Id_Pedido_Presencial = idpedido;
                    detalle_Pedido_Presencial.Sub_Total = detalle_Pedido_Presencial.Precio*detalle_Pedido_Presencial.Cantidad;
                    detalle_Pedido_Presencial.Estado_Eliminacion = false;
                    detalle_Pedido_Presencial.Id_Estado = pedidoDB.Id_Estado;
                    context.Detalle_PedidosPresenciales.Add(detalle_Pedido_Presencial);
                    context.SaveChanges();
                    var detalles_Pedido = context.Detalle_PedidosPresenciales.Where(a => a.Id_Pedido_Presencial == idpedido && a.Estado_Eliminacion == false).ToList();
                    if (detalles_Pedido.Count > 0)
                    {
                        decimal Total = 0;
                        for (int i = 0; i < detalles_Pedido.Count; i++)
                        {
                            Total = Total + detalles_Pedido[i].Sub_Total;
                        }
                        pedidoDB.Total = Total;
                        context.SaveChanges();
                    }
                    return RedirectToAction(vista,new { mensaje = true});
                }
            }
            return RedirectToAction(vista);
        }
        [HttpGet]
        public IActionResult CambiarPorcentajeConstruccion(int idpedido, int porcentaje)
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            if (idpedido > 0 && porcentaje >= 0 && porcentaje <= 100)
            {
                Pedido_Presencial pedidoDB = context.Pedidos_Presenciales.First(a => a.Id_Pedido_Presencial == idpedido);
                pedidoDB.Avance = porcentaje;
                context.SaveChanges();
            }
            return RedirectToAction("EnConstruccion");
        }
        public void ValidarProductoAlAgregarAPedido(Detalle_Pedido_Presencial detalle_Pedido_Presencial)
        {
            if (detalle_Pedido_Presencial.Producto == null || detalle_Pedido_Presencial.Producto == "")
                ModelState.AddModelError("Producto", "El campo nombre del producto es obligatorio");
            if (detalle_Pedido_Presencial.Medidas == null || detalle_Pedido_Presencial.Medidas == "")
                ModelState.AddModelError("Medidas", "El campo medidas del producto es obligatorio");
            if (detalle_Pedido_Presencial.Descripcion == null || detalle_Pedido_Presencial.Descripcion == "")
                ModelState.AddModelError("Descripcion", "El campo descripcion es obligatorio");
            if (detalle_Pedido_Presencial.Cantidad < 1)
                ModelState.AddModelError("Cantidad", "El campo cantidad debe ser mayor a 1");
            if (detalle_Pedido_Presencial.Precio < 1)
                ModelState.AddModelError("Precio", "El campo precio debe ser mayor a 1");
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
