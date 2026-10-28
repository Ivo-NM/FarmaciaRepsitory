using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Farmacia_Api.Models;

public partial class Barrio
{
    public int IdBarrio { get; set; }

    public string NombreBarrio { get; set; } = null!;

    public int IdLocalidad { get; set; }

    [JsonIgnore]
    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    [JsonIgnore]
    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();

    public virtual Localidade IdLocalidadNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Sucursale> Sucursales { get; set; } = new List<Sucursale>();
}
