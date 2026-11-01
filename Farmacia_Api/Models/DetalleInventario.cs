using System;
using System.Collections.Generic;

namespace Farmacia_Api.Models;

public partial class DetalleInventario
{
    public int NroDetalle { get; set; }

    public int? NroInventario { get; set; }

    public int? CodMedicamento { get; set; }

    public int? CodProducto { get; set; }

    public int Stock { get; set; }

    public virtual Medicamento? CodMedicamentoNavigation { get; set; } = null;

    public virtual OtrosProducto? CodProductoNavigation { get; set; } = null;

    public virtual Inventario? NroInventarioNavigation { get; set; } = null;
}
