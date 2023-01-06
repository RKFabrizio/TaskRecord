using System;
using System.Collections.Generic;

namespace TSK.Models.EF
{
    public partial class Reporte
    {
        public Reporte()
        {
            RepEntregas = new HashSet<RepEntrega>();
            RepSistemas = new HashSet<RepSistema>();
        }

        public int IdRep { get; set; }
        public int IdPm { get; set; }
        public int IdUni { get; set; }
        public DateTime? Fecha { get; set; }
        public int? Horometro { get; set; }
        public string Comentario { get; set; }
        public bool? Creado { get; set; }
        public double? Avance { get; set; }
        public bool? Habilitado { get; set; }
        public string Extracolumn1 { get; set; }
        public string Extracolumn2 { get; set; }
        public string Extracolumn3 { get; set; }

        public virtual Pm IdPmNavigation { get; set; }
        public virtual Unidad IdUniNavigation { get; set; }
        public virtual ICollection<RepEntrega> RepEntregas { get; set; }
        public virtual ICollection<RepSistema> RepSistemas { get; set; }
    }
}
