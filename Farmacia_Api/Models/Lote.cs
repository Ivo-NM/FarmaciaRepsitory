using System;
using System.Collections.Generic;

namespace Farmacia_Api.Models;

public partial class Lote
{
    public int NroLote { get; set; }

    public int CodMedicamento { get; set; }

    public DateTime FechaFabricacion { get; set; }

    public DateTime FechaVencimiento { get; set; }

    public virtual Medicamento CodMedicamentoNavigation { get; set; } = null!;
}
