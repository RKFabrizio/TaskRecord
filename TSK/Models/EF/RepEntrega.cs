using System;
using System.Collections.Generic;

namespace TSK.Models.EF
{
    public partial class RepEntrega
    {
        public int IdRent { get; set; }
        public int IdRep { get; set; }
        public int? IdEnt { get; set; }
        public string Resultado { get; set; }
        public bool? Habilitado { get; set; }
        public string Extracolumn1 { get; set; }
        public string Extracolumn2 { get; set; }
        public string Extracolumn3 { get; set; }

        public virtual Entrega IdEntNavigation { get; set; }
        public virtual Reporte IdRepNavigation { get; set; }
    }
}
