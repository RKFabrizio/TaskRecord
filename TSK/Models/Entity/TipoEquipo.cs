using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TSK.Models.Entity
{
    public partial class TipoEquipo
    {
        public TipoEquipo()
        {
            Flota = new HashSet<Flotum>();
        }

        public int IdTeq { get; set; }

        [Required(ErrorMessage = "La Empresa es obligatoria")]
        public int IdArea { get; set; }

        [Required(ErrorMessage = "El Tipo de Equipo es obligatorio")]
        public string TEquipo { get; set; }
        public bool? Habilitado { get; set; }
        public string Extracolumn1 { get; set; }
        public string Extracolumn2 { get; set; }
        public string Extracolumn3 { get; set; }

        public virtual Area IdAreaNavigation { get; set; }
        public virtual ICollection<Flotum> Flota { get; set; }
    }
}
