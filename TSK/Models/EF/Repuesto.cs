using System;
using System.Collections.Generic;

namespace TSK.Models.EF
{
    public partial class Repuesto
    {
        public Repuesto()
        {
            Complementos = new HashSet<Complemento>();
        }

        public int IdRep { get; set; }
        public string Nombre { get; set; }
        public string Nroparte { get; set; }
        public bool? Habilitado { get; set; }
        public string Extracolumn1 { get; set; }
        public string Extracolumn2 { get; set; }
        public string Extracolumn3 { get; set; }

        public virtual ICollection<Complemento> Complementos { get; set; }
    }
}
