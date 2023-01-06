using System;
using System.Collections.Generic;

namespace TSK.Models.EF
{
    public partial class Flotum
    {
        public Flotum()
        {
            Pms = new HashSet<Pm>();
            Unidads = new HashSet<Unidad>();
        }

        public int IdFlt { get; set; }
        public int IdTeq { get; set; }
        public string Flota { get; set; }
        public bool? Habilitado { get; set; }
        public string Extracolumn1 { get; set; }
        public string Extracolumn2 { get; set; }
        public string Extracolumn3 { get; set; }

        public virtual TipoEquipo IdTeqNavigation { get; set; }
        public virtual ICollection<Pm> Pms { get; set; }
        public virtual ICollection<Unidad> Unidads { get; set; }
    }
}
