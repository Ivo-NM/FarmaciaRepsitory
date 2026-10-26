using System;
using System.Collections.Generic;

namespace Farmacia_Api.Models;

public partial class Barrio
{
    public int IdBarrio { get; set; }

    public string NombreBarrio { get; set; } = null!;

    public int IdLocalidad { get; set; }

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();

    public virtual Localidade IdLocalidadNavigation { get; set; } = null!;

    public virtual ICollection<Sucursale> Sucursales { get; set; } = new List<Sucursale>();
}
