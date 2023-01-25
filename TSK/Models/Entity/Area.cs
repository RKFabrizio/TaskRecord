using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TSK.Models.Entity
{
    public partial class Area
    {
        public Area()
        {
            TipoEquipos = new HashSet<TipoEquipo>();
        }

        public int IdArea { get; set; }

        [Required(ErrorMessage = "El Nombre es obligatorio")]
        public string Nombre { get; set; }
        public bool? Habilitado { get; set; }
        public string Extracolumn1 { get; set; }
        public string Extracolumn2 { get; set; }
        public string Extracolumn3 { get; set; }

        public virtual ICollection<TipoEquipo> TipoEquipos { get; set; }
    }
}
