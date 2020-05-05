using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VentaTicketsUnicornio.Models;

namespace VentaTicketsUnicornio.Controllers
{
    public class CatalogosController : Controller
    {
        private TicketDBContext db = new TicketDBContext();

        // GET: Catalogos
        [Authorize]
        public async Task<ActionResult> Index()
        {
            return View(await db.Catalogos.ToListAsync());
        }

        // GET: Catalogos/Details/5
        [Authorize]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Catalogo catalogo = await db.Catalogos.FindAsync(id);
            if (catalogo == null)
            {
                return HttpNotFound();
            }
            return View(catalogo);
        }

        // GET: Catalogos/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Catalogos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Create([Bind(Include = "IdCatalogo,Nombre,Genero,Precio,HoraInicio,HoraFin,Asientos")] Catalogo catalogo)
        {
            if (ModelState.IsValid)
            {
                db.Catalogos.Add(catalogo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(catalogo);
        }

        // GET: Catalogos/Edit/5
        [Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Catalogo catalogo = await db.Catalogos.FindAsync(id);
            if (catalogo == null)
            {
                return HttpNotFound();
            }
            return View(catalogo);
        }

        // POST: Catalogos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Edit([Bind(Include = "IdCatalogo,Nombre,Genero,Precio,HoraInicio,HoraFin,Asientos")] Catalogo catalogo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(catalogo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(catalogo);
        }

        // GET: Catalogos/Delete/5
        [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Catalogo catalogo = await db.Catalogos.FindAsync(id);
            if (catalogo == null)
            {
                return HttpNotFound();
            }
            return View(catalogo);
        }

        // POST: Catalogos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Catalogo catalogo = await db.Catalogos.FindAsync(id);
            db.Catalogos.Remove(catalogo);
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
