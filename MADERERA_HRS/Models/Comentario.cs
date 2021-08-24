using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MADERERA_HRS.Models
{
    public class Comentario
    {
        public int Id_Comentario { get; set; }
        public int Id_Usuario { get; set; }
        public int Id_Producto { get; set; }
        public string Descripcion { get; set; }

        public Usuario Usuario { get; set; }
        public Producto Producto { get; set; }
    }
}