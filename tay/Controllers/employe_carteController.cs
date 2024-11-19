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
    public class employe_carteController : Controller
    {
        private GestionCartesEntities2 db = new GestionCartesEntities2();

        // GET: employe_carte
        public ActionResult Index()
        {
            var employe_carte = db.employe_carte.Include(e => e.CarteModel).Include(e => e.Employe);
            return View(employe_carte.ToList());
        }

        // GET: employe_carte/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            employe_carte employe_carte = db.employe_carte.Find(id);
            if (employe_carte == null)
            {
                return HttpNotFound();
            }
            return View(employe_carte);
        }

        // GET: employe_carte/Create
        public ActionResult Create()
        {
            ViewBag.idCarte = new SelectList(db.CarteModel, "id", "reference");
            ViewBag.idEmp = new SelectList(db.Employe, "id", "Nom_Prenom");
            return View();
        }

        // POST: employe_carte/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,idEmp,idCarte")] employe_carte employe_carte)
        {
            if (ModelState.IsValid)
            {
                db.employe_carte.Add(employe_carte);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idCarte = new SelectList(db.CarteModel, "id", "reference", employe_carte.idCarte);
            ViewBag.idEmp = new SelectList(db.Employe, "id", "Nom_Prenom", employe_carte.idEmp);
            return View(employe_carte);
        }

        // GET: employe_carte/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            employe_carte employe_carte = db.employe_carte.Find(id);
            if (employe_carte == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCarte = new SelectList(db.CarteModel, "id", "reference", employe_carte.idCarte);
            ViewBag.idEmp = new SelectList(db.Employe, "id", "Nom_Prenom", employe_carte.idEmp);
            return View(employe_carte);
        }

        // POST: employe_carte/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,idEmp,idCarte")] employe_carte employe_carte)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employe_carte).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idCarte = new SelectList(db.CarteModel, "id", "reference", employe_carte.idCarte);
            ViewBag.idEmp = new SelectList(db.Employe, "id", "Nom_Prenom", employe_carte.idEmp);
            return View(employe_carte);
        }

        // GET: employe_carte/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            employe_carte employe_carte = db.employe_carte.Find(id);
            if (employe_carte == null)
            {
                return HttpNotFound();
            }
            return View(employe_carte);
        }

        // POST: employe_carte/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            employe_carte employe_carte = db.employe_carte.Find(id);
            db.employe_carte.Remove(employe_carte);
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
