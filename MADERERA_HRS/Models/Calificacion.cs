using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MADERERA_HRS.Models
{
    public class Calificacion
    {
        public int Id_Calificacion { get; set; }
        public int Id_Usuario { get; set; }
        public int Id_Producto { get; set; }
        public int Numero_Calificacion { get; set; }

        public Usuario Usuario { get; set; }
        public Producto Producto { get; set; }
    }
}
