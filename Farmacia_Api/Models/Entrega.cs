using System;
using System.Collections.Generic;

namespace Farmacia_Api.Models;

public partial class Entrega
{
    public int NroEntrega { get; set; }

    public int NroSucursal { get; set; }

    public DateTime? Fecha { get; set; }

    public string FirmaEmpresa { get; set; } = null!;

    public string RespEntrega { get; set; } = null!;

    public int IdEmpleado { get; set; }

    public virtual ICollection<DetalleEntrega> DetalleEntregas { get; set; } = new List<DetalleEntrega>();

    public virtual Empleado IdEmpleadoNavigation { get; set; } = null!;

    public virtual Sucursale NroSucursalNavigation { get; set; } = null!;
}
