using System.ComponentModel.DataAnnotations;

namespace pedidos_dap5.Models.ViewModels
{
    public class ViewModelPedido
    {
        [Key]
        public int Idpedido { get; set; }
        public int? Estado { get; set; }
        public string Descripcion_pedido { get; set; } = null!;

        public string Usuario1 { get; set; } = null!;

        public string Tecnico1 { get; set; } = null!;

        public string? Interno { get; set; }
        public string Descripcion_estado { get; set; } = null!;

        public String FechaInicio { get; set; }




    }
}
