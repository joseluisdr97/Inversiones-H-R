using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MADERERA_HRS.Models
{
    public class Usuario
    {
        public int Id_Usuario { get; set; }
        public int Id_Rol { get; set; }
        public string Imagen { get; set; }
        public string Nombre { get; set; }
        public string DNI { get; set; }
        public string Apodo { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Contrasenia { get; set; }
        public bool Estado_Eliminacion { get; set; }
        public Rol Rol { get; set; }

        public List<Carrito_Cotizacion> Carrito_Cotizaciones { get; set; }
        public List<Carrito_Pedido> Carrito_Pedidos { get; set; }
        public List<Cotizacion> Cotizaciones { get; set; }
        public List<Pedido> Pedidos { get; set; }
        public List<Notificacion> Notificaciones { get; set; }
        public List<Comentario> Comentarios { get; set; }
        public List<Calificacion> Calificaciones { get; set; }
    }
}
