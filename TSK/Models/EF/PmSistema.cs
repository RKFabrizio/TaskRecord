using System;
using System.Collections.Generic;

namespace TSK.Models.EF
{
    public partial class PmSistema
    {
        public PmSistema()
        {
            Complementos = new HashSet<Complemento>();
            PmsisActividads = new HashSet<PmsisActividad>();
        }

        public int IdPms { get; set; }
        public int IdSis { get; set; }
        public int IdPm { get; set; }
        public int IdDis { get; set; }
        public bool? Habilitado { get; set; }
        public string Extracolumn1 { get; set; }
        public string Extracolumn2 { get; set; }
        public string Extracolumn3 { get; set; }

        public virtual Disciplina IdDisNavigation { get; set; }
        public virtual Pm IdPmNavigation { get; set; }
        public virtual Sistema IdSisNavigation { get; set; }
        public virtual ICollection<Complemento> Complementos { get; set; }
        public virtual ICollection<PmsisActividad> PmsisActividads { get; set; }
    }
}
