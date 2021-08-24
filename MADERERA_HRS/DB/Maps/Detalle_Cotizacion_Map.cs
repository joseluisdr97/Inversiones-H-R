using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MADERERA_HRS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MADERERA_HRS.DB.Maps
{
    public class Detalle_Cotizacion_Map : IEntityTypeConfiguration<Detalle_Cotizacion>
    {
        public void Configure(EntityTypeBuilder<Detalle_Cotizacion> builder)
        {
            builder.ToTable("Detalle_Cotizacion");
            builder.HasKey(o => o.Id_Detalle_Cotizacion);

            builder.HasOne(o => o.Cotizacion).WithMany(o => o.Detalle_Cotizaciones).HasForeignKey(o => o.Id_Cotizacion);
            builder.HasOne(o => o.Estado).WithMany(o => o.Detalle_Cotizaciones).HasForeignKey(o => o.Id_Estado);
        }
    }
}