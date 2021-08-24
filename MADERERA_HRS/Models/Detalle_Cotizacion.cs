using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MADERERA_HRS.Models
{
    public class Detalle_Cotizacion
    {
        public int Id_Detalle_Cotizacion { get; set; }
        public int Id_Cotizacion { get; set; }
        public int Id_Estado { get; set; }
        public string Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Sub_Total { get; set; }
        public string Imagen { get; set; }
        public string Medidas { get; set; }
        public string Descripcion { get; set; }
        public bool Estado_Eliminacion { get; set; }
        public Cotizacion Cotizacion { get; set; }
        public Estado Estado { get; set; }
    }
}
