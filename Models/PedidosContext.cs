using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using pedidos_dap5.Models.ViewModels;

namespace pedidos_dap5.Models;

public partial class PedidosContext : DbContext
{
    public PedidosContext()
    {
    }

    public PedidosContext(DbContextOptions<PedidosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<Grado> Grados { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<PedidosHistorial> PedidosHistorials { get; set; }

    public virtual DbSet<Solucione> Soluciones { get; set; }

    public virtual DbSet<Tecnico> Tecnicos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Leer la connection string del archivo appsettings.json
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        // Pasar la connection string al método UseSqlServer
        optionsBuilder.UseSqlServer(connectionString);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.Idestado);

            entity.Property(e => e.Idestado).HasColumnName("idestado");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Grado>(entity =>
        {
            entity.HasKey(e => e.IdGrado);

            entity.Property(e => e.IdGrado).HasColumnName("idGrado");
            entity.Property(e => e.Grado1)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("Grado");
        });

        modelBuilder.Entity<Pedido>()
        .ToTable(tb => tb.HasTrigger("llenarHistorial")); 

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.Idpedido).HasName("PK_pedidos");



            entity.Property(e => e.Idpedido).HasColumnName("idpedido");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaInicio)
                .HasColumnType("datetime")
                .HasColumnName("fecha_inicio");
            entity.Property(e => e.Tecnico).HasColumnName("tecnico");
            entity.Property(e => e.Usuario).HasColumnName("usuario");

            entity.HasOne(d => d.EstadoNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.Estado)
                .HasConstraintName("FK_Pedidos_Estados");

            entity.HasOne(d => d.TecnicoNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.Tecnico)
                .HasConstraintName("FK_pedidos_tecnicos");

            entity.HasOne(d => d.UsuarioNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.Usuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_pedidos_usuarios");
        });

        modelBuilder.Entity<PedidosHistorial>(entity =>
        {
            entity.HasKey(e => e.IdHistorial);

            entity.ToTable("Pedidos_historial");

            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("fecha");

            entity.HasOne(d => d.EstadoNavigation).WithMany(p => p.PedidosHistorials)
                .HasForeignKey(d => d.Estado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pedidos_historial_Estado");

            entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.PedidosHistorials)
                .HasForeignKey(d => d.IdPedido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pedidos_Pedidos_historial");
        });

        modelBuilder.Entity<Solucione>(entity =>
        {
            entity.HasKey(e => e.Idsolucion).HasName("PK_soluciones");

            entity.Property(e => e.Idsolucion).HasColumnName("idsolucion");
            entity.Property(e => e.FechaSolucion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_solucion");
            entity.Property(e => e.Idpedido).HasColumnName("idpedido");
            entity.Property(e => e.Solucion)
                .HasMaxLength(200)
                .HasColumnName("solucion");
            entity.Property(e => e.Tecnico).HasColumnName("tecnico");

            entity.HasOne(d => d.IdpedidoNavigation).WithMany(p => p.Soluciones)
                .HasForeignKey(d => d.Idpedido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_soluciones_pedidos");

            entity.HasOne(d => d.TecnicoNavigation).WithMany(p => p.Soluciones)
                .HasForeignKey(d => d.Tecnico)
                .HasConstraintName("FK_Soluciones_Tecnico");
        });

        modelBuilder.Entity<Tecnico>(entity =>
        {
            entity.HasKey(e => e.Idtecnico).HasName("PK_tecnicos");

            entity.Property(e => e.Idtecnico).HasColumnName("idtecnico");
            entity.Property(e => e.CuentaRina)
                .HasMaxLength(50)
                .HasColumnName("cuenta_rina");
            entity.Property(e => e.Interno)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("interno");
            entity.Property(e => e.Tecnico1)
                .HasMaxLength(50)
                .HasColumnName("tecnico");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Idusuaio).HasName("PK_usuarios");

            entity.Property(e => e.Idusuaio).HasColumnName("idusuaio");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Grado).HasColumnName("grado");
            entity.Property(e => e.Interno)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.TelParticular)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("tel_particular");
            entity.Property(e => e.Usuario1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("usuario");

            entity.HasOne(d => d.GradoNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.Grado)
                .HasConstraintName("FK_Usuarios_Grados");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public DbSet<pedidos_dap5.Models.ViewModels.ViewModelHistorialPedido> ViewModelHistorialPedido { get; set; }

    public DbSet<pedidos_dap5.Models.ViewModels.ViewModelPedido> ViewModelPedido { get; set; }
}
