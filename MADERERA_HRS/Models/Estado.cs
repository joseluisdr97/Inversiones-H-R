using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MADERERA_HRS.Models
{
    public class Estado
    {
        public int Id_Estado { get; set; }
        public int Numero_Estado { get; set; }
        public string Nombre { get; set; }
        public bool Estado_Eliminacion { get; set; }

        public List<Cotizacion> Cotizaciones { get; set; }
        public List<Detalle_Cotizacion> Detalle_Cotizaciones { get; set; }
        public List<Detalle_Pedido> Detalle_Pedidos { get; set; }
        public List<Detalle_Pedido_Presencial> Detalle_Pedidos_Presenciales { get; set; }
        public List<Pedido> Pedidos { get; set; }
        public List<Pedido_Presencial> Pedidos_Presenciales { get; set; }
    }
}
