using System.ComponentModel.DataAnnotations;

namespace pedidos_dap5.Models.ViewModels
{
    public class ViewModelHistorialPedido
    {
        [Key]
        public int IdHistorial { get; set; }
        public int IdPedido { get; set; }
        public string Estado_descripcion { get; set; }
        public string Fecha { get; set; }

    }
}
