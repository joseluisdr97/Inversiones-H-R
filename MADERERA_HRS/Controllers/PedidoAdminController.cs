using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MADERERA_HRS.DB;
using MADERERA_HRS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MADERERA_HRS.Controllers
{
    [Authorize]
    public class PedidoAdminController : Controller
    {
        private AppContextDB context;
        public PedidoAdminController(AppContextDB context)
        {
            this.context = context;
        }
        [HttpGet]
        public IActionResult Nuevos(string query)
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            List<Pedido> listapedidos = new List<Pedido>();
            if(query!=null && query != "")
            {
                listapedidos = context.Pedidos.Include(a => a.Usuario).Include(a => a.Estado).Where(a => a.Id_Estado == 1 && a.Estado_Eliminacion == false && a.Usuario.DNI.Contains(query)).ToList();
                return View(listapedidos);
            }
            else
            {
                listapedidos = context.Pedidos.Include(a => a.Usuario).Include(a => a.Estado).Where(a => a.Id_Estado == 1 && a.Estado_Eliminacion == false).ToList();
            }
            return View(listapedidos);
        }
        [HttpGet]
        public IActionResult EnConstruccion(string query)
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            List<Pedido> listapedidos = new List<Pedido>();
            if (query != null && query != "")
            {
                listapedidos = context.Pedidos.Include(a => a.Usuario).Include(a => a.Estado).Where(a => a.Id_Estado == 2 && a.Estado_Eliminacion == false && a.Usuario.DNI.Contains(query)).ToList();
                return View(listapedidos);
            }
            else
            {
                listapedidos = context.Pedidos.Include(a => a.Usuario).Include(a => a.Estado).Where(a => a.Id_Estado == 2 && a.Estado_Eliminacion == false).ToList();
            }
            return View(listapedidos);
        }
        [HttpGet]
        public IActionResult Terminados(string query)
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            List<Pedido> listapedidos = new List<Pedido>();
            if (query != null && query != "")
            {
                listapedidos = context.Pedidos.Include(a => a.Usuario).Include(a => a.Estado).Where(a => a.Id_Estado == 4 && a.Estado_Eliminacion == false && a.Usuario.DNI.Contains(query)).ToList();
                return View(listapedidos);
            }
            else
            {
                listapedidos = context.Pedidos.Include(a => a.Usuario).Include(a => a.Estado).Where(a => a.Id_Estado == 4 && a.Estado_Eliminacion == false).ToList();
            }
            return View(listapedidos);
        }
        [HttpGet]
        public IActionResult Entregados(string query)
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            List<Pedido> listapedidos = new List<Pedido>();
            if (query != null && query != "")
            {
                listapedidos = context.Pedidos.Include(a => a.Usuario).Include(a => a.Estado).Where(a => a.Id_Estado == 5 && a.Estado_Eliminacion == false && a.Usuario.DNI.Contains(query)).ToList();
                return View(listapedidos);
            }
            else
            {
                listapedidos = context.Pedidos.Include(a => a.Usuario).Include(a => a.Estado).Where(a => a.Id_Estado == 5 && a.Estado_Eliminacion == false).ToList();
            }
            listapedidos = listapedidos.OrderByDescending(a => a.Fecha_Entrega_Solicitada).ToList();
            return View(listapedidos);
        }
        [HttpGet]
        public IActionResult Eliminar(int idpedido, string Vista)
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
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

            return RedirectToAction(Vista);
        }
        [HttpGet]
        public IActionResult IniciarConstruccion(int idpedido)
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            if (idpedido > 0)
            {
                Pedido pedidoDB = context.Pedidos.First(a => a.Id_Pedido == idpedido);
                pedidoDB.Id_Estado = 2;
                context.SaveChanges();
                var detalles = context.Detalle_Pedidos.Where(a => a.Id_Pedido == idpedido).ToList();
                for(int i = 0; i < detalles.Count; i++)
                {
                    detalles[i].Id_Estado = 2;
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
                Pedido pedidoDB = context.Pedidos.First(a => a.Id_Pedido == idpedido);
                pedidoDB.Id_Estado = 4;
                pedidoDB.Avance = 100;
                context.SaveChanges();
                var detalles = context.Detalle_Pedidos.Where(a => a.Id_Pedido == idpedido).ToList();
                for (int i = 0; i < detalles.Count; i++)
                {
                    detalles[i].Id_Estado = 4;
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
                Pedido pedidoDB = context.Pedidos.First(a => a.Id_Pedido == idpedido);
                pedidoDB.Id_Estado = 5;
                context.SaveChanges();
                var detalles = context.Detalle_Pedidos.Where(a => a.Id_Pedido == idpedido).ToList();
                for (int i = 0; i < detalles.Count; i++)
                {
                    detalles[i].Id_Estado = 5;
                    context.SaveChanges();
                }
            }
            return RedirectToAction("Terminados");
        }
        [HttpGet]
        public IActionResult CambiarPorcentajeConstruccion(int idpedido, int porcentaje)
        {
            if (!VerificarSiUsuarioEsAdministrador()) { return RedirectToAction("Logaut", "Auth"); }
            if (idpedido > 0 && porcentaje>=0 && porcentaje<=100)
            {
                Pedido pedidoDB = context.Pedidos.First(a => a.Id_Pedido == idpedido);
                pedidoDB.Avance = porcentaje;
                context.SaveChanges();
            }
            return RedirectToAction("EnConstruccion");
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
