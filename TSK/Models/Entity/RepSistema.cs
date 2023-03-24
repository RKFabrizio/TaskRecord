using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TSK.Models.Entity
{
    public partial class RepSistema
    {
        public RepSistema()
        {
            RepDetalles = new HashSet<RepDetalle>();
        }

        public int IdRepsis { get; set; }
        public int IdRep { get; set; }
        public string NomSistema { get; set; }
        public string NomDisciplina { get; set; }
        public double Avance { get; set; }
        public bool? Habilitado { get; set; }

        public string Extracolumn1 { get; set; }
        public string Extracolumn2 { get; set; }
        public string Extracolumn3 { get; set; }
        public string IdCod { get; set; }
        public virtual Reporte IdRepNavigation { get; set; }
        public virtual ICollection<RepDetalle> RepDetalles { get; set; }
    }
}
