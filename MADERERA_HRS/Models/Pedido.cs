using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MADERERA_HRS.Models
{
    public class Pedido
    {
        public int Id_Pedido { get; set; }
        public int Id_Estado { get; set; }
        public int Id_Usuario { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public int Avance { get; set; }
        public DateTime Fecha_Entrega_Solicitada { get; set; }
        public string Direccion_Entrega { get; set; }
        public string Telefono_Comunicacion { get; set; }
        public bool Estado_Eliminacion {get; set;}
        public Estado Estado { get; set; }
        public Usuario Usuario { get; set; }

        public List<Detalle_Pedido> Detalle_Pedidos { get; set; }
    }
}
