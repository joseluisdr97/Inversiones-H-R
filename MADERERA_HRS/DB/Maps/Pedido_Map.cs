using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MADERERA_HRS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MADERERA_HRS.DB.Maps
{
    public class Pedido_Map : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable("Pedido");
            builder.HasKey(o => o.Id_Pedido);

            builder.HasOne(o => o.Estado).WithMany(o => o.Pedidos).HasForeignKey(o => o.Id_Estado);
            builder.HasOne(o => o.Usuario).WithMany(o => o.Pedidos).HasForeignKey(o => o.Id_Usuario);
        }
    }
}