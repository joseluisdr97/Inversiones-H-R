using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MADERERA_HRS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MADERERA_HRS.DB.Maps
{
    public class Carrito_Pedido_Map : IEntityTypeConfiguration<Carrito_Pedido>
    {
        public void Configure(EntityTypeBuilder<Carrito_Pedido> builder)
        {
            builder.ToTable("Carrito_Pedido");
            builder.HasKey(o => o.Id_Carrito_Pedido);

            builder.HasOne(o => o.Usuario).WithMany(o => o.Carrito_Pedidos).HasForeignKey(o => o.Id_Usuario);
            builder.HasOne(o => o.Producto).WithMany(o => o.Carrito_Pedidos).HasForeignKey(o => o.Id_Producto);
        }
    }
}