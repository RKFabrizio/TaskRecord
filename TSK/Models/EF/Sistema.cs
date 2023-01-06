using System;
using System.Collections.Generic;

namespace TSK.Models.EF
{
    public partial class Sistema
    {
        public Sistema()
        {
            Componentes = new HashSet<Componente>();
            PmSistemas = new HashSet<PmSistema>();
        }

        public int IdSis { get; set; }
        public int? IdSec { get; set; }
        public string IdCod { get; set; }
        public string Nombre { get; set; }
        public bool? Habilitado { get; set; }
        public string Extracolumn1 { get; set; }
        public string Extracolumn2 { get; set; }
        public string Extracolumn3 { get; set; }

        public virtual Condicion IdCodNavigation { get; set; }
        public virtual Sector IdSecNavigation { get; set; }
        public virtual ICollection<Componente> Componentes { get; set; }
        public virtual ICollection<PmSistema> PmSistemas { get; set; }
    }
}
