using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TSK.Models.Entity
{
    public partial class ActSub
    {
        [Required(ErrorMessage = "La Actividad es obligatoria")]
        public int IdAct { get; set; }

        [Required(ErrorMessage = "La SubActividad es obligatoria")]
        public int IdSubAct { get; set; }

        public bool? Habilitado { get; set; }
        public string Extracolumn1 { get; set; }
        public string Extracolumn2 { get; set; }
        public string Extracolumn3 { get; set; }
        public virtual Actividad IdActNavigation { get; set; }
        public virtual SubActividad IdSubActNavigation { get; set; }
    }
}
