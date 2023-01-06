using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TSK.Models.Entity
{
    public partial class Flotum
    {
        public Flotum()
        {
            Unidads = new HashSet<Unidad>();
            Pms = new HashSet<Pm>();
        }


        public int IdFlt { get; set; }
        public int IdTeq { get; set; }

        [Required(ErrorMessage = "El Nombre es obligatorio")]
        public string Flota { get; set; }
        public bool? Habilitado { get; set; }
        public string Extracolumn1 { get; set; }
        public string Extracolumn2 { get; set; }
        public string Extracolumn3 { get; set; }

        public virtual TipoEquipo IdTeqNavigation { get; set; }
        public virtual ICollection<Unidad> Unidads { get; set; }
        public virtual ICollection<Pm> Pms { get; set; }
    }
}
