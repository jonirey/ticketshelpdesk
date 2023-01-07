using System.ComponentModel.DataAnnotations;

namespace pedidos_dap5.Models.ViewModels

{
    public class ViewModelSoluciones 
    {
        [Key]
        public int IdSolucion { get; set; }
        public int IdPedido { get; set; }
        public string Pedido { get; set; }
        public string Solucion { get; set; }
         public string UsuarioDescripcion { get; set; }
        public string TecnicoDescricpcion { get; set; }
        public string EstadoDescripcion { get; set; }
        public string FechaInicio { get; set; }
        public string FechaSolucion { get; set; }
    }
}
