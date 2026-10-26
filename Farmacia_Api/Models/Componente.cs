using System;
using System.Collections.Generic;

namespace Farmacia_Api.Models;

public partial class Componente
{
    public int CodComponente { get; set; }

    public string? Detalle { get; set; }

    public virtual ICollection<MedicamentosComponente> MedicamentosComponentes { get; set; } = new List<MedicamentosComponente>();
}
