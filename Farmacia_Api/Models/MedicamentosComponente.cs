using System;
using System.Collections.Generic;

namespace Farmacia_Api.Models;

public partial class MedicamentosComponente
{
    public int Id { get; set; }

    public int CodMedicamento { get; set; }

    public int CodComponente { get; set; }

    public decimal? Cantidad { get; set; }

    public string? UnidadMedida { get; set; }

    public virtual Componente CodComponenteNavigation { get; set; } = null!;

    public virtual Medicamento CodMedicamentoNavigation { get; set; } = null!;
}
