using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MADERERA_HRS.DB;
using MADERERA_HRS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MADERERA_HRS.Controllers
{
    public class PedidoClienteController : Controller
    {
        private AppContextDB context;
        public PedidoClienteController(AppContextDB context)
        {
            this.context = context;
        }
        [HttpGet]
        public IActionResult Crear()
        {
            if (!VerificarSiUsuarioEsCliente()) { return RedirectToAction("Logaut", "Auth"); }
            decimal Total = 0;
            var carritodepedidos = context.Carrito_Pedidos.Where(a => a.Id_Usuario == getlooged().Id_Usuario).ToList();
            if (carritodepedidos.Count > 0)
            {
                for (int i = 0; i < carritodepedidos.Count; i++)
                {
                    Total = Total + carritodepedidos[i].Sub_Total;
                }
            }
            ViewBag.cantidadProdductosenCarrito = carritodepedidos.Count;
            ViewBag.Total = Total;
            ViewBag.carritodepedidos = context.Carrito_Pedidos.Include(a => a.Producto).Where(a => a.Id_Usuario == getlooged().Id_Usuario).ToList();
            return View(new Carrito_Pedido());
        }
        [HttpPost]
        public IActionResult Crear(Pedido pedido)
        {
            if (!VerificarSiUsuarioEsCliente()) { return RedirectToAction("Logaut", "Auth"); }
            ValidarPedido(pedido);
            if (ModelState.IsValid)
            {
                decimal Total=0;
                var carritodepedidos = context.Carrito_Pedidos.Where(a => a.Id_Usuario == getlooged().Id_Usuario).ToList();
                for(int i = 0; i < carritodepedidos.Count; i++)
                {
                    Total = Total + carritodepedidos[i].Sub_Total;
                }
                pedido.Id_Estado = 1;//Estado reservado
                pedido.Id_Usuario = getlooged().Id_Usuario;
                pedido.Fecha = DateTime.Now;
                pedido.Total = Total;
                pedido.Avance = 0;
                pedido.Estado_Eliminacion = false;
                context.Pedidos.Add(pedido);
                context.SaveChanges();
                
                for (int i = 0; i < carritodepedidos.Count; i++)
                {
                    Detalle_Pedido detalle_Pedido = new Detalle_Pedido();
                    detalle_Pedido.Id_Pedido = pedido.Id_Pedido;
                    detalle_Pedido.Id_Producto = carritodepedidos[i].Id_Producto;
                    detalle_Pedido.Cantidad = carritodepedidos[i].Cantidad;
                    detalle_Pedido.Precio = carritodepedidos[i].Precio;
                    detalle_Pedido.Sub_Total = carritodepedidos[i].Sub_Total;
                    detalle_Pedido.Estado_Eliminacion = false;
                    detalle_Pedido.Id_Estado = 1;//reservado
                    Total = Total + carritodepedidos[i].Sub_Total;
                    context.Detalle_Pedidos.Add(detalle_Pedido);
                    context.SaveChanges();
                    //Eliminar producto del carrito
                    context.Carrito_Pedidos.Remove(carritodepedidos[i]);
                    context.SaveChanges();
                }
                //CREAR UNA NOTIFICACIÓN LUEGO DE GUARDAR EL PEDIDO
                Notificacion notificacion = new Notificacion();
                notificacion.Id_Usuario= getlooged().Id_Usuario;
                notificacion.Fecha_Hora = DateTime.Now;
                notificacion.Mensaje = "Tienes un nuevo pedido virtual del cliente " + context.Usuarios.First(a => a.Id_Usuario == getlooged().Id_Usuario).Nombre + ", comunicate de inmediato";
                context.Notificaciones.Add(notificacion);
                context.SaveChanges();
                return RedirectToAction("MisPedidos", "Home", new {nuevoregistro=true });
            }
           var carritopedidos= context.Carrito_Pedidos.Include(a => a.Producto).Where(a => a.Id_Usuario == getlooged().Id_Usuario).ToList();
            ViewBag.carritodepedidos = carritopedidos;
            ViewBag.cantidadProdductosenCarrito = carritopedidos.Count;
            return View(pedido);
        }
        public void ValidarPedido(Pedido pedido)
        {
            if (pedido.Fecha_Entrega_Solicitada == null)
                ModelState.AddModelError("Fecha_Entrega_Solicitada", "El campo fecha de entrega solicitada es obligatorio");
            if (pedido.Telefono_Comunicacion == null ||pedido.Telefono_Comunicacion=="")
                ModelState.AddModelError("Telefono_Comunicacion", "El campo teléfono para una buena comunicación es obligatorio");
            if (pedido.Direccion_Entrega == null || pedido.Direccion_Entrega == "")
                ModelState.AddModelError("Direccion_Entrega", "El campo direción de entrega es obligatorio");
            if(context.Carrito_Pedidos.Count(a => a.Id_Usuario == getlooged().Id_Usuario) <= 0)
            {
                ModelState.AddModelError("TengoProductosAgregados", "Para completar mi pedido almenos debo de tener un producto agregado");
            }
        }
        [HttpGet]
        public IActionResult Eliminar(int idpedido)
        {
            if (!VerificarSiUsuarioEsCliente()) { return RedirectToAction("Logaut", "Auth"); }
            if (idpedido != 0)
            {
                Pedido pedidoDB = context.Pedidos.First(a => a.Id_Pedido == idpedido);
                pedidoDB.Estado_Eliminacion = true;
                context.SaveChanges();
                var detalles_pedido = context.Detalle_Pedidos.Where(a => a.Id_Pedido == idpedido && a.Estado_Eliminacion == false).ToList();
                for (int i = 0; i < detalles_pedido.Count(); i++)
                {
                    detalles_pedido[i].Estado_Eliminacion = true;
                    context.SaveChanges();
                }
            }
           
            return RedirectToAction("MisPedidos", "Home");
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
