using System;
using System.Collections.Generic;

namespace Farmacia_Api.Models;

public partial class Sucursale
{
    public int NroSucursal { get; set; }

    public string NomSede { get; set; } = null!;

    public string? Calle { get; set; }

    public int? Altura { get; set; }

    public int IdBarrio { get; set; }

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();

    public virtual ICollection<Entrega> Entregas { get; set; } = new List<Entrega>();

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual Barrio IdBarrioNavigation { get; set; } = null!;

    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();
}
