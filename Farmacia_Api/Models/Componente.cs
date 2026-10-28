using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Farmacia_Api.Models;

public partial class Componente
{
    public int CodComponente { get; set; }

    public string? Detalle { get; set; }

    [JsonIgnore]
    public virtual ICollection<MedicamentosComponente> MedicamentosComponentes { get; set; } = new List<MedicamentosComponente>();
}
