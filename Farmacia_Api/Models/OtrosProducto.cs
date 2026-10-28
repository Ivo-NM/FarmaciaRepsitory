using System;
using System.Collections.Generic;

namespace Farmacia_Api.Models;

public partial class OtrosProducto
{
    public int CodProducto { get; set; }

    public string Detalle { get; set; } = null!;

    public decimal PreUnitario { get; set; }

    public virtual ICollection<DetalleEntrega> DetalleEntregas { get; set; } = new List<DetalleEntrega>();

    public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; } = new List<DetalleFactura>();

    public virtual ICollection<DetalleInventario> DetalleInventarios { get; set; } = new List<DetalleInventario>();
}
