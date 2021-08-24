using MADERERA_HRS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MADERERA_HRS.DB.Maps
{
    public class Detalle_Pedido_PresencialMap : IEntityTypeConfiguration<Detalle_Pedido_Presencial>
    {
        public void Configure(EntityTypeBuilder<Detalle_Pedido_Presencial> builder)
        {
            builder.ToTable("Detalle_Pedido_Presencial");
            builder.HasKey(o => o.Id_Detalle_Pedido_Presencial);

            builder.HasOne(o => o.Estado).WithMany(o => o.Detalle_Pedidos_Presenciales).HasForeignKey(o => o.Id_Estado);
            builder.HasOne(o => o.Pedido_Presencial).WithMany(o => o.Detalle_Pedidos_Presenciales).HasForeignKey(o => o.Id_Pedido_Presencial);
        }
    }
}