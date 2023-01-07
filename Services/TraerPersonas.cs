using pedidos_dap5.Models;

namespace pedidos_dap5.Services

{
    public class TraerPersonas
    {

        PedidosContext db = new PedidosContext();
        public int existetecnico()
        {
            int registrado = 0;
            //string user = User.Identity.Name.ToString();
            string user = Environment.UserName.ToString();
            var existe = db.Tecnicos.FirstOrDefault(u => u.CuentaRina == user);

            if (existe != null)
            {
                registrado = existe.Idtecnico;
            }

            return registrado;
        }

        public int existeusuario()
        {
            int registrado = 0;
            //string user = User.Identity.Name.ToString();
            string user = Environment.UserName.ToString();
            var existe = db.Usuarios.FirstOrDefault(u => u.Usuario1 == user);

            if (existe != null)
            {
                registrado = existe.Idusuaio;
            }

            return registrado;
        }

        public string Name()
        {
           
            string user = Environment.UserName.ToString();
            var usuario = (from u in db.Usuarios
                           join g in db.Grados on u.Grado equals g.IdGrado
                           where u.Usuario1 == user
                           select new 
                           {
                               gr = g.Grado1,
                               Descripcion = u.Descripcion
                           }).FirstOrDefault();

            if (usuario != null)
            {
                user = usuario.gr + " " + usuario.Descripcion; 
            }
            else
            {
                user = "Nuevo usuario";
            }
            return user;
        }
    }
}
