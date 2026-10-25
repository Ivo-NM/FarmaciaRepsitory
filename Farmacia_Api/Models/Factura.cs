using System;
using System.Collections.Generic;

namespace Farmacia_Api.Models;

public partial class Factura
{
    public int NroFactura { get; set; }

    public int NroSucursal { get; set; }

    public DateTime Fecha { get; set; }

    public int IdEmpleado { get; set; }

    public int IdCliente { get; set; }

    public int IdFormaPago { get; set; }

    public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; } = new List<DetalleFactura>();

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual Empleado IdEmpleadoNavigation { get; set; } = null!;

    public virtual FormasDePago IdFormaPagoNavigation { get; set; } = null!;

    public virtual Sucursale NroSucursalNavigation { get; set; } = null!;
}
