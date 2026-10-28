using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Farmacia_Api.Models;

public partial class FormasDePago
{
    public int IdFormaPago { get; set; }

    public string? Descripcion { get; set; }

    [JsonIgnore]
    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();
}
