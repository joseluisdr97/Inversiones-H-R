using Microsoft.EntityFrameworkCore;
using MADERERA_HRS.DB.Maps;
using MADERERA_HRS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MADERERA_HRS.DB
{
    public class AppContextDB : DbContext
    {
        public DbSet<Carrito_Cotizacion> Carrito_Cotizaciones { get; set; }
        public DbSet<Carrito_Pedido> Carrito_Pedidos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Cotizacion> Cotizaciones { get; set; }
        public DbSet<Detalle_Cotizacion> Detalle_Cotizaciones { get; set; }
        public DbSet<Detalle_Pedido> Detalle_Pedidos { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Pedido_Presencial> Pedidos_Presenciales { get; set; }
        public DbSet<Detalle_Pedido_Presencial> Detalle_PedidosPresenciales { get; set; }
        public DbSet<Carrito_Pedido_Presencial> Carrito_Pedidos_Presenciales { get; set; }
        public DbSet<Notificacion> Notificaciones { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Calificacion> Calificaciones { get; set; }

        public AppContextDB(DbContextOptions<AppContextDB> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new Carrito_Cotizacion_Map());
            modelBuilder.ApplyConfiguration(new Carrito_Pedido_Map());
            modelBuilder.ApplyConfiguration(new Categoria_Map());
            modelBuilder.ApplyConfiguration(new Cotizacion_Map());
            modelBuilder.ApplyConfiguration(new Detalle_Cotizacion_Map());
            modelBuilder.ApplyConfiguration(new Detalle_Pedido_Map());
            modelBuilder.ApplyConfiguration(new Estado_Map());
            modelBuilder.ApplyConfiguration(new Pedido_Map());
            modelBuilder.ApplyConfiguration(new Producto_Map());
            modelBuilder.ApplyConfiguration(new Rol_Map());
            modelBuilder.ApplyConfiguration(new Usuario_Map());
            modelBuilder.ApplyConfiguration(new Pedido_PresencialMap());
            modelBuilder.ApplyConfiguration(new Detalle_Pedido_PresencialMap());
            modelBuilder.ApplyConfiguration(new Carrito_Pedido_PresencialMap());
            modelBuilder.ApplyConfiguration(new NotificacionMap());
            modelBuilder.ApplyConfiguration(new Comentario_Map());
            modelBuilder.ApplyConfiguration(new Calificacion_Map());
        }
    }
}
