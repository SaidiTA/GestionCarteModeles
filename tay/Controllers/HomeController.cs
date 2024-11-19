using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;

namespace tay.Controllers
{
    public class HomeController : Controller
    {
    
        public ActionResult Index()
        {

            var model = new DashboardViewModel
            {
                NombreCartes = db.CarteModel.Count(),
                NombreEmployees = db.Employe.Count(),
                NombreDepartements = db.Departement.Count(),
                CarteValide = db.CarteModel.Count(c => c.status_carte == "valide"),   
                CarteInvalide = db.CarteModel.Count(c => c.status_carte == "invalide")

            };

            return View(model);

        }
        private GestionCartesEntities2 db = new GestionCartesEntities2();
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}