using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MADERERA_HRS.Models
{
    public class Notificacion
    {
        public int Id_Notificacion { get; set; }
        public int Id_Usuario { get; set; }
        public string Mensaje { get; set; }
        public DateTime Fecha_Hora { get; set; }
        public Usuario Usuario { get; set; }
    }
}
