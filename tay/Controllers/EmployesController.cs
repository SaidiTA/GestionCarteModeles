using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using tay.Models;

namespace tay.Controllers
{
    public class EmployesController : Controller
    {
        private GestionCartesEntities2 db = new GestionCartesEntities2();

        // GET: Employes
        public ActionResult Index()
        {
            var employe = db.Employe.Include(e => e.Departement1).Include(e => e.Role1);
            return View(employe.ToList());
        }

        // GET: Employes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employe employe = db.Employe.Find(id);
            if (employe == null)
            {
                return HttpNotFound();
            }
            return View(employe);
        }

        // GET: Employes/Create
        public ActionResult Create()
        {
            ViewBag.departement = new SelectList(db.Departement, "id", "name");
            ViewBag.role = new SelectList(db.Role, "id", "nom");
            return View();
        }

        // POST: Employes/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Nom_Prenom,matricule_Emp,matricule_W,Email,Responsable,role,departement")] Employe employe)
        {
            if (ModelState.IsValid)
            {
                db.Employe.Add(employe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.departement = new SelectList(db.Departement, "id", "name", employe.departement);
            ViewBag.role = new SelectList(db.Role, "id", "nom", employe.role);
            return View(employe);
        }

        // GET: Employes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employe employe = db.Employe.Find(id);
            if (employe == null)
            {
                return HttpNotFound();
            }
            ViewBag.departement = new SelectList(db.Departement, "id", "name", employe.departement);
            ViewBag.role = new SelectList(db.Role, "id", "nom", employe.role);
            return View(employe);
        }

        // POST: Employes/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Nom_Prenom,matricule_Emp,matricule_W,Email,Responsable,role,departement")] Employe employe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.departement = new SelectList(db.Departement, "id", "name", employe.departement);
            ViewBag.role = new SelectList(db.Role, "id", "nom", employe.role);
            return View(employe);
        }

        // GET: Employes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employe employe = db.Employe.Find(id);
            if (employe == null)
            {
                return HttpNotFound();
            }
            return View(employe);
        }

        // POST: Employes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employe employe = db.Employe.Find(id);
            db.Employe.Remove(employe);
            db.SaveChanges();
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
