using System;
using System.Collections.Generic;

namespace pedidos_dap5.Models;

public partial class Grado
{
    public int IdGrado { get; set; }

    public string Grado1 { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; } = new List<Usuario>();
}
