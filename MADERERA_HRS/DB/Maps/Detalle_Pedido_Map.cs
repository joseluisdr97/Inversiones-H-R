using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MADERERA_HRS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MADERERA_HRS.DB.Maps
{
    public class Detalle_Pedido_Map : IEntityTypeConfiguration<Detalle_Pedido>
    {
        public void Configure(EntityTypeBuilder<Detalle_Pedido> builder)
        {
            builder.ToTable("Detalle_Pedido");
            builder.HasKey(o => o.Id_Detalle_Pedido);

            builder.HasOne(o => o.Pedido).WithMany(o => o.Detalle_Pedidos).HasForeignKey(o => o.Id_Pedido);
            builder.HasOne(o => o.Producto).WithMany(o => o.Detalle_Pedidos).HasForeignKey(o => o.Id_Producto);
            builder.HasOne(o => o.Estado).WithMany(o => o.Detalle_Pedidos).HasForeignKey(o => o.Id_Estado);
        }
    }
}