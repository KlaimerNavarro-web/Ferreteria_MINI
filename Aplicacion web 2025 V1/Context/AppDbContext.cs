using Capa_Entidad;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion_web_2025_V1.Context;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Categoria> Categoria => Set<Categoria>();
    public DbSet<Cliente> Cliente => Set<Cliente>();
    public DbSet<Compra> Compra => Set<Compra>();
    public DbSet<Detalle_Compra> Detalle_Compra => Set<Detalle_Compra>();
    public DbSet<Detalle_Factura> Detalle_Factura => Set<Detalle_Factura>();
    public DbSet<Factura> Factura => Set<Factura>();
    public DbSet<Permiso> Permiso => Set<Permiso>();
    public DbSet<Presentacion> Presentacion => Set<Presentacion>();
    public DbSet<Producto> Producto => Set<Producto>();
    public DbSet<Proveedor> Proveedor => Set<Proveedor>();
    public DbSet<Rol> Rol => Set<Rol>();
    public DbSet<Usuario> Usuario => Set<Usuario>();
    public DbSet<Inventario> Inventario => Set<Inventario>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>().HasKey(x => x.id_categoria);
        modelBuilder.Entity<Cliente>().HasKey(x => x.id_cliente);
        modelBuilder.Entity<Compra>().HasKey(x => x.id_compra);
        modelBuilder.Entity<Detalle_Compra>().HasKey(x => x.id_detalle_compra);
        modelBuilder.Entity<Detalle_Factura>().HasKey(x => x.id_detalle_factura);
        modelBuilder.Entity<Factura>().HasKey(x => x.id_factura);
        modelBuilder.Entity<Permiso>().HasKey(x => x.id_permiso);
        modelBuilder.Entity<Presentacion>().HasKey(x => x.id_presentacion);
        modelBuilder.Entity<Producto>().HasKey(x => x.id_producto);
        modelBuilder.Entity<Proveedor>().HasKey(x => x.id_proveedor);
        modelBuilder.Entity<Rol>().HasKey(x => x.id_rol);
        modelBuilder.Entity<Usuario>().HasKey(x => x.id_usuario);
        modelBuilder.Entity<Inventario>().HasKey(x => x.id_inventario);
        modelBuilder.Entity<Producto>().HasOne<Categoria>().WithMany().HasForeignKey(x => x.id_categoria);
        modelBuilder.Entity<Producto>().HasOne<Presentacion>().WithMany().HasForeignKey(x => x.id_presentacion);
        modelBuilder.Entity<Compra>().HasOne<Usuario>().WithMany().HasForeignKey(x => x.id_usuario);
        modelBuilder.Entity<Compra>().HasOne<Proveedor>().WithMany().HasForeignKey(x => x.id_proveedor);
        modelBuilder.Entity<Detalle_Compra>().HasOne<Compra>().WithMany(x => x.Detalle_Compra).HasForeignKey(x => x.id_compra);
        modelBuilder.Entity<Detalle_Compra>().HasOne<Producto>().WithMany().HasForeignKey(x => x.id_producto);
        modelBuilder.Entity<Factura>().HasOne<Cliente>().WithMany().HasForeignKey(x => x.id_cliente);
        modelBuilder.Entity<Factura>().HasOne<Usuario>().WithMany().HasForeignKey(x => x.id_usuario);
        modelBuilder.Entity<Detalle_Factura>().HasOne<Factura>().WithMany(x => x.Detalle_Factura).HasForeignKey(x => x.id_factura);
        modelBuilder.Entity<Detalle_Factura>().HasOne<Producto>().WithMany().HasForeignKey(x => x.id_producto);
        modelBuilder.Entity<Usuario>().HasOne<Rol>().WithMany().HasForeignKey(x => x.oRol);
        modelBuilder.Entity<Permiso>().HasOne<Rol>().WithMany().HasForeignKey(x => x.id_rol);
        modelBuilder.Entity<Inventario>().HasOne<Producto>().WithMany().HasForeignKey(x => x.id_producto);
        modelBuilder.Entity<Inventario>().HasOne<Detalle_Compra>().WithMany().HasForeignKey(x => x.id_detalle_compra);
        modelBuilder.Entity<Inventario>().HasOne<Detalle_Factura>().WithMany().HasForeignKey(x => x.id_detalle_factura);

        base.OnModelCreating(modelBuilder);
    }
}

public sealed class Cliente
{
    public int id_cliente { get; set; }
    public string? documento { get; set; }
    public string nombre { get; set; } = string.Empty;
    public string? correo { get; set; }
    public string? telefono { get; set; }
    public DateTime? fecha_registro { get; set; }
    public bool? estado { get; set; }
}

public sealed class Rol
{
    public int id_rol { get; set; }
    public string? descripcion { get; set; }
    public DateTime? fecha_registro { get; set; }
}

public sealed class Permiso
{
    public int id_permiso { get; set; }
    public int? id_rol { get; set; }
    public string? nombre_menu { get; set; }
    public DateTime? fecha_registro { get; set; }
}

public sealed class Presentacion
{
    public int id_presentacion { get; set; }
    public string? nombre { get; set; }
    public string? unidad_medida { get; set; }
    public string? peso { get; set; }
    public bool? estado { get; set; }
}
