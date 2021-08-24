using MADERERA_HRS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MADERERA_HRS.DB.Maps
{
    public class Carrito_Pedido_PresencialMap : IEntityTypeConfiguration<Carrito_Pedido_Presencial>
    {
        public void Configure(EntityTypeBuilder<Carrito_Pedido_Presencial> builder)
        {
            builder.ToTable("Carrito_Pedido_Presencial");
            builder.HasKey(o => o.Id_Carrito_Pedido_Presencial);
        }
    }
}