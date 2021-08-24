using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MADERERA_HRS.Models
{
    public class Pedido_Presencial
    {
        public int Id_Pedido_Presencial { get; set; }
        public int Id_Estado { get; set; }
        public string Cliente { get; set; }
        public string DNI { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public int Avance { get; set; }
        public bool Estado_Eliminacion { get; set; }
        public DateTime Fecha_Entrega_Solicitada { get; set; }
        public string Direccion_Entrega { get; set; }
        public string Telefono_Comunicacion { get; set; }

        public Estado Estado { get; set; }
        public List<Detalle_Pedido_Presencial> Detalle_Pedidos_Presenciales { get; set; }
    }
}
