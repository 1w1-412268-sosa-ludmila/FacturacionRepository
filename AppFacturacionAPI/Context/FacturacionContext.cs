using System;
using System.Collections.Generic;
using AppFacturacionAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppFacturacionAPI.Context;

public partial class FacturacionContext : DbContext
{
    public FacturacionContext()
    {
    }

    public FacturacionContext(DbContextOptions<FacturacionContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Articulo> Articulos { get; set; }

    public virtual DbSet<DetallesFactura> DetallesFacturas { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<FormasPago> FormasPagos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-PVTAFR9\\SQLEXPRESS;Database=Facturacion;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Articulo>(entity =>
        {
            entity.HasKey(e => e.IdArticulo).HasName("PK__Articulo__3F6E828818D348EF");

            entity.Property(e => e.IdArticulo).HasColumnName("id_articulo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.PrecioUnitario)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precioUnitario");
        });

        modelBuilder.Entity<DetallesFactura>(entity =>
        {
            entity.HasKey(e => e.IdDetalle).HasName("PK__Detalles__4F1332DECF0E91F4");

            entity.ToTable("DetallesFactura");

            entity.Property(e => e.IdDetalle).HasColumnName("id_detalle");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.IdArticulo).HasColumnName("id_articulo");
            entity.Property(e => e.NroFactura).HasColumnName("nro_factura");

            entity.HasOne(d => d.IdArticuloNavigation).WithMany(p => p.DetallesFacturas)
                .HasForeignKey(d => d.IdArticulo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_id_articulo");

            entity.HasOne(d => d.NroFacturaNavigation).WithMany(p => p.DetallesFacturas)
                .HasForeignKey(d => d.NroFactura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_nro_factura");
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.NroFactura).HasName("PK__Facturas__B31FA9AF667B5991");

            entity.Property(e => e.NroFactura).HasColumnName("nro_factura");
            entity.Property(e => e.Cliente)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cliente");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.IdFormapago).HasColumnName("id_formapago");

            entity.HasOne(d => d.IdFormapagoNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdFormapago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_formapago");
        });

        modelBuilder.Entity<FormasPago>(entity =>
        {
            entity.HasKey(e => e.IdFormapago).HasName("PK__FormasPa__30C4DB33D88CBB08");

            entity.ToTable("FormasPago");

            entity.Property(e => e.IdFormapago).HasColumnName("id_formapago");
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
