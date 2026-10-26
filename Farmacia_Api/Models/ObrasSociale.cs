using System;
using System.Collections.Generic;

namespace Farmacia_Api.Models;

public partial class ObrasSociale
{
    public int CodObraSocial { get; set; }

    public string Detalle { get; set; } = null!;

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    public virtual ICollection<Cobertura> Coberturas { get; set; } = new List<Cobertura>();
}
