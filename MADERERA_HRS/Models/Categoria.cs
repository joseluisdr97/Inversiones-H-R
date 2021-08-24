using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MADERERA_HRS.Models
{
    public class Categoria
    {
        public int Id_Categoria { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Estado_Eliminacion { get; set; }

        public List<Producto> Productos { get; set; }
    }
}
