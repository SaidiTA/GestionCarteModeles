using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tay.Models
{
    public class DashboardViewModel
    {
        public int NombreCartes { get; set; }
        public int NombreEmployees { get; set; }
        public int NombreDepartements { get; set; }
        public int CarteValide { get; set; }
        public int CarteInvalide { get; set; }
    }
}