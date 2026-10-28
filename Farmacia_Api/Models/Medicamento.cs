using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Farmacia_Api.Models;

public partial class Medicamento
{
    public int CodMedicamento { get; set; }

    public string NombreMedicamento { get; set; } = null!;

    public bool PrescripcionMedica { get; set; }

    public string? Descripcion { get; set; }

    public string? IndicacionesUso { get; set; }

    public decimal PrecioUnitario { get; set; }

    [JsonIgnore]
    public virtual ICollection<Cobertura> Coberturas { get; set; } = new List<Cobertura>();

    [JsonIgnore]
    public virtual ICollection<DetalleEntrega> DetalleEntregas { get; set; } = new List<DetalleEntrega>();

    [JsonIgnore]
    public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; } = new List<DetalleFactura>();

    [JsonIgnore]
    public virtual ICollection<DetalleInventario> DetalleInventarios { get; set; } = new List<DetalleInventario>();

    [JsonIgnore]
    public virtual ICollection<Lote> Lotes { get; set; } = new List<Lote>();

    [JsonIgnore]
    public virtual ICollection<MedicamentosComponente> MedicamentosComponentes { get; set; } = new List<MedicamentosComponente>();
}
