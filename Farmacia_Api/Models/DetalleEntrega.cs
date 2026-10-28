using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Farmacia_Api.Models;

public partial class DetalleEntrega
{
    public int NroDetalle { get; set; }

    public int? NroEntrega { get; set; }

    public int? CodMedicamento { get; set; }

    public int? CodProducto { get; set; }

    public int Cant { get; set; }

    public virtual Medicamento? CodMedicamentoNavigation { get; set; }

    public virtual OtrosProducto? CodProductoNavigation { get; set; }

    [JsonIgnore]
    public virtual Entrega? NroEntregaNavigation { get; set; }
}
