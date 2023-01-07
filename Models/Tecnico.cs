using System;
using System.Collections.Generic;

namespace pedidos_dap5.Models;

public partial class Tecnico
{
    public int Idtecnico { get; set; }

    public string Tecnico1 { get; set; } = null!;

    public string CuentaRina { get; set; } = null!;

    public string? Interno { get; set; }

    public virtual ICollection<Pedido> Pedidos { get; } = new List<Pedido>();

    public virtual ICollection<Solucione> Soluciones { get; } = new List<Solucione>();
}
