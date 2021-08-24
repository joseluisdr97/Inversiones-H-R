using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MADERERA_HRS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MADERERA_HRS.DB.Maps
{
    public class Usuario_Map : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");
            builder.HasKey(o => o.Id_Usuario);

            builder.HasOne(o => o.Rol).WithMany(o => o.Usuarios).HasForeignKey(o => o.Id_Rol);
        }
    }
}