//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace tay.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class reunion_employe
    {
        public int id { get; set; }
        public string ReunionTitre { get; set; }
        public int EmployeId { get; set; }
    
        public virtual Employe Employe { get; set; }
        public virtual Reunions Reunions { get; set; }
    }
}
