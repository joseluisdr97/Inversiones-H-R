using MADERERA_HRS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MADERERA_HRS.DB.Maps
{
    public class Calificacion_Map : IEntityTypeConfiguration<Calificacion>
    {
        public void Configure(EntityTypeBuilder<Calificacion> builder)
        {
            builder.ToTable("Calificacion");
            builder.HasKey(o => o.Id_Calificacion);

            builder.HasOne(o => o.Usuario).WithMany(o => o.Calificaciones).HasForeignKey(o => o.Id_Usuario);
            builder.HasOne(o => o.Producto).WithMany(o => o.Calificaciones).HasForeignKey(o => o.Id_Producto);
        }
    }
}