using Microsoft.AspNetCore.Mvc;
using pedidos_dap5.Models;
using pedidos_dap5.Models.ViewModels;
using pedidos_dap5.Services;


namespace pedidos_dap5.Controllers
{
    public class PedidosController : Controller
    {
        PedidosContext db = new PedidosContext();

    

        
        public IActionResult Index(int? id)
        {
            //busca al usuario
            TraerPersonas personas = new TraerPersonas();
            int esta = personas.existeusuario();
            int tec = personas.existetecnico();
            //si  esta le muestra sus pedidos
            
            if (esta != 0 || tec!=0)
            {

                var lista = (from p in db.Pedidos
                            join u in db.Usuarios on p.Usuario equals u.Idusuaio
                           join t in db.Tecnicos on p.Tecnico equals t.Idtecnico
                             join e in db.Estados on p.Estado equals e.Idestado
                             where p.Usuario == esta
                             orderby p.Idpedido descending 
                                 select new ViewModelPedido
                              {
                                  Idpedido = p.Idpedido,
                                  Descripcion_pedido = p.Descripcion.ToString(),
                                  Usuario1 = u.Usuario1.ToString(),
                                  Tecnico1 = t.Tecnico1.ToString(),
                                  Interno = t.Interno.ToString(),
                                  Descripcion_estado = e.Descripcion.ToString(),
                                  FechaInicio = p.FechaInicio.ToShortDateString()
                              });
                           
                if (id == null) { 
                lista.ToList();
                    return View(lista);
                }
                else
                {
                   ViewModelPedido pedido = lista.FirstOrDefault(i=> i.Idpedido ==id);
                    return PartialView("Details", pedido);
                }




                
            }

            //sino lo envia a que se registre
            else
            {
                return RedirectToAction("Redirect", "Pedidos");

            }
        }

        public List<ViewModelHistorialPedido> Detalles (int Id)
        {
            var detalles = (from h in db.PedidosHistorials
                            join p in db.Pedidos on h.IdPedido equals p.Idpedido
                            join e in db.Estados on h.Estado equals e.Idestado
                            where p.Idpedido == Id
                            orderby h.IdHistorial descending
                            select new ViewModelHistorialPedido
                            {
                                IdPedido = p.Idpedido,
                                Estado_descripcion = e.Descripcion.ToString(),
                                Fecha = h.Fecha.ToShortDateString()
                            }
                             );
           // detalles.ToList();

            return detalles.ToList();
        }

        [HttpGet]
        public IActionResult Create() 
        {
            Pedido pedido = new Pedido();

            TraerPersonas personas = new TraerPersonas();
            int esta = personas.existeusuario();
            int tec = personas.existetecnico();

            CargarCombos cargar = new CargarCombos();
            if (esta != 0 || tec !=0)
            {
                ViewBag.Usuario = cargar.Listausuarios(0);
                ViewBag.Tecnico = cargar.Listatecnicos(0);
                ViewBag.Estado = cargar.Listaestados(0).Take(3);
                return PartialView();
            }

            else 
            { 
                return RedirectToAction("Redirect", "Pedidos");
            }
           
        }
        [HttpPost]
        public IActionResult Create(Pedido oPedido) 
        {
            TraerPersonas personas = new TraerPersonas();
            int esta = 0;
             esta = personas.existeusuario();
            int esttecnico = 0;
             esttecnico = personas.existetecnico();
            int tec = 0;
            tec = db.Tecnicos.FirstOrDefault(u => u.Tecnico1.ToUpper() == "SIN TECNICO").Idtecnico;
            int est = 0;
            est = db.Estados.FirstOrDefault(u => u.Descripcion.ToUpper() == "ABIERTO").Idestado;

            if (esta != 0 && esttecnico == 0)
            {
                oPedido.Usuario = esta;
                oPedido.Tecnico = tec;
                oPedido.Estado = est;
            }
            oPedido.FechaInicio = DateTime.Now;
            db.Pedidos.Add(oPedido);
            db.SaveChanges();
             if (esta != 0) { 
            return RedirectToAction("Index", "Pedidos");
            }
            else
                {
                return RedirectToAction("Index", "Tecnicos");

            }

        }

        public IActionResult Redirect()
        {
            return View();
        }      

        public IActionResult Details()
        {
            return PartialView();
        }

    }
}
