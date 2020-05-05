using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using VentaTicketsUnicornio.Models;

namespace VentaTicketsUnicornio.Controllers
{

    public class HomeController : Controller
    {
        private TicketDBContext db = new TicketDBContext();

        public ActionResult Index()
        {
            var ventas = db.Ventas.Include(v => v.Catalogos);

            ViewBag.VentasTotales = 0;
            ViewBag.GranTotal = 0;

            var empleado = User.Identity.Name;
            if (empleado.Length < 1)
            {
                empleado = "Anonimo";
            }
            ViewBag.Usuario = empleado;

            try
            {
                ViewBag.VentasTotales = ventas.LongCount<Venta>();
                ViewBag.GranTotal = ventas.Sum<Venta>(x => x.Cobrado);
            }
            catch (Exception) { }

            return View();
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Programacion, soporte y web.";

            return View();
        }
        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Programacion, soporte y web.";

            return View();
        }
    }
}