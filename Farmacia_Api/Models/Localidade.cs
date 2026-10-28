using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Farmacia_Api.Models;

public partial class Localidade
{
    public int IdLocalidad { get; set; }

    public string NombreLocalidad { get; set; } = null!;

    public int IdProvincia { get; set; }

    public virtual ICollection<Barrio> Barrios { get; set; } = new List<Barrio>();

    [JsonIgnore]
    public virtual Provincia IdProvinciaNavigation { get; set; } = null!;
}
