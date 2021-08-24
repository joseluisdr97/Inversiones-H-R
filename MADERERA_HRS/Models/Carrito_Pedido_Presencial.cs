using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MADERERA_HRS.Models
{
    public class Carrito_Pedido_Presencial
    {
        public int Id_Carrito_Pedido_Presencial { get; set; }
        public string Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Sub_Total { get; set; }
        public string Descripcion { get; set; }
        public string Medidas { get; set; }
    }
}
