using System;
using System.Collections.Generic;

namespace Farmacia_Api.Models;

public partial class DetalleFactura
{
    public int NroDetalle { get; set; }

    public int NroFactura { get; set; }

    public int? CodMedicamento { get; set; }

    public int? CodProducto { get; set; }

    public decimal PreUnitario { get; set; }

    public int Cantidad { get; set; }

    public decimal? DescuentoPorObraSocial { get; set; }

    public bool Reembolsado { get; set; }

    public virtual Medicamento? CodMedicamentoNavigation { get; set; }

    public virtual OtrosProducto? CodProductoNavigation { get; set; }

    public virtual Factura NroFacturaNavigation { get; set; } = null!;
}
