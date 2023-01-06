using System;
using System.Collections.Generic;

namespace TSK.Models.EF
{
    public partial class Herramientum
    {
        public Herramientum()
        {
            Complementos = new HashSet<Complemento>();
        }

        public int IdHerr { get; set; }
        public string Nombre { get; set; }
        public bool? Habilitado { get; set; }
        public string Extracolumn1 { get; set; }
        public string Extracolumn2 { get; set; }
        public string Extracolumn3 { get; set; }

        public virtual ICollection<Complemento> Complementos { get; set; }
    }
}
