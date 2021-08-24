using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MADERERA_HRS.DB;
using MADERERA_HRS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MADERERA_HRS.Controllers
{
    public class LlamadasAjaxController : Controller
    {
        private AppContextDB context;
        private IConfiguration configuration;
        public LlamadasAjaxController(AppContextDB context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }
        //PETICIONES DEL ADMINISTRADOR
        [HttpGet]
        public IActionResult ObtenerNotificaciones()
        {
            return View(context.Notificaciones.Include(a=>a.Usuario).ToList());
        }
        public int ObtenerNumerodenotificaciones()
        {
            return context.Notificaciones.Count();
        }
        public bool EliminarNotificacion(int idnotificacion)
        {
            if (idnotificacion > 0)
            {
                context.Notificaciones.Remove(context.Notificaciones.First(a => a.Id_Notificacion == idnotificacion));
                context.SaveChanges();
                return true;
            }
            return false;
        }

        [HttpGet]
        public IActionResult ObtenerPedidosNuevosAdmin()
        {
            var pedidos = context.Pedidos.Include(a => a.Usuario).Include(a => a.Estado).Where(a => a.Id_Estado == 1 && a.Estado_Eliminacion == false).ToList();
            return View(pedidos);
        }

        [HttpGet]
        public IActionResult ObtenerPedidosEnConstruccionAdmin()
        {
            var pedidos = context.Pedidos.Include(a => a.Usuario).Include(a => a.Estado).Where(a => a.Id_Estado == 2 && a.Estado_Eliminacion == false).ToList();
            return View(pedidos);
        }
        [HttpGet]
        public IActionResult ObtenerPedidosTerminadosAdmin()
        {
            var pedidos = context.Pedidos.Include(a => a.Usuario).Include(a => a.Estado).Where(a => a.Id_Estado == 4 && a.Estado_Eliminacion == false).ToList();
            return View(pedidos);
        }
        [HttpGet]
        public IActionResult ObtenerListaDeProductosPorCategoriaParaAdmin(int id)
        {
            List<Producto> productos = null;
            if (id == 0)
            {
                productos = context.Productos.Include(a => a.Categoria).Where(p=>p.Estado_Eliminacion==false).ToList();
            }
            else
            {
                productos = context.Productos.Include(a => a.Categoria).Where(p => p.Id_Categoria == id && p.Estado_Eliminacion == false).ToList();
            }
            return View(productos);
        }
        [HttpGet]
        public IActionResult ObtenerListaDeProductosParaAgregarAPedido()
        {
            return View(context.Productos.Include(a => a.Categoria).Where(e => e.Estado_Eliminacion == false).ToList());
        }
        public bool AgregarProductoAPedido(int idpedido, int idproducto)
        {
            if (idpedido > 0 & idproducto > 0)
            {
                var existeDetallePedido = context.Detalle_Pedidos.Any(a => a.Id_Pedido == idpedido && a.Id_Producto == idproducto && a.Estado_Eliminacion==false);
                if (existeDetallePedido)
                {
                    var productoDBDetalle = context.Detalle_Pedidos.First(a => a.Id_Pedido == idpedido && a.Id_Producto == idproducto);
                    productoDBDetalle.Cantidad = productoDBDetalle.Cantidad + 1;
                    productoDBDetalle.Sub_Total = productoDBDetalle.Sub_Total + productoDBDetalle.Precio;
                    context.SaveChanges();
                }
                else
                {
                    var productoDB = context.Productos.First(a => a.Id_Producto == idproducto);
                    Detalle_Pedido detalle_Pedido = new Detalle_Pedido();
                    detalle_Pedido.Id_Pedido = idpedido;
                    detalle_Pedido.Id_Producto = idproducto;
                    detalle_Pedido.Cantidad = 1;
                    detalle_Pedido.Precio = productoDB.Precio;
                    detalle_Pedido.Sub_Total = productoDB.Precio;
                    detalle_Pedido.Estado_Eliminacion = false;
                    detalle_Pedido.Id_Estado = 1;
                    context.Detalle_Pedidos.Add(detalle_Pedido);
                    context.SaveChanges();
                }
                decimal Total = 0;
                var detalles_pedido = context.Detalle_Pedidos.Where(a => a.Id_Pedido == idpedido && a.Estado_Eliminacion == false).ToList();
                for (int i = 0; i < detalles_pedido.Count; i++)
                {
                    Total = Total + detalles_pedido[i].Sub_Total;
                }
                var pedido = context.Pedidos.First(a => a.Id_Pedido == idpedido);
                pedido.Total = Total;
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public IActionResult ObtenerDetallePedidoAdmin(int idpedido)
        {
            if (idpedido > 0)
            {
                var detalles_pedido = context.Detalle_Pedidos.Include(a => a.Producto).Include(a => a.Pedido).Include(a => a.Estado).Where(a => a.Estado_Eliminacion == false && a.Id_Pedido == idpedido).ToList();
                return View(detalles_pedido);
            }
            return View();
        }
        //public IActionResult ObtenerDetallePedidoTerminadoAdmin(int idpedido)
        //{
        //    if (idpedido > 0)
        //    {
        //        var detalles_pedido = context.Detalle_Pedidos.Include(a => a.Producto).Include(a => a.Pedido).Include(a => a.Estado).Where(a => a.Estado_Eliminacion == false && a.Id_Pedido == idpedido).ToList();
        //        return View(detalles_pedido);
        //    }
        //    return View();
        //}
        public IActionResult ObtenerDetallePedidoEntregadoAdmin(int idpedido)
        {
            if (idpedido > 0)
            {
                var detalles_pedido = context.Detalle_Pedidos.Include(a => a.Producto).Include(a => a.Pedido).Include(a => a.Estado).Where(a => a.Estado_Eliminacion == false && a.Id_Pedido == idpedido).ToList();
                return View(detalles_pedido);
            }
            return View();
        }
        public IActionResult ObtenerDetalleCotizacionAdmin(int idcotizacion)
        {
            if (idcotizacion > 0)
            {
                var detallesCotizacion = context.Detalle_Cotizaciones.Include(a => a.Estado).Include(a => a.Cotizacion).Where(a => a.Id_Cotizacion == idcotizacion && a.Estado_Eliminacion == false).ToList();
                return View(detallesCotizacion);
            }
            return View();
        }
        public bool ConfirmarCotizaciondeitemDetalleCotizacion(int iddetallecotizacion, decimal precio)
        {
            if (iddetallecotizacion > 0 && precio > 0)
            {
                var detalle_cotizacionDB = context.Detalle_Cotizaciones.First(a => a.Id_Detalle_Cotizacion == iddetallecotizacion);
                detalle_cotizacionDB.Id_Estado = 6;
                detalle_cotizacionDB.Precio = precio;
                detalle_cotizacionDB.Sub_Total = detalle_cotizacionDB.Cantidad * precio;
                context.SaveChanges();
                decimal Total = 0;
                var detalles_cotizacion = context.Detalle_Cotizaciones.Where(a => a.Id_Cotizacion == detalle_cotizacionDB.Id_Cotizacion && a.Estado_Eliminacion == false).ToList();
                for (int i = 0; i < detalles_cotizacion.Count; i++)
                {
                    Total = Total + detalles_cotizacion[i].Sub_Total;
                }
                var cotizacion = context.Cotizaciones.First(a => a.Id_Cotizacion == detalle_cotizacionDB.Id_Cotizacion);
                cotizacion.Total = Total;
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public IActionResult ObtenerNuevasCotizacionesAdmin()
        {

            var cotizaciones = context.Cotizaciones.Include(a => a.Usuario).Include(a => a.Estado).Where(a => a.Estado_Eliminacion == false && a.Id_Estado==3).ToList();
            return View(cotizaciones);
        }
        public IActionResult ObtenerCotizacionesFinalizadasAdmin()
        {

            var cotizaciones = context.Cotizaciones.Include(a => a.Usuario).Include(a => a.Estado).Where(a => a.Estado_Eliminacion == false && a.Id_Estado == 6).ToList();
            return View(cotizaciones);
        }
        public IActionResult ObtenerNuevosPedidosCotizadosAdmin()
        {
            var cotizaciones = context.Cotizaciones.Include(a => a.Usuario).Include(a => a.Estado).Where(a => a.Estado_Eliminacion == false && a.Id_Estado == 1).ToList();
            return View(cotizaciones);
        }
        public IActionResult ObtenerPedidosCotizadosEnConstruccionAdmin()
        {
            var cotizaciones = context.Cotizaciones.Include(a => a.Usuario).Include(a => a.Estado).Where(a => a.Estado_Eliminacion == false && a.Id_Estado == 2).ToList();
            return View(cotizaciones);
        }
        public IActionResult ObtenerPedidosCotizadosTerminadosAdmin()
        {
            var cotizaciones = context.Cotizaciones.Include(a => a.Usuario).Include(a => a.Estado).Where(a => a.Estado_Eliminacion == false && a.Id_Estado == 4).ToList();
            return View(cotizaciones);
        }
        [HttpGet]
        public bool ActualizarCantidadDeDetalleCotizacionAdmin(int iddetallecotizacion, int cantidad)
        {
            if (iddetallecotizacion > 0 & cantidad > 0)
            {
                Detalle_Cotizacion detalle_CotizacionDB = context.Detalle_Cotizaciones.First(a => a.Id_Detalle_Cotizacion == iddetallecotizacion);
                if (detalle_CotizacionDB.Id_Estado != 3)
                {
                    detalle_CotizacionDB.Cantidad = cantidad;
                    detalle_CotizacionDB.Sub_Total = detalle_CotizacionDB.Precio * cantidad;
                    context.SaveChanges();
                    var detalles_cotizacion = context.Detalle_Cotizaciones.Where(a => a.Id_Cotizacion == detalle_CotizacionDB.Id_Cotizacion && a.Estado_Eliminacion == false).ToList();
                    decimal Total = 0;
                    for (int i = 0; i < detalles_cotizacion.Count; i++)
                    {
                        Total = Total + detalles_cotizacion[i].Sub_Total;
                    }
                    var cotizacion = context.Cotizaciones.First(a => a.Id_Cotizacion == detalle_CotizacionDB.Id_Cotizacion);
                    cotizacion.Total = Total;
                    context.SaveChanges();
                }
                else
                {
                        detalle_CotizacionDB.Cantidad = cantidad;
                        context.SaveChanges();
                }
                return true;
            }
            return false;
        }
        public bool EliminarItemDetalleCotizacionAdmin(int iddetallecotizacion)
        {
            if (iddetallecotizacion > 0)
            {
                var detalle_Cotizacion = context.Detalle_Cotizaciones.First(a => a.Id_Detalle_Cotizacion == iddetallecotizacion);
                detalle_Cotizacion.Estado_Eliminacion = true;
                context.SaveChanges();
                if (context.Cotizaciones.First(a => a.Id_Cotizacion == detalle_Cotizacion.Id_Cotizacion).Total>0)
                {
                    var detalles_cotizacion = context.Detalle_Cotizaciones.Where(a => a.Id_Cotizacion == detalle_Cotizacion.Id_Cotizacion && a.Estado_Eliminacion == false).ToList();
                    if (detalles_cotizacion.Count > 0)
                    {
                        decimal Total = 0;
                        for (int i = 0; i < detalles_cotizacion.Count; i++)
                        {
                            Total = Total + detalles_cotizacion[i].Sub_Total;
                        }
                        var cotizacion = context.Cotizaciones.First(a => a.Id_Cotizacion == detalle_Cotizacion.Id_Cotizacion);
                        cotizacion.Total = Total;
                        context.SaveChanges();
                    }
                    else
                    {
                        var cotizacion = context.Pedidos.First(a => a.Id_Pedido == detalle_Cotizacion.Id_Cotizacion);
                        cotizacion.Total = 0;
                        cotizacion.Estado_Eliminacion = true;
                        context.SaveChanges();
                    }
                }
                return true;
            }
            return false;
        }
        public string ObtenerImagenDeProducto(int iddetalle)
        {
            if (iddetalle > 0)
            {
                var detalleDB = context.Detalle_Cotizaciones.First(a => a.Id_Detalle_Cotizacion == iddetalle);
                return detalleDB.Imagen;
            }
            return null;
        }
        public bool ComprobarSiYaSeCotizoTodosLosDetalles(int idcotizacion)
        {
            if (idcotizacion > 0)
            {
                var detalles = context.Detalle_Cotizaciones.Where(a => a.Id_Cotizacion == idcotizacion && a.Estado_Eliminacion == false).ToList();
                if (detalles.Count > 0)
                {
                    var contador = 0;
                    for(int i = 0; i < detalles.Count; i++)
                    {
                        if (detalles[i].Id_Estado != 6)
                        {
                            contador++;
                        }
                    }
                    if (contador > 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        //--------------------------------------------------PEDIDO PRESENCIAL DEL ADMINISTRADOR--------------------------------------
        public IActionResult ObtenerDetallePedidoPresencialCliente(int idpedido)
        {
            if (idpedido > 0)
            {
                return View(context.Detalle_PedidosPresenciales.Where(a => a.Id_Pedido_Presencial == idpedido).ToList());
            }
            return View();
        }
        public bool EliminaritemCarritoPedidoPresencial(int idcarritopedido)
        {
            if (idcarritopedido > 0)
            {
                var itemcarrito = context.Carrito_Pedidos_Presenciales.First(a => a.Id_Carrito_Pedido_Presencial == idcarritopedido);
                context.Carrito_Pedidos_Presenciales.Remove(itemcarrito);
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public bool ActualizarCantidadCarritoPedidoPresencial(int idcarritopedido, int cantidad)
        {
            if (idcarritopedido > 0 && cantidad > 0)
            {
                var itemcarrito = context.Carrito_Pedidos_Presenciales.First(a => a.Id_Carrito_Pedido_Presencial == idcarritopedido);
                itemcarrito.Cantidad = cantidad;
                itemcarrito.Sub_Total = cantidad * itemcarrito.Precio;
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public IActionResult ObtenerListaDeProductosDeCarritoPedidoPresencial()
        {
            ViewBag.Carrito_Pedidos = context.Carrito_Pedidos_Presenciales.ToList();
            ViewBag.cantidadProdductosenCarrito = context.Carrito_Pedidos_Presenciales.Count();
            return View();
        }
        public IActionResult ObtenerDetallePedidoPresencialAdmin(int idpedido)
        {
            if (idpedido > 0)
            {
                var detalles_pedido = context.Detalle_PedidosPresenciales.Include(a => a.Pedido_Presencial).Include(a => a.Estado).Where(a => a.Estado_Eliminacion == false && a.Id_Pedido_Presencial == idpedido).ToList();
                return View(detalles_pedido);
            }
            return View();
        }
        public IActionResult ObtenerPedidosPresencialesNuevosAdmin()
        {
            var pedidos = context.Pedidos_Presenciales.Where(a => a.Estado_Eliminacion == false && a.Id_Estado == 1).ToList();
            return View(pedidos);
        }
        public IActionResult ObtenerPedidosPresencialesEnConstruccionAdmin()
        {
            var pedidos = context.Pedidos_Presenciales.Where(a => a.Estado_Eliminacion == false && a.Id_Estado == 2).ToList();
            return View(pedidos);
        }
        public IActionResult ObtenerPedidosPresencialesTerminadosAdmin()
        {
            var pedidos = context.Pedidos_Presenciales.Where(a => a.Estado_Eliminacion == false && a.Id_Estado == 4).ToList();
            return View(pedidos);
        }
        [HttpGet]
        public bool ActualizarCantidadDeDetallePedidoPresencialAdmin(int iddetallepedido, int cantidad)
        {
            if (iddetallepedido > 0 & cantidad > 0)
            {
                var detalle_PedidoDB = context.Detalle_PedidosPresenciales.First(a => a.Id_Detalle_Pedido_Presencial == iddetallepedido);
                detalle_PedidoDB.Cantidad = cantidad;
                detalle_PedidoDB.Sub_Total = detalle_PedidoDB.Precio * cantidad;
                context.SaveChanges();
                var detalles_pedido = context.Detalle_PedidosPresenciales.Where(a => a.Id_Pedido_Presencial == detalle_PedidoDB.Id_Pedido_Presencial && a.Estado_Eliminacion == false).ToList();
                decimal Total = 0;
                for (int i = 0; i < detalles_pedido.Count; i++)
                {
                    Total = Total + detalles_pedido[i].Sub_Total;
                }
                var pedido = context.Pedidos_Presenciales.First(a => a.Id_Pedido_Presencial == detalle_PedidoDB.Id_Pedido_Presencial);
                pedido.Total = Total;
                context.SaveChanges();
                return true;
            }
            return false;
        }
        [HttpGet]
        public bool EliminarItemDetallePedidoPresencialAdmin(int iddetallepedido)
        {
            if (iddetallepedido > 0)
            {
                var detalle_Pedido = context.Detalle_PedidosPresenciales.First(a => a.Id_Detalle_Pedido_Presencial == iddetallepedido);
                detalle_Pedido.Estado_Eliminacion = true;
                context.SaveChanges();
                var detalles_pedido = context.Detalle_PedidosPresenciales.Where(a => a.Id_Pedido_Presencial == detalle_Pedido.Id_Pedido_Presencial && a.Estado_Eliminacion == false).ToList();
                if (detalles_pedido.Count > 0)
                {
                    decimal Total = 0;
                    for (int i = 0; i < detalles_pedido.Count; i++)
                    {
                        Total = Total + detalles_pedido[i].Sub_Total;
                    }
                    var pedido = context.Pedidos_Presenciales.First(a => a.Id_Pedido_Presencial == detalle_Pedido.Id_Pedido_Presencial);
                    pedido.Total = Total;
                    context.SaveChanges();
                }
                return true;
            }
            return false;
        }


        //PETICIONES DEL CLIENTE
        public bool GuardarCalificacion(int iddetallepedido, int cantidad)
        {
            if (iddetallepedido > 0 && cantidad>0)
            {
                var detallepedido = context.Detalle_Pedidos.First(a => a.Id_Detalle_Pedido == iddetallepedido);
                var calificado = context.Calificaciones.Any(a => a.Id_Producto == detallepedido.Id_Producto && a.Id_Usuario == getlooged().Id_Usuario);
                if (calificado)
                {
                    var calificacion = context.Calificaciones.First(a => a.Id_Producto == detallepedido.Id_Producto && a.Id_Usuario == getlooged().Id_Usuario);
                    calificacion.Numero_Calificacion = cantidad;
                    context.SaveChanges();
                }
                else
                {
                    Calificacion calificacion = new Calificacion();
                    calificacion.Id_Usuario = getlooged().Id_Usuario;
                    calificacion.Id_Producto = detallepedido.Id_Producto;
                    calificacion.Numero_Calificacion = cantidad;
                    context.Calificaciones.Add(calificacion);
                    context.SaveChanges();
                }
                return true;
            }
            return false;
        }
        [HttpGet]
        public bool VerificarSiUsuarioClienteExiste()
        {
            if (getlooged() != null)
            {
                if (getlooged().Id_Rol == 1)
                {
                    return true;
                }
            }
            return false;
        }
        [HttpGet]
        public bool AgregarProductoACarritoPedido(int idproducto)
        {
            if (idproducto != 0 && getlooged()!=null)
            {
                if (getlooged().Id_Rol == 1)
                {
                    var existeproductoencarrito = context.Carrito_Pedidos.Any(a => a.Id_Producto == idproducto && a.Id_Usuario == getlooged().Id_Usuario);
                    if (existeproductoencarrito)
                    {
                        var productoDBCarrito = context.Carrito_Pedidos.First(a => a.Id_Usuario == getlooged().Id_Usuario && a.Id_Producto == idproducto);
                        productoDBCarrito.Cantidad = productoDBCarrito.Cantidad + 1;
                        productoDBCarrito.Sub_Total = productoDBCarrito.Sub_Total + productoDBCarrito.Precio;
                        context.SaveChanges();
                    }
                    else
                    {
                        Carrito_Pedido carrito = new Carrito_Pedido();
                        carrito.Id_Usuario = getlooged().Id_Usuario;
                        carrito.Id_Producto = idproducto;
                        carrito.Cantidad = 1;
                        carrito.Precio = context.Productos.First(a => a.Id_Producto == idproducto).Precio;
                        carrito.Sub_Total = carrito.Precio;
                        context.Carrito_Pedidos.Add(carrito);
                        context.SaveChanges();
                    }
                    return true;
                }
                return false;
            }
            return false;
        }
        [HttpGet]
        public IActionResult ObtenerProductosDeCarritoPedido()
        {
            decimal Total = 0;
            var carritodepedidos = context.Carrito_Pedidos.Include(a => a.Producto).Where(a => a.Id_Usuario == getlooged().Id_Usuario).ToList();
            if (carritodepedidos.Count > 0)
            {
                for (int i = 0; i < carritodepedidos.Count; i++)
                {
                    Total = Total + carritodepedidos[i].Sub_Total;
                }
            }
            ViewBag.Total = Total;
            return View(carritodepedidos);
        }
        [HttpGet]
        public IActionResult ObtenerProductosDeCarritoPedidoEnCompletarPedido()
        {
            decimal Total = 0;
            var carritodepedidos = context.Carrito_Pedidos.Where(a => a.Id_Usuario == getlooged().Id_Usuario).ToList();
            if (carritodepedidos.Count > 0)
            {
                for (int i = 0; i < carritodepedidos.Count; i++)
                {
                    Total = Total + carritodepedidos[i].Sub_Total;
                }
            }
            ViewBag.Total = Total;
            ViewBag.carritodepedidos = context.Carrito_Pedidos.Include(a => a.Producto).Where(a => a.Id_Usuario == getlooged().Id_Usuario).ToList();
            return View();
        }
        [HttpGet]
        public IActionResult ObtenerDatosDeProductoEnProducto(int idproducto)
        {
            Producto producto = context.Productos.Include(a => a.Categoria).First(a => a.Id_Producto == idproducto);
            return View(producto);
        }
        [HttpGet]
        public bool ActualizarCantidadDeProductoDeCarrito(int idcarritopedido, int cantidad)
        {
            if (idcarritopedido > 0 & cantidad>0)
            {
                var productoCarrito = context.Carrito_Pedidos.First(a => a.Id_Carrito_Pedido == idcarritopedido);
                productoCarrito.Cantidad = cantidad;
                productoCarrito.Sub_Total = productoCarrito.Precio * cantidad;
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public bool EliminarProductoDeCarritoEnProductos(int idcarritopedido)
        {
            if (idcarritopedido != 0)
            {
                var productoCarrito = context.Carrito_Pedidos.First(a => a.Id_Carrito_Pedido == idcarritopedido);
                context.Carrito_Pedidos.Remove(productoCarrito);
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public IActionResult ObtenerDetallePedido(int idpedido)
        {
            if (idpedido > 0)
            {
                var detalles_pedido = context.Detalle_Pedidos.Include(a => a.Producto).Include(a=>a.Pedido).Include(a => a.Estado).Where(a => a.Estado_Eliminacion == false && a.Id_Pedido == idpedido).ToList();
                return View(detalles_pedido);
            }
            return View();
        }
        public bool EliminarItemDetallePedido(int iddetallepedido)
        {
            if (iddetallepedido > 0)
            {
                var detalle_Pedido = context.Detalle_Pedidos.First(a => a.Id_Detalle_Pedido == iddetallepedido);
                detalle_Pedido.Estado_Eliminacion = true;
                context.SaveChanges();
                var detalles_pedido = context.Detalle_Pedidos.Where(a => a.Id_Pedido == detalle_Pedido.Id_Pedido && a.Estado_Eliminacion == false).ToList();
                if (detalles_pedido.Count > 0)
                {
                    decimal Total = 0;
                    for (int i = 0; i < detalles_pedido.Count; i++)
                    {
                        Total = Total + detalles_pedido[i].Sub_Total;
                    }
                    var pedido = context.Pedidos.First(a => a.Id_Pedido == detalle_Pedido.Id_Pedido);
                    pedido.Total = Total;
                    context.SaveChanges();
                }
                else
                {
                    var pedido = context.Pedidos.First(a => a.Id_Pedido == detalle_Pedido.Id_Pedido);
                    pedido.Total = 0;
                    pedido.Estado_Eliminacion = true;
                    context.SaveChanges();
                }

                return true;
            }
            return false;
        }
        [HttpGet]
        public bool ActualizarCantidadDeDetallePedido(int iddetallepedido, int cantidad)
        {
            if (iddetallepedido > 0 & cantidad > 0)
            {
                var detalle_PedidoDB = context.Detalle_Pedidos.First(a => a.Id_Detalle_Pedido == iddetallepedido);
                detalle_PedidoDB.Cantidad = cantidad;
                detalle_PedidoDB.Sub_Total = detalle_PedidoDB.Precio * cantidad;
                context.SaveChanges();
                var detalles_pedido = context.Detalle_Pedidos.Where(a => a.Id_Pedido == detalle_PedidoDB.Id_Pedido && a.Estado_Eliminacion == false).ToList();
                decimal Total = 0;
                for (int i = 0; i < detalles_pedido.Count; i++)
                {
                    Total = Total + detalles_pedido[i].Sub_Total;
                }
                var pedido = context.Pedidos.First(a => a.Id_Pedido == detalle_PedidoDB.Id_Pedido);
                pedido.Total = Total;
                context.SaveChanges();
                return true;
            }
            return false;
        }
        [HttpGet]
        public IActionResult ObtenerPedidosCliente()
        {
            var pedidos = context.Pedidos.Include(a => a.Estado).Where(a => a.Id_Usuario == getlooged().Id_Usuario && a.Estado_Eliminacion == false).ToList();
            pedidos = pedidos.OrderByDescending(x => x.Fecha).ToList();
            return View(pedidos);
        }

        //PETICIONES DEL CLIENTE DE COTIZACIONES
        public int ObtenerEstadoCotizacion(int idcotizacion)
        {
            if (idcotizacion > 0)
            {
                var CotizacionDB = context.Cotizaciones.First(a => a.Id_Cotizacion == idcotizacion);
                return CotizacionDB.Id_Estado;
            }
            return 0;
        }

        public bool actualizarCantidadCarritoCotizacion(int idcarritocotizacion, int cantidad)
        {
            if(cantidad>0 && idcarritocotizacion > 0)
            {
                var carritocotizacionDB = context.Carrito_Cotizaciones.First(a => a.Id_Carrito_Cotizacion == idcarritocotizacion);
                carritocotizacionDB.Cantidad = cantidad;
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public IActionResult ObtenerListaDeProductosDeCarritoCotizacion()
        {
            ViewBag.carritodecotizaciones = context.Carrito_Cotizaciones.Where(a => a.Id_Usuario==getlooged().Id_Usuario).ToList();
                return View();
        }
        public bool EliminaritemCarritoCotizacion(int idcarritocotizacion)
        {
            if (idcarritocotizacion > 0)
            {
                var carritocotizacionDB = context.Carrito_Cotizaciones.First(a => a.Id_Carrito_Cotizacion == idcarritocotizacion);
                context.Carrito_Cotizaciones.Remove(carritocotizacionDB);
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public IActionResult ObtenerDetalleCotizacionCliente(int idcotizacion)
        {
            if (idcotizacion > 0)
            {
                var detallesCotizacion = context.Detalle_Cotizaciones.Include(a=>a.Estado).Include(a=>a.Cotizacion).Where(a => a.Id_Cotizacion == idcotizacion && a.Estado_Eliminacion==false).ToList();
                return View(detallesCotizacion);
            }
            return View();
        }
        [HttpGet]
        public bool ActualizarCantidadDeDetalleCotizacion(int iddetallecotizacion, int cantidad)
        {
            if (iddetallecotizacion > 0 & cantidad > 0)
            {
                Detalle_Cotizacion detalle_CotizacionDB = context.Detalle_Cotizaciones.First(a => a.Id_Detalle_Cotizacion == iddetallecotizacion);
                if (detalle_CotizacionDB.Id_Estado != 3)
                {
                    detalle_CotizacionDB.Cantidad = cantidad;
                    detalle_CotizacionDB.Sub_Total = detalle_CotizacionDB.Precio * cantidad;
                    context.SaveChanges();
                    var detalles_cotizacion = context.Detalle_Cotizaciones.Where(a => a.Id_Cotizacion == detalle_CotizacionDB.Id_Cotizacion && a.Estado_Eliminacion == false).ToList();
                    decimal Total = 0;
                    for (int i = 0; i < detalles_cotizacion.Count; i++)
                    {
                        Total = Total + detalles_cotizacion[i].Sub_Total;
                    }
                    var cotizacion = context.Cotizaciones.First(a => a.Id_Cotizacion == detalle_CotizacionDB.Id_Cotizacion);
                    cotizacion.Total = Total;
                    context.SaveChanges();
                }
                else
                {
                    if (detalle_CotizacionDB.Sub_Total > 0)
                    {
                        detalle_CotizacionDB.Cantidad = cantidad;
                        detalle_CotizacionDB.Sub_Total = cantidad * detalle_CotizacionDB.Precio;
                        context.SaveChanges();
                        var detalles_cotizacion = context.Detalle_Cotizaciones.Where(a => a.Id_Cotizacion == detalle_CotizacionDB.Id_Cotizacion && a.Estado_Eliminacion == false).ToList();
                        decimal Total = 0;
                        for (int i = 0; i < detalles_cotizacion.Count; i++)
                        {
                            Total = Total + detalles_cotizacion[i].Sub_Total;
                        }
                        var cotizacion = context.Cotizaciones.First(a => a.Id_Cotizacion == detalle_CotizacionDB.Id_Cotizacion);
                        cotizacion.Total = Total;
                        context.SaveChanges();
                    }
                    else
                    {
                        detalle_CotizacionDB.Cantidad = cantidad;
                        context.SaveChanges();
                    }
                }
                return true;
            }
            return false;
        }
        public IActionResult ObtenerCotizacionesCliente()
        {
            var cotizaciones = context.Cotizaciones.Include(a => a.Estado).Where(a => a.Id_Usuario == getlooged().Id_Usuario && a.Estado_Eliminacion == false).ToList();
            cotizaciones = cotizaciones.OrderByDescending(a => a.Fecha).ToList();
            return View(cotizaciones);
        }
        public bool EliminarItemDetalleCotizacion(int iddetallecotizacion)
        {
            if (iddetallecotizacion > 0)
            {
                var detalle_Cotizacion = context.Detalle_Cotizaciones.First(a => a.Id_Detalle_Cotizacion == iddetallecotizacion);
                detalle_Cotizacion.Estado_Eliminacion = true;
                context.SaveChanges();
                if(context.Cotizaciones.First(a=>a.Id_Cotizacion==detalle_Cotizacion.Id_Cotizacion).Id_Estado != 3)
                {
                    var detalles_cotizacion = context.Detalle_Cotizaciones.Where(a => a.Id_Cotizacion == detalle_Cotizacion.Id_Cotizacion && a.Estado_Eliminacion == false).ToList();
                    if (detalles_cotizacion.Count > 0)
                    {
                        decimal Total = 0;
                        for (int i = 0; i < detalles_cotizacion.Count; i++)
                        {
                            Total = Total + detalles_cotizacion[i].Sub_Total;
                        }
                        var cotizacion = context.Cotizaciones.First(a => a.Id_Cotizacion == detalle_Cotizacion.Id_Cotizacion);
                        cotizacion.Total = Total;
                        context.SaveChanges();
                    }
                    else
                    {
                        var cotizacion = context.Pedidos.First(a => a.Id_Pedido == detalle_Cotizacion.Id_Cotizacion);
                        cotizacion.Total = 0;
                        cotizacion.Estado_Eliminacion = true;
                        context.SaveChanges();
                    }
                }
                return true;
            }
            return false;
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

    }
}
