using System;
using System.Collections.Generic;

namespace Farmacia_Api.Models;

public partial class Medicamento
{
    public int CodMedicamento { get; set; }

    public string NombreMedicamento { get; set; } = null!;

    public bool PrescripcionMedica { get; set; }

    public string? Descripcion { get; set; }

    public string? IndicacionesUso { get; set; }

    public decimal PrecioUnitario { get; set; }

    public virtual ICollection<Cobertura> Coberturas { get; set; } = new List<Cobertura>();

    public virtual ICollection<DetalleEntrega> DetalleEntregas { get; set; } = new List<DetalleEntrega>();

    public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; } = new List<DetalleFactura>();

    public virtual ICollection<DetalleInventario> DetalleInventarios { get; set; } = new List<DetalleInventario>();

    public virtual ICollection<Lote> Lotes { get; set; } = new List<Lote>();

    public virtual ICollection<MedicamentosComponente> MedicamentosComponentes { get; set; } = new List<MedicamentosComponente>();
}
