using Microsoft.AspNetCore.Mvc;
using pedidos_dap5.Models.ViewModels;
using pedidos_dap5.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using pedidos_dap5.Services;
using System.Collections.Generic;

namespace pedidos_dap5.Controllers
{
    
    public class TecnicosController : Controller
    {
        PedidosContext db = new PedidosContext();
        CargarCombos cargar = new CargarCombos();
        public IActionResult Index()
        {
            TraerPersonas tec = new TraerPersonas();
            int  tecnico = tec.existetecnico();

            if (tecnico != 0) { 
                var lista = (from p in db.Pedidos
                             join u in db.Usuarios on p.Usuario equals u.Idusuaio
                             join t in db.Tecnicos on p.Tecnico equals t.Idtecnico
                             join e in db.Estados on p.Estado equals e.Idestado
                            where p.Estado == 1
                             select new ViewModelPedido
                             {
                                 Idpedido = p.Idpedido,
                                 Descripcion_pedido = p.Descripcion.ToString(),
                                 Usuario1 = u.Usuario1.ToString(),
                                 Tecnico1 = t.Tecnico1.ToString(),
                                 Descripcion_estado = e.Descripcion.ToString(),
                                 FechaInicio = p.FechaInicio.ToShortDateString()
                             });

                lista.ToList();
                return View(lista);
            }
            else { return NotFound(); }
        }

        [HttpGet]
        public IActionResult Take(int id)
        {
            var oPedido = db.Pedidos.Find(id);
            CargarCombos cargar = new CargarCombos();
            ViewBag.Tecnico = cargar.Listatecnicos((int)oPedido.Tecnico);
             ViewBag.Estado = cargar.Listaestados((int)oPedido.Estado);
            ViewBag.Usuario = cargar.Listausuarios((int)oPedido.Usuario);
            if (oPedido == null)
            {
                return NotFound();
            }
            return View (oPedido);
        }

        [HttpPost]
        public IActionResult Take (int id, Pedido pedido)
        {
            Pedido mPedido = new Pedido();
          mPedido = db.Pedidos.Find(id);
            mPedido.Tecnico = pedido.Tecnico;
            mPedido.Estado = 2;
            
            db.Entry(mPedido).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index","Tecnicos");

        }

        [HttpGet]
        public IActionResult PedidosXTecnico()
        {
            TraerPersonas personas = new TraerPersonas();
            int idTecnico = personas.existetecnico();
            ViewBag.Estado = cargar.Listaestados(0);
            List<ViewModelPedido> lista = (from p in db.Pedidos 
                                           join u in db.Usuarios on p.Usuario equals u.Idusuaio
                                           join t in db.Tecnicos on p.Tecnico equals t.Idtecnico
                                           join e in db.Estados on p.Estado equals e.Idestado
                                           where   p.Tecnico == idTecnico
                                           orderby p.Idpedido descending
                                           select new ViewModelPedido
                                           {
                                               Idpedido = p.Idpedido,
                                               Estado = p.Estado,
                                               Descripcion_pedido = p.Descripcion.ToString(),
                                               Usuario1 = u.Usuario1.ToString(),
                                               Tecnico1 = t.Tecnico1.ToString(),
                                               Descripcion_estado = e.Descripcion.ToString(),
                                               FechaInicio = p.FechaInicio.ToShortDateString()
                                           }).ToList();
            

            return View(lista);

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var oPedido = db.Pedidos.Find(id);
            ViewBag.Tecnico = db.Tecnicos.FirstOrDefault(t => t.Idtecnico == oPedido.Tecnico).Tecnico1.ToString(); //Listatecnicos((int)oPedido.Tecnico);
           
            ViewBag.Estado = cargar.Listaestados((int)oPedido.Estado);
            ViewBag.Usuario = db.Usuarios.FirstOrDefault(u => u.Idusuaio == oPedido.Usuario).Usuario1.ToString();
            if (oPedido == null)
            {
                return NotFound();
            }
            return PartialView(oPedido);
        }

        [HttpPost]
        public IActionResult Edit (int id, Pedido oPedido, string textSolucion)
        {
            Pedido mPedido;
            mPedido = db.Pedidos.Find(id);
            mPedido.Estado = oPedido.Estado;

          
            db.Entry(mPedido).State = EntityState.Modified;
            db.SaveChanges();
            if (mPedido.Estado <= 3)
            {
                return RedirectToAction("PedidosXTecnico", "Tecnicos");
            }

            else
            {
                Solucione oSolucion = new Solucione();
                oSolucion.Idpedido = mPedido.Idpedido;
                oSolucion.FechaSolucion = DateTime.Now;
                oSolucion.Tecnico = mPedido.Tecnico;
                oSolucion.Solucion = textSolucion;
                db.Soluciones.Add(oSolucion);
                db.SaveChanges();
                return RedirectToAction("PedidosXTecnico", "Tecnicos");
            }
        }
    }
}
