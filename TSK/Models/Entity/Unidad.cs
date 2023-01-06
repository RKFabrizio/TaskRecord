using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TSK.Models.Entity
{
    public partial class Unidad
    {
        public Unidad()
        {
            Reportes = new HashSet<Reporte>();
        }

        public int IdUni { get; set; }
        public int IdFlt { get; set; }

        [Required(ErrorMessage = "La Unidad es obligatorio")]
        public string Unidad1 { get; set; }
        public bool? Habilitado { get; set; }
        public string Extracolumn1 { get; set; }
        public string Extracolumn2 { get; set; }
        public string Extracolumn3 { get; set; }

        public virtual Flotum IdFltNavigation { get; set; }
        public virtual ICollection<Reporte> Reportes { get; set; }
    }
}
