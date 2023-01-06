using System;
using System.Collections.Generic;

namespace TSK.Models.EF
{
    public partial class Pm
    {
        public Pm()
        {
            PmSistemas = new HashSet<PmSistema>();
            Reportes = new HashSet<Reporte>();
        }

        public int IdPm { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool? Habilitado { get; set; }
        public string Extracolumn1 { get; set; }
        public string Extracolumn2 { get; set; }
        public string Extracolumn3 { get; set; }
        public int? IdFlt { get; set; }
        public string IdPmcopy { get; set; }

        public virtual Flotum IdFltNavigation { get; set; }
        public virtual ICollection<PmSistema> PmSistemas { get; set; }
        public virtual ICollection<Reporte> Reportes { get; set; }
    }
}
