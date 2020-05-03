using System;
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
        [Authorize]
        public async Task<ActionResult> Index()
        {
            var ventas = db.Ventas.Include(v => v.Catalogos).Include(v => v.Empleados);

            ViewBag.VentasTotales = 0;
            ViewBag.GranTotal = 0;
            try
            {
                ViewBag.VentasTotales = ventas.LongCount<Venta>();
                ViewBag.GranTotal = ventas.Sum<Venta>(x => x.Cobrado);
            }
            catch (Exception){}

            return View(await ventas.ToListAsync());
        }

        // GET: Ventas/Details/5
        [Authorize]
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
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.IdCatalogo = new SelectList(db.Catalogos, "IdCatalogo", "Nombre");
            ViewBag.IdEmpleado = new SelectList(db.Empleados, "IdEmpleado", "Usuario");
            return View();
        }

        // POST: Ventas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Create([Bind(Include = "IdVenta,Fecha,Asientos,IdCatalogo,EnEfectivo,Cobrado,IdEmpleado")] Venta venta)
        {
            if (ModelState.IsValid)
            {
                db.Ventas.Add(venta);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdCatalogo = new SelectList(db.Catalogos, "IdCatalogo", "Nombre", venta.IdCatalogo);
            ViewBag.IdEmpleado = new SelectList(db.Empleados, "IdEmpleado", "Usuario", venta.IdEmpleado);
            return View(venta);
        }

        // GET: Ventas/Edit/5
        [Authorize]
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
            ViewBag.IdEmpleado = new SelectList(db.Empleados, "IdEmpleado", "Usuario", venta.IdEmpleado);
            return View(venta);
        }

        // POST: Ventas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Edit([Bind(Include = "IdVenta,Fecha,Asientos,IdCatalogo,EnEfectivo,Cobrado,IdEmpleado")] Venta venta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(venta).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdCatalogo = new SelectList(db.Catalogos, "IdCatalogo", "Nombre", venta.IdCatalogo);
            ViewBag.IdEmpleado = new SelectList(db.Empleados, "IdEmpleado", "Usuario", venta.IdEmpleado);
            return View(venta);
        }

        // GET: Ventas/Delete/5
        [Authorize]
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
        [Authorize]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Venta venta = await db.Ventas.FindAsync(id);
            db.Ventas.Remove(venta);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
