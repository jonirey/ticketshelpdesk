using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using pedidos_dap5.Models;
using pedidos_dap5.Services;

namespace pedidos_dap5.Controllers
{
    public class UserController : Controller
    {
        PedidosContext db = new PedidosContext();
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            //acá relleno el desplegable de grados
            List<Grado> grado = null;
            grado = (from g in db.Grados
                     select g).ToList();
            List<SelectListItem> items = grado.ConvertAll(g =>
            {

                return new SelectListItem()
                {
                    Text = g.Grado1.ToString(),
                    Value = g.IdGrado.ToString(),
                    Selected = false
                };
            }

            );
            ViewBag.Grado = items;
            Usuario oUsuario = new Usuario();
            TraerPersonas personas = new();
            int tec = 0;
            tec = personas.existetecnico();
            if (tec == 0) { 
            ViewBag.Usuario = Environment.UserName.ToString();
            }
            return PartialView(oUsuario);
        }

        [HttpPost]
        public IActionResult Create(Usuario oUsuario)
        {
            TraerPersonas traer = new TraerPersonas();
            int tecnico = 0;
            tecnico = traer.existetecnico();
            if (oUsuario != null)
            {
                if (tecnico == 0) 
                { 
                oUsuario.Usuario1 = Environment.UserName.ToString();
                }
                
                
                db.Usuarios.Add(oUsuario);
                db.SaveChanges();
                return RedirectToAction("Index", "Pedidos");
            }
            else
            {
                return RedirectToAction("Error","Home");
            }
            
        }
    }
}
