using Microsoft.AspNetCore.Mvc;

namespace pedidos_dap5.Services
{
    public class SoloUsuariosDiapMiddleware
    {
        private readonly RequestDelegate _next;

        public SoloUsuariosDiapMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //var nombreUsuario = context.User.Identity.Name;
            var nombreUsuario = Environment.UserName.ToString().ToUpper();
            if (!nombreUsuario.Contains("USER"))
            {
                context.Response.StatusCode = 403;
                //  context.Response.Redirect("/Home/Privacy");
                await context.Response.WriteAsync("<h1>Acceso denegado</h1><p>No tiene permiso para acceder a esta página.</p>");
                return; 
            }

            await _next(context);
        }
    }
}
