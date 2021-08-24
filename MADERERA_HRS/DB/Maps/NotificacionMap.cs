using MADERERA_HRS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MADERERA_HRS.DB.Maps
{
    public class NotificacionMap : IEntityTypeConfiguration<Notificacion>
    {
        public void Configure(EntityTypeBuilder<Notificacion> builder)
        {
            builder.ToTable("Notificacion");
            builder.HasKey(o => o.Id_Notificacion);

            builder.HasOne(o => o.Usuario).WithMany(o => o.Notificaciones).HasForeignKey(o => o.Id_Usuario);
        }
    }
}