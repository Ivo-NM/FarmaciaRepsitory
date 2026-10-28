using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Farmacia_Api.Models;

public partial class Cobertura
{
    public int CodCobertura { get; set; }

    public int CodObraSocial { get; set; }

    public int CodMedicamento { get; set; }

    public string? TipoCobertura { get; set; }

    [JsonIgnore]
    public virtual Medicamento CodMedicamentoNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual ObrasSociale CodObraSocialNavigation { get; set; } = null!;
}
