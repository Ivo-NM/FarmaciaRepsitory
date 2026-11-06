using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;
using System.Text.Json.Serialization;

namespace Farmacia_Api.Models
{
    [Table("USUARIOS")]
    public class Usuario
    {
        [Key]
        [ForeignKey(nameof(Empleado))]
        public int id_empleado { get; set; }
        [Required]
        public string contrasenia { get; set; }
        [Required]
        public string correo { get; set; }
        [JsonIgnore]
        public virtual Empleado EmpleadosNavegador { get; set; } = null;
    }
}
