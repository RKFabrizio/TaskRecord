using System;
using System.Collections.Generic;

namespace TSK.Models.EF
{
    public partial class Sector
    {
        public Sector()
        {
            Sistemas = new HashSet<Sistema>();
        }

        public int IdSec { get; set; }
        public string Nombre { get; set; }
        public bool? Habilitado { get; set; }
        public string Extracolumn1 { get; set; }
        public string Extracolumn2 { get; set; }
        public string Extracolumn3 { get; set; }

        public virtual ICollection<Sistema> Sistemas { get; set; }
    }
}
