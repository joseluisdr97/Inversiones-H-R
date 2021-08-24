using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MADERERA_HRS.Models
{
    public class Carrito_Pedido
    {
        public int Id_Carrito_Pedido { get; set; }
        public int Id_Usuario { get; set; }
        public int Id_Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Sub_Total { get; set; }
        public Usuario Usuario { get; set; }
        public Producto Producto { get; set; }
    }
}
