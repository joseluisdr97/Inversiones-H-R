using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MADERERA_HRS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MADERERA_HRS.DB.Maps
{
    public class Cotizacion_Map : IEntityTypeConfiguration<Cotizacion>
    {
        public void Configure(EntityTypeBuilder<Cotizacion> builder)
        {
            builder.ToTable("Cotizacion");
            builder.HasKey(o => o.Id_Cotizacion);

            builder.HasOne(o => o.Estado).WithMany(o => o.Cotizaciones).HasForeignKey(o => o.Id_Estado);
            builder.HasOne(o => o.Usuario).WithMany(o => o.Cotizaciones).HasForeignKey(o => o.Id_Usuario);
        }
    }
}