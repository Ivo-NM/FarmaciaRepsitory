using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Farmacia_Api.Models;

public partial class OtrosProducto
{
    public int CodProducto { get; set; }

    public string Detalle { get; set; } = null!;

    public decimal PreUnitario { get; set; }

    [JsonIgnore]
    public virtual ICollection<DetalleEntrega> DetalleEntregas { get; set; } = new List<DetalleEntrega>();

    [JsonIgnore]
    public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; } = new List<DetalleFactura>();

    [JsonIgnore]
    public virtual ICollection<DetalleInventario> DetalleInventarios { get; set; } = new List<DetalleInventario>();
}
