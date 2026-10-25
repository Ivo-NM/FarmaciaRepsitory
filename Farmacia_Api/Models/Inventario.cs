using System;
using System.Collections.Generic;

namespace Farmacia_Api.Models;

public partial class Inventario
{
    public int NroInventario { get; set; }

    public int NroSucursal { get; set; }

    public DateTime? FechaInventario { get; set; }

    public int IdEmpleado { get; set; }

    public virtual ICollection<DetalleInventario> DetalleInventarios { get; set; } = new List<DetalleInventario>();

    public virtual Empleado IdEmpleadoNavigation { get; set; } = null!;

    public virtual Sucursale NroSucursalNavigation { get; set; } = null!;
}
