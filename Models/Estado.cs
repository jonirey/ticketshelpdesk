using System;
using System.Collections.Generic;

namespace pedidos_dap5.Models;

public partial class Estado
{
    public int Idestado { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Pedido> Pedidos { get; } = new List<Pedido>();

    public virtual ICollection<PedidosHistorial> PedidosHistorials { get; } = new List<PedidosHistorial>();
}
