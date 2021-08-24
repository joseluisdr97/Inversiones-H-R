using MADERERA_HRS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MADERERA_HRS.DB.Maps
{
    public class Pedido_PresencialMap : IEntityTypeConfiguration<Pedido_Presencial>
    {
        public void Configure(EntityTypeBuilder<Pedido_Presencial> builder)
        {
            builder.ToTable("Pedido_Presencial");
            builder.HasKey(o => o.Id_Pedido_Presencial);

            builder.HasOne(o => o.Estado).WithMany(o => o.Pedidos_Presenciales).HasForeignKey(o => o.Id_Estado);
        }
    }
}