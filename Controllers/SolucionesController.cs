using Microsoft.AspNetCore.Mvc;
using pedidos_dap5.Models;
using pedidos_dap5.Models.ViewModels;

namespace pedidos_dap5.Controllers
{
    public class SolucionesController : Controller
    {
        PedidosContext db = new PedidosContext();
        public IActionResult Details (int id)
        {
            ViewModelSoluciones sol;
            sol = (from p in db.Pedidos
                   join u in db.Usuarios on p.Usuario equals u.Idusuaio
                   join t in db.Tecnicos on p.Tecnico equals t.Idtecnico
                   join e in db.Estados on p.Estado equals e.Idestado
                   join s in db.Soluciones on p.Idpedido equals s.Idpedido
                   where p.Idpedido == id
                   select new ViewModelSoluciones
                   {
                       IdPedido = p.Idpedido,
                       Pedido = p.Descripcion.ToString(),
                       UsuarioDescripcion = u.Usuario1.ToString(),
                       TecnicoDescricpcion = t.Tecnico1.ToString(),
                       EstadoDescripcion = e.Descripcion.ToString(),
                       Solucion = s.Solucion.ToString(),
                       FechaInicio = p.FechaInicio.ToShortDateString(),
                       FechaSolucion = p.FechaInicio.ToShortDateString()
                   }).FirstOrDefault();

            return PartialView(sol);
        }
    }
}
