using System;
using System.Collections.Generic;

namespace Farmacia_Api.Models;

public partial class FormasDePago
{
    public int IdFormaPago { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();
}
