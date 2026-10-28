using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

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

    [JsonIgnore]
    public virtual ICollection<Entrega> Entregas { get; set; } = new List<Entrega>();

    [JsonIgnore]
    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual Barrio IdBarrioNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();

    [JsonIgnore]
    public virtual Sucursale NroSucursalNavigation { get; set; } = null!;
}
