using System;
using System.Collections.Generic;

namespace Farmacia_Api.Models;

public partial class Cobertura
{
    public int CodCobertura { get; set; }

    public int CodObraSocial { get; set; }

    public int CodMedicamento { get; set; }

    public string? TipoCobertura { get; set; }

    public virtual Medicamento CodMedicamentoNavigation { get; set; } = null!;

    public virtual ObrasSociale CodObraSocialNavigation { get; set; } = null!;
}
