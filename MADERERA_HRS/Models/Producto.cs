using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MADERERA_HRS.Models
{
    public class Producto
    {
        public int Id_Producto { get; set; }
        public int Id_Categoria { get; set; }
        public string Nombre { get; set; }
        public string Medidas { get; set; }
        public decimal Precio { get; set; }
        public string Imagen { get; set; }
        public string Imagen1 { get; set; }
        public string Imagen2 { get; set; }
        public string Descripcion { get; set; }
        public bool Estado_Eliminacion { get; set; }
        public Categoria Categoria { get; set; }

        public List<Carrito_Pedido> Carrito_Pedidos { get; set; }
        public List<Detalle_Pedido> Detalle_Pedidos { get; set; }
        public List<Comentario> Comentarios { get; set; }
        public List<Calificacion> Calificaciones { get; set; }
    }
}
