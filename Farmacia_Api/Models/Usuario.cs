using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Farmacia_Api.Models
{
    [Table("USUARIOS")]
    public class Usuario
    {
        [Key]
        [Column("id_empleado")]
        public int IdEmpleado { get; set; }

        public string Contrasenia { get; set; }
        public string Correo { get; set; }
        public Empleado EmpleadoNavigation { get; set; }
    }
}
