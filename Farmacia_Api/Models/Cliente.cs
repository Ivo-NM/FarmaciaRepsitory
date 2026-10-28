using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Farmacia_Api.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public int Documento { get; set; }

    public string? Calle { get; set; }

    public int? Altura { get; set; }

    public int IdBarrio { get; set; }

    public int? NroTelefono { get; set; }

    public string? EMail { get; set; }

    public int? CodObraSocial { get; set; }

    public virtual ObrasSociale? CodObraSocialNavigation { get; set; }

    [JsonIgnore]
    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual Barrio IdBarrioNavigation { get; set; } = null!;
}
