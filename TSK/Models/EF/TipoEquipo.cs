using System;
using System.Collections.Generic;

namespace TSK.Models.EF
{
    public partial class TipoEquipo
    {
        public TipoEquipo()
        {
            Flota = new HashSet<Flotum>();
        }

        public int IdTeq { get; set; }
        public int IdEmp { get; set; }
        public string TEquipo { get; set; }
        public bool? Habilitado { get; set; }
        public string Extracolumn1 { get; set; }
        public string Extracolumn2 { get; set; }
        public string Extracolumn3 { get; set; }

        public virtual Empresa IdEmpNavigation { get; set; }
        public virtual ICollection<Flotum> Flota { get; set; }
    }
}
