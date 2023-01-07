using System;
using System.Collections.Generic;

namespace pedidos_dap5.Models;

public partial class PedidosHistorial
{
    public int IdHistorial { get; set; }

    public int IdPedido { get; set; }

    public int Estado { get; set; }

    public DateTime Fecha { get; set; }

    public virtual Estado EstadoNavigation { get; set; } = null!;

    public virtual Pedido IdPedidoNavigation { get; set; } = null!;
}
