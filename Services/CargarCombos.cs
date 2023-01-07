using Microsoft.AspNetCore.Mvc.Rendering;
using pedidos_dap5.Models;

namespace pedidos_dap5.Services
{
    public class CargarCombos
    {
        PedidosContext db = new PedidosContext();
        public List<SelectListItem> Listatecnicos(int selectedValue)
        {
            List<Tecnico> tecnicos = (from t in db.Tecnicos
                                      select t).ToList();
            List<SelectListItem> items = tecnicos.ConvertAll(t =>
            {
                var item = new SelectListItem()
                {
                    Text = t.Tecnico1,
                    Value = t.Idtecnico.ToString(),
                };

                if (t.Idtecnico == selectedValue)
                {
                    item.Selected = true;
                }

                return item;
            });
            return items;
        }

        public List<SelectListItem> Listaestados(int estado)
        {

            List<Estado> estados = (from e in db.Estados
                                    select e).ToList();
            List<SelectListItem> items = estados.ConvertAll(e =>
            {

                var item = new SelectListItem()
                {
                    Text = e.Descripcion,
                    Value = e.Idestado.ToString(),
                };

                if (e.Idestado == estado)
                {
                    item.Selected = true;
                }

                return item;


            });
            return items;
        }

        public List<SelectListItem> Listausuarios(int user)
        {

            List<Usuario> usuarios = (from u in db.Usuarios
                                      select u).ToList();
            List<SelectListItem> items = usuarios.ConvertAll(u =>
            {

                var item = new SelectListItem()
                {
                    Text = u.Usuario1,
                    Value = u.Idusuaio.ToString(),
                };

                if (u.Idusuaio == user)
                {
                    item.Selected = true;
                }

                return item;


            });
            return items;
        }

    }
}
