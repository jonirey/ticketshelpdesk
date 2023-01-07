using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pedidos_dap5.Models;

public partial class Pedido
{
    public int Idpedido { get; set; }
    [StringLength(1000, ErrorMessage = "La longitud del pedido debe ser menor o igual a 1000 caracteres.")]
    public string Descripcion { get; set; } = null!;

    public int Usuario { get; set; }

    public int? Tecnico { get; set; }

    public int? Estado { get; set; }
    
    public DateTime FechaInicio { get; set; }

    public virtual Estado? EstadoNavigation { get; set; }

    public virtual ICollection<PedidosHistorial> PedidosHistorials { get; } = new List<PedidosHistorial>();

    public virtual ICollection<Solucione> Soluciones { get; } = new List<Solucione>();

    public virtual Tecnico? TecnicoNavigation { get; set; }

    public virtual Usuario UsuarioNavigation { get; set; } = null!;
}
