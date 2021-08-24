using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MADERERA_HRS.Models
{
    public class Detalle_Pedido_Presencial
    {
        public int Id_Detalle_Pedido_Presencial { get; set; }
        public int Id_Pedido_Presencial { get; set; }
        public string Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Sub_Total { get; set; }
        public bool Estado_Eliminacion { get; set; }
        public int Id_Estado { get; set; }
        public string Descripcion { get; set; }
        public string Medidas { get; set; }

        public Estado Estado { get; set; }
        public Pedido_Presencial Pedido_Presencial { get; set; }
    }
}
