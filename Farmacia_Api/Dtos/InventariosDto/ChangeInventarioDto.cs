namespace Farmacia_Api.Dtos.InventariosDto
{
    public class ChangeInventarioDto
    {
        public int SucursalId { get; set; }
        public int InsumoId { get; set; }
        public int Cantidad { get; set; }
        public bool esMedicamento { get; set; }
    }
}
