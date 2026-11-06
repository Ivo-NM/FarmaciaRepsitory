using System;
using System.Collections.Generic;
using Farmacia_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Farmacia_Api.Data;

public partial class FarmaciaDbContext : DbContext
{
    public FarmaciaDbContext()
    {
    }

    public FarmaciaDbContext(DbContextOptions<FarmaciaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Barrio> Barrios { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Cobertura> Coberturas { get; set; }

    public virtual DbSet<Componente> Componentes { get; set; }

    public virtual DbSet<DetalleEntrega> DetalleEntregas { get; set; }

    public virtual DbSet<DetalleFactura> DetalleFacturas { get; set; }

    public virtual DbSet<DetalleInventario> DetalleInventarios { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Entrega> Entregas { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<FormasDePago> FormasDePagos { get; set; }

    public virtual DbSet<Inventario> Inventarios { get; set; }

    public virtual DbSet<Localidade> Localidades { get; set; }

    public virtual DbSet<Lote> Lotes { get; set; }

    public virtual DbSet<Medicamento> Medicamentos { get; set; }

    public virtual DbSet<MedicamentosComponente> MedicamentosComponentes { get; set; }

    public virtual DbSet<ObrasSociale> ObrasSociales { get; set; }

    public virtual DbSet<OtrosProducto> OtrosProductos { get; set; }

    public virtual DbSet<Provincia> Provincias { get; set; }

    public virtual DbSet<Sucursale> Sucursales { get; set; }
    public virtual DbSet<Usuario> Usuarios { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Barrio>(entity =>
        {
            entity.HasKey(e => e.IdBarrio).HasName("PK_BARRIO");

            entity.ToTable("BARRIOS");

            entity.Property(e => e.IdBarrio).HasColumnName("Id_Barrio");
            entity.Property(e => e.IdLocalidad).HasColumnName("Id_Localidad");
            entity.Property(e => e.NombreBarrio)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Nombre_Barrio");

            entity.HasOne(d => d.IdLocalidadNavigation).WithMany(p => p.Barrios)
                .HasForeignKey(d => d.IdLocalidad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BARRIO_LOCALIDAD");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK_Cliente");

            entity.ToTable("CLIENTES");

            entity.Property(e => e.IdCliente).HasColumnName("Id_Cliente");
            entity.Property(e => e.Apellido)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Calle)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.CodObraSocial).HasColumnName("Cod_Obra_Social");
            entity.Property(e => e.EMail)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("e-mail");
            entity.Property(e => e.IdBarrio).HasColumnName("Id_Barrio");
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.NroTelefono).HasColumnName("Nro_telefono");

            entity.HasOne(d => d.ObraSocialNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.CodObraSocial)
                .HasConstraintName("FK_CLIENTE_ObraSocial");

            entity.HasOne(d => d.BarrioNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.IdBarrio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CLIENTE_BARRIO");
        });

        modelBuilder.Entity<Cobertura>(entity =>
        {
            entity.HasKey(e => e.CodCobertura).HasName("PK_COBERTURA");

            entity.ToTable("COBERTURAS");

            entity.Property(e => e.CodCobertura).HasColumnName("Cod_Cobertura");
            entity.Property(e => e.CodMedicamento).HasColumnName("Cod_Medicamento");
            entity.Property(e => e.CodObraSocial).HasColumnName("Cod_Obra_Social");
            entity.Property(e => e.TipoCobertura)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Tipo_Cobertura");

            entity.HasOne(d => d.CodMedicamentoNavigation).WithMany(p => p.Coberturas)
                .HasForeignKey(d => d.CodMedicamento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_COBERTURA_MEDICAMENTOS");

            entity.HasOne(d => d.CodObraSocialNavigation).WithMany(p => p.Coberturas)
                .HasForeignKey(d => d.CodObraSocial)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_COBERTURA_OBRASOCIAL");
        });

        modelBuilder.Entity<Componente>(entity =>
        {
            entity.HasKey(e => e.CodComponente).HasName("PK_COMPOSICION");

            entity.ToTable("COMPONENTES");

            entity.Property(e => e.CodComponente).HasColumnName("Cod_componente");
            entity.Property(e => e.Detalle)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DetalleEntrega>(entity =>
        {
            entity.HasKey(e => e.NroDetalle).HasName("PK_DETALLE_ENTR");

            entity.ToTable("DETALLE_ENTREGAS");

            entity.Property(e => e.NroDetalle).HasColumnName("Nro_Detalle");
            entity.Property(e => e.CodMedicamento).HasColumnName("Cod_Medicamento");
            entity.Property(e => e.CodProducto).HasColumnName("Cod_Producto");
            entity.Property(e => e.NroEntrega).HasColumnName("Nro_Entrega");

            entity.HasOne(d => d.CodMedicamentoNavigation).WithMany(p => p.DetalleEntregas)
                .HasForeignKey(d => d.CodMedicamento)
                .HasConstraintName("FK_ENTREGA_MEDICAMENTO");

            entity.HasOne(d => d.CodProductoNavigation).WithMany(p => p.DetalleEntregas)
                .HasForeignKey(d => d.CodProducto)
                .HasConstraintName("FK_ENTREGA_PRODUCTOS");

            entity.HasOne(d => d.NroEntregaNavigation).WithMany(p => p.DetalleEntregas)
                .HasForeignKey(d => d.NroEntrega)
                .HasConstraintName("FK_DETALLE_ENTREGA");
        });

        modelBuilder.Entity<DetalleFactura>(entity =>
        {
            entity.HasKey(e => e.NroDetalle).HasName("PK_DETALLE_FAC");

            entity.ToTable("DETALLE_FACTURAS");

            entity.Property(e => e.NroDetalle).HasColumnName("Nro_Detalle");
            entity.Property(e => e.CodMedicamento).HasColumnName("Cod_Medicamento");
            entity.Property(e => e.CodProducto).HasColumnName("Cod_Producto");
            entity.Property(e => e.DescuentoPorObraSocial)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("Descuento_por_obra_Social");
            entity.Property(e => e.NroFactura).HasColumnName("Nro_Factura");
            entity.Property(e => e.PreUnitario)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("Pre_Unitario");
            entity.Property(e => e.Reembolsado).HasColumnName("reembolsado");

            entity.HasOne(d => d.CodMedicamentoNavigation).WithMany(p => p.DetalleFacturas)
                .HasForeignKey(d => d.CodMedicamento)
                .HasConstraintName("FK_FACTURA_MEDICAMENTO");

            entity.HasOne(d => d.CodProductoNavigation).WithMany(p => p.DetalleFacturas)
                .HasForeignKey(d => d.CodProducto)
                .HasConstraintName("FK_FACTURA_PRODUCTOS");

            entity.HasOne(d => d.NroFacturaNavigation).WithMany(p => p.DetalleFacturas)
                .HasForeignKey(d => d.NroFactura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DETALLE_FACTURA");
        });

        modelBuilder.Entity<DetalleInventario>(entity =>
        {
            entity.HasKey(e => e.NroDetalle).HasName("PK_DETALLE_INV");

            entity.ToTable("DETALLE_INVENTARIOS");

            entity.Property(e => e.NroDetalle).HasColumnName("Nro_Detalle");
            entity.Property(e => e.CodMedicamento).HasColumnName("Cod_Medicamento");
            entity.Property(e => e.CodProducto).HasColumnName("Cod_Producto");
            entity.Property(e => e.NroInventario).HasColumnName("Nro_Inventario");

            entity.HasOne(d => d.CodMedicamentoNavigation).WithMany(p => p.DetalleInventarios)
                .HasForeignKey(d => d.CodMedicamento)
                .HasConstraintName("FK_INVENTARIO_MEDICAMENTO");

            entity.HasOne(d => d.CodProductoNavigation).WithMany(p => p.DetalleInventarios)
                .HasForeignKey(d => d.CodProducto)
                .HasConstraintName("FK_INVENTARIO_PRODUCTOS");

            entity.HasOne(d => d.NroInventarioNavigation).WithMany(p => p.DetalleInventarios)
                .HasForeignKey(d => d.NroInventario)
                .HasConstraintName("FK_DETALLE_INVENTARIO");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado).HasName("PK_EMPLEADO");

            entity.ToTable("EMPLEADOS");

            entity.Property(e => e.IdEmpleado).HasColumnName("Id_Empleado");
            entity.Property(e => e.ApellEmpleado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Apell_Empleado");
            entity.Property(e => e.Calle)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.IdBarrio).HasColumnName("Id_Barrio");
            entity.Property(e => e.NomEmpleado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Nom_Empleado");
            entity.Property(e => e.NroSucursal).HasColumnName("Nro_sucursal");
            entity.Property(e => e.Ocupacion)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdBarrioNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdBarrio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EMPLEADO_BARRIO");

            entity.HasOne(d => d.NroSucursalNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.NroSucursal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EMPLEADO_FARMACIA");

        });

        modelBuilder.Entity<Entrega>(entity =>
        {
            entity.HasKey(e => e.NroEntrega).HasName("PK_ENTREGA");

            entity.ToTable("ENTREGAS");

            entity.Property(e => e.NroEntrega).HasColumnName("Nro_Entrega");
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.FirmaEmpresa)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Firma_Empresa");
            entity.Property(e => e.IdEmpleado).HasColumnName("Id_Empleado");
            entity.Property(e => e.NroSucursal).HasColumnName("Nro_sucursal");
            entity.Property(e => e.RespEntrega)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Resp_Entrega");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.Entregas)
                .HasForeignKey(d => d.IdEmpleado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ENTREGA_EMPLEADO");

            entity.HasOne(d => d.NroSucursalNavigation).WithMany(p => p.Entregas)
                .HasForeignKey(d => d.NroSucursal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ENTREGA_FARMACIA");
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.NroFactura).HasName("PK_FACTURA");

            entity.ToTable("FACTURAS");

            entity.Property(e => e.NroFactura).HasColumnName("Nro_Factura");
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.IdCliente).HasColumnName("Id_Cliente");
            entity.Property(e => e.IdEmpleado).HasColumnName("Id_Empleado");
            entity.Property(e => e.IdFormaPago).HasColumnName("id_forma_pago");
            entity.Property(e => e.NroSucursal).HasColumnName("Nro_sucursal");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FACTURAS_CLIENTE");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdEmpleado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FACTURAS_VENDEDOR");

            entity.HasOne(d => d.IdFormaPagoNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdFormaPago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Factura_FormaPago");

            entity.HasOne(d => d.NroSucursalNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.NroSucursal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FACTURA_FARMACIA");
        });

        modelBuilder.Entity<FormasDePago>(entity =>
        {
            entity.HasKey(e => e.IdFormaPago).HasName("PK_forma_pago");

            entity.ToTable("FORMAS_DE_PAGO");

            entity.Property(e => e.IdFormaPago).HasColumnName("id_forma_pago");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<Inventario>(entity =>
        {
            entity.HasKey(e => e.NroInventario).HasName("PK_INVENTARIO");

            entity.ToTable("INVENTARIOS");

            entity.Property(e => e.NroInventario).HasColumnName("Nro_Inventario");
            entity.Property(e => e.FechaInventario)
                .HasColumnType("datetime")
                .HasColumnName("fecha_inventario");
            entity.Property(e => e.IdEmpleado).HasColumnName("Id_Empleado");
            entity.Property(e => e.NroSucursal).HasColumnName("Nro_sucursal");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.Inventarios)
                .HasForeignKey(d => d.IdEmpleado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_INVENTARIO_EMPLEADO");

            entity.HasOne(d => d.NroSucursalNavigation).WithMany(p => p.Inventarios)
                .HasForeignKey(d => d.NroSucursal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_INVENTARIO_FARMACIA");
        });

        modelBuilder.Entity<Localidade>(entity =>
        {
            entity.HasKey(e => e.IdLocalidad).HasName("PK_LOCALIDAD");

            entity.ToTable("LOCALIDADES");

            entity.Property(e => e.IdLocalidad).HasColumnName("Id_Localidad");
            entity.Property(e => e.IdProvincia).HasColumnName("Id_Provincia");
            entity.Property(e => e.NombreLocalidad)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Nombre_Localidad");

            entity.HasOne(d => d.IdProvinciaNavigation).WithMany(p => p.Localidades)
                .HasForeignKey(d => d.IdProvincia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LOCALIDA_PROVINCIA");
        });

        modelBuilder.Entity<Lote>(entity =>
        {
            entity.HasKey(e => e.NroLote).HasName("PK_LOTE");

            entity.ToTable("LOTES");

            entity.Property(e => e.NroLote)
                .ValueGeneratedNever()
                .HasColumnName("Nro_Lote");
            entity.Property(e => e.CodMedicamento).HasColumnName("Cod_Medicamento");
            entity.Property(e => e.FechaFabricacion)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_Fabricacion");
            entity.Property(e => e.FechaVencimiento)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_Vencimiento");

            entity.HasOne(d => d.CodMedicamentoNavigation).WithMany(p => p.Lotes)
                .HasForeignKey(d => d.CodMedicamento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LOTE_MEDICAMENTO");
        });

        modelBuilder.Entity<Medicamento>(entity =>
        {
            entity.HasKey(e => e.CodMedicamento).HasName("PK_SUMINISTRO");

            entity.ToTable("MEDICAMENTOS");

            entity.Property(e => e.CodMedicamento).HasColumnName("Cod_Medicamento");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IndicacionesUso)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Indicaciones_Uso");
            entity.Property(e => e.NombreMedicamento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Nombre_Medicamento");
            entity.Property(e => e.PrecioUnitario)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Precio_Unitario");
            entity.Property(e => e.PrescripcionMedica).HasColumnName("Prescripcion_Medica");
        });

        modelBuilder.Entity<MedicamentosComponente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Med_componentes");

            entity.ToTable("MEDICAMENTOS_COMPONENTES");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cantidad)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("cantidad");
            entity.Property(e => e.CodComponente).HasColumnName("Cod_componente");
            entity.Property(e => e.CodMedicamento).HasColumnName("Cod_medicamento");
            entity.Property(e => e.UnidadMedida)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("unidad_medida");

            entity.HasOne(d => d.CodComponenteNavigation).WithMany(p => p.MedicamentosComponentes)
                .HasForeignKey(d => d.CodComponente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_COMP_VARIOS");

            entity.HasOne(d => d.CodMedicamentoNavigation).WithMany(p => p.MedicamentosComponentes)
                .HasForeignKey(d => d.CodMedicamento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MED_VARIOS");
        });

        modelBuilder.Entity<ObrasSociale>(entity =>
        {
            entity.HasKey(e => e.CodObraSocial).HasName("FK_OBRA_SOCIAL");

            entity.ToTable("OBRAS_SOCIALES");

            entity.Property(e => e.CodObraSocial).HasColumnName("Cod_Obra_Social");
            entity.Property(e => e.Detalle)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<OtrosProducto>(entity =>
        {
            entity.HasKey(e => e.CodProducto).HasName("PK_PRODUCTO");

            entity.ToTable("OTROS_PRODUCTOS");

            entity.Property(e => e.CodProducto).HasColumnName("Cod_Producto");
            entity.Property(e => e.Detalle)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("detalle");
            entity.Property(e => e.PreUnitario)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Pre_unitario");
        });

        modelBuilder.Entity<Provincia>(entity =>
        {
            entity.HasKey(e => e.IdProvincia);

            entity.ToTable("PROVINCIAS");

            entity.Property(e => e.IdProvincia).HasColumnName("Id_Provincia");
            entity.Property(e => e.NombreProvincia)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Nombre_Provincia");
        });

        modelBuilder.Entity<Sucursale>(entity =>
        {
            entity.HasKey(e => e.NroSucursal).HasName("PK_FARMACIA");

            entity.ToTable("SUCURSALES");

            entity.Property(e => e.NroSucursal).HasColumnName("Nro_sucursal");
            entity.Property(e => e.Calle)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.IdBarrio).HasColumnName("Id_Barrio");
            entity.Property(e => e.NomSede)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Nom_Sede");

            entity.HasOne(d => d.IdBarrioNavigation).WithMany(p => p.Sucursales)
                .HasForeignKey(d => d.IdBarrio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SUCURSAL_BARRIO");
        });
        modelBuilder.Entity<Usuario>()
            .HasKey(u => u.id_empleado);
        modelBuilder.Entity<Usuario>()
            .HasOne(u => u.EmpleadosNavegador)
            .WithOne(e => e.UsuariosNavegation)
            .HasForeignKey<Usuario>(u => u.id_empleado);


        OnModelCreatingPartial(modelBuilder);
    }

    


    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
