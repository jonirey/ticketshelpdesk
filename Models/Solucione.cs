using System;
using System.Collections.Generic;

namespace pedidos_dap5.Models;

public partial class Solucione
{
    public int Idsolucion { get; set; }

    public int Idpedido { get; set; }

    public string Solucion { get; set; } = null!;

    public DateTime? FechaSolucion { get; set; }

    public int? Tecnico { get; set; }

    public virtual Pedido IdpedidoNavigation { get; set; } = null!;

    public virtual Tecnico? TecnicoNavigation { get; set; }
}
