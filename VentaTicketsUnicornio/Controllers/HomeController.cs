using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using VentaTicketsUnicornio.Models;

namespace VentaTicketsUnicornio.Controllers
{

    public class HomeController : Controller
    {
        private TicketDBContext db = new TicketDBContext();

        public ActionResult Index()
        {
            var ventas = db.Ventas.Include(v => v.Catalogos).Include(v => v.Empleados);

            ViewBag.VentasTotales = 0;
            ViewBag.GranTotal = 0;
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