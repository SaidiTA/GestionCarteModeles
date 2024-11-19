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
    public class CarteModelsController : Controller
    {
        private GestionCartesEntities2 db = new GestionCartesEntities2();
        public ActionResult calendrier()
        {
           // ViewBag.Lieu = new SelectList(db.Salles, "nom", "nom");
           // ViewBag.UtilisateursDisponibles = db.Utilisateurs.ToList();
            return View();
        }
        // GET: CarteModels
        public ActionResult Index()
        {
            return View(db.CarteModel.ToList());
        }
        // POST: CarteModels/Approve
        [HttpPost]
        public JsonResult Approve(int id)
        {
            var carte = db.CarteModel.Find(id);
            if (carte == null)
            {
                return Json(new { success = false });
            }

            carte.status_carte = "Valide";
            db.Entry(carte).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return Json(new { success = true });
        }
        // GET: CarteModels/Search
        [HttpGet]
        public JsonResult Search(string reference)
        {
            var results = db.CarteModel
                .Where(c => c.reference.Contains(reference))
                .Select(c => new
                {
                    c.id,
                    c.reference,
                    c.type_poste,
                    c.status,
                    c.status_carte,
                    c.date_creation,
                    c.date_fin
                })
                .ToList();

            return Json(results, JsonRequestBehavior.AllowGet);
        }
    

    // GET: CarteModels/Details/5
    public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarteModel carteModel = db.CarteModel.Find(id);
            if (carteModel == null)
            {
                return HttpNotFound();
            }
            return View(carteModel);
        }

        // GET: CarteModels/Create
        public ActionResult Create()
        {

            var model = new CarteModel
            {
                status_carte = "Invalide"  // Définir la valeur par défaut
            };
            return View(model);
           
        }

        // POST: CarteModels/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,reference,type_poste,status,status_carte,date_creation,date_fin")] CarteModel carteModel)
        {
            if (ModelState.IsValid)
            {
                db.CarteModel.Add(carteModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(carteModel);
        }

        // GET: CarteModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarteModel carteModel = db.CarteModel.Find(id);
            if (carteModel == null)
            {
                return HttpNotFound();
            }
            return View(carteModel);
        }

        // POST: CarteModels/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,reference,type_poste,status,status_carte,date_creation,date_fin")] CarteModel carteModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carteModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(carteModel);
        }

        // GET: CarteModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarteModel carteModel = db.CarteModel.Find(id);
            if (carteModel == null)
            {
                return HttpNotFound();
            }
            return View(carteModel);
        }

        public JsonResult GetCardStatusStats()
        {
            // Charger les cartes depuis la base de données
            var cartes = db.CarteModel.ToList();

            // Compter le nombre de cartes par statut
            var validCount = cartes.Count(c => c.status_carte == "Valide");
            var invalidCount = cartes.Count(c => c.status_carte == "Invalide");

            var result = new
            {
                labels = new[] { "Valide", "Invalide" },
                data = new[] { validCount, invalidCount }
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }



        // POST: CarteModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CarteModel carteModel = db.CarteModel.Find(id);
            db.CarteModel.Remove(carteModel);
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
