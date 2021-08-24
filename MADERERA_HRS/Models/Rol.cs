using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MADERERA_HRS.Models
{
    public class Rol
    {
        public int Id_Rol { get; set; }
        public string Nombre { get; set; }
        public bool Estado_Eliminacion { get; set; }
        public string Descripcion { get; set; }

        public List<Usuario> Usuarios { get; set; }
    }
}
