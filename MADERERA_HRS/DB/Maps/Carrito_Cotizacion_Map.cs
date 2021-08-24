using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MADERERA_HRS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MADERERA_HRS.DB.Maps
{
    public class Carrito_Cotizacion_Map : IEntityTypeConfiguration<Carrito_Cotizacion>
    {
        public void Configure(EntityTypeBuilder<Carrito_Cotizacion> builder)
        {
            builder.ToTable("Carrito_Cotizacion");
            builder.HasKey(o => o.Id_Carrito_Cotizacion);

            builder.HasOne(o => o.Usuario).WithMany(o => o.Carrito_Cotizaciones).HasForeignKey(o => o.Id_Usuario);
        }
    }
}