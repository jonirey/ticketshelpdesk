using System;
using System.Collections.Generic;

namespace pedidos_dap5.Models;

public partial class Usuario
{
    public int Idusuaio { get; set; }

    public string Usuario1 { get; set; } = null!;

    public int? Grado { get; set; }

    public string Descripcion { get; set; } = null!;

    public string? Interno { get; set; }

    public string? TelParticular { get; set; }

    public virtual Grado? GradoNavigation { get; set; }

    public virtual ICollection<Pedido> Pedidos { get; } = new List<Pedido>();
}
