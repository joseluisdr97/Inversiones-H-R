using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MADERERA_HRS.Models
{
    public class Detalle_Pedido
    {
        public int Id_Detalle_Pedido { get; set; }
        public int Id_Pedido { get; set; }
        public int Id_Producto { get; set; }
        public int Id_Estado { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Sub_Total { get; set; }
        public bool Estado_Eliminacion { get; set; }
        public Pedido Pedido { get; set; }
        public Producto Producto { get; set; }
        public Estado Estado { get; set; }
    }
}
