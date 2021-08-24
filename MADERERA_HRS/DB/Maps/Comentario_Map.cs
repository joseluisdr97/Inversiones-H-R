using MADERERA_HRS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MADERERA_HRS.DB.Maps
{
    public class Comentario_Map : IEntityTypeConfiguration<Comentario>
    {
        public void Configure(EntityTypeBuilder<Comentario> builder)
        {
            builder.ToTable("Comentario");
            builder.HasKey(o => o.Id_Comentario);

            builder.HasOne(o => o.Usuario).WithMany(o => o.Comentarios).HasForeignKey(o => o.Id_Usuario);
            builder.HasOne(o => o.Producto).WithMany(o => o.Comentarios).HasForeignKey(o => o.Id_Producto);
        }
    }
}