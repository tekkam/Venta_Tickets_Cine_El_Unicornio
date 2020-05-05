using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using VentaTicketsUnicornio.Models;

namespace VentaTicketsUnicornio.Controllers
{
    public class VentasController : Controller
    {
        private TicketDBContext db = new TicketDBContext();

        // GET: Ventas
        public async Task<ActionResult> Index()
        {
            var ventas = db.Ventas.Include(v => v.Catalogos);
            ViewBag.VentasTotales = ventas.LongCount<Venta>();
            ViewBag.GranTotal = ventas.Sum<Venta>(x => x.Cobrado);
            return View(await ventas.ToListAsync());
        }

        // GET: Ventas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venta venta = await db.Ventas.FindAsync(id);
            if (venta == null)
            {
                return HttpNotFound();
            }
            return View(venta);
        }

        // GET: Ventas/Create
        public ActionResult Create()
        {
            ViewBag.IdCatalogo = new SelectList(db.Catalogos, "IdCatalogo", "Nombre");
            return View();
        }

        // POST: Ventas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdVenta,Fecha,IdCatalogo,Asientos,TipoPago,Cobrado,Empleado")] Venta venta)
        {
            venta.Fecha = DateTime.Now.Date;
            var empleado = User.Identity.Name;
            if (empleado.Length < 1)
            {
                empleado = "Anonimo";
            }
            venta.Empleado = empleado;

            try
            {
                Catalogo catal = await db.Catalogos.FindAsync(venta.IdCatalogo);
                decimal precio = catal.Precio;
                decimal asientos = (decimal)venta.Asientos;
                var cobrado = precio * asientos;
                if (venta.Asientos > 0)
                {
                    if (precio > 0)
                    {
                        venta.Cobrado = ((double)cobrado);
                    }

                }
            }
            catch (Exception) { }

            int limite;
            if (venta.IdVenta<=0)
            {
                limite = 100;
            }
            else
            {
                limite = db.Catalogos.Distinct().Where(o => o.IdCatalogo.Equals(venta.IdCatalogo)).Select(o => o.Asientos).SingleOrDefault();
            }

            int tomados = 0;
            try
            {
                tomados = db.Ventas.Where(y => y.IdCatalogo.Equals(venta.IdCatalogo)).Select(u => u.Asientos).Sum();
            }
            catch (Exception) { }

            var reservado = venta.Asientos + tomados;

            if (reservado < limite)
            {
                if (ModelState.IsValid)
                {
                    db.Ventas.Add(venta);
                    await db.SaveChangesAsync();
                    var retorno = "Details/" + venta.IdVenta;
                    return RedirectToAction(retorno);
                }
            }
            else { return RedirectToAction("LimiteAsientos"); }

            ViewBag.IdCatalogo = new SelectList(db.Catalogos, "IdCatalogo", "Nombre", venta.IdCatalogo);
            return View(venta);
        }

        // GET: Ventas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venta venta = await db.Ventas.FindAsync(id);
            if (venta == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCatalogo = new SelectList(db.Catalogos, "IdCatalogo", "Nombre", venta.IdCatalogo);
            return View(venta);
        }

        // POST: Ventas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdVenta,Fecha,IdCatalogo,Asientos,procede,TipoPago,Cobrado,Empleado")] Venta venta)
        {
            venta.Fecha = DateTime.Now.Date;
            var empleado = User.Identity.Name;
            if (empleado.Length < 1)
            {
                empleado = "Anonimo";
            }
            venta.Empleado = empleado;

            try
            {
                Catalogo catal = await db.Catalogos.FindAsync(venta.IdCatalogo);
                decimal precio = catal.Precio;
                decimal asientos = (decimal)venta.Asientos;
                var cobrado = precio * asientos;
                if (venta.Asientos > 0)
                {
                    if (precio > 0)
                    {
                        venta.Cobrado = ((double)cobrado);
                    }

                }
            }
            catch (Exception) { }
            if (ModelState.IsValid)
            {
                db.Entry(venta).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdCatalogo = new SelectList(db.Catalogos, "IdCatalogo", "Nombre", venta.IdCatalogo);
            return View(venta);
        }

        // GET: Ventas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venta venta = await db.Ventas.FindAsync(id);
            if (venta == null)
            {
                return HttpNotFound();
            }
            return View(venta);
        }

        // POST: Ventas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Venta venta = await db.Ventas.FindAsync(id);
            db.Ventas.Remove(venta);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public ActionResult LimiteAsientos()
        {
            ViewBag.Message = "Se alcanzo el limite de asientos para el estreno solicitado";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
