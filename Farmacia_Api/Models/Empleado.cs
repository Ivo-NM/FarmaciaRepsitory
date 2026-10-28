using System;
using System.Collections.Generic;

namespace Farmacia_Api.Models;

public partial class Empleado
{
    public int IdEmpleado { get; set; }

    public string NomEmpleado { get; set; } = null!;

    public string ApellEmpleado { get; set; } = null!;

    public string? Calle { get; set; }

    public int? Altura { get; set; }

    public int IdBarrio { get; set; }

    public string Ocupacion { get; set; } = null!;

    public int NroSucursal { get; set; }

    public virtual ICollection<Entrega> Entregas { get; set; } = new List<Entrega>();

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual Barrio IdBarrioNavigation { get; set; } = null!;

    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();

    public virtual Sucursale NroSucursalNavigation { get; set; } = null!;
}
