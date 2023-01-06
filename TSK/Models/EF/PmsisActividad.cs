using System;
using System.Collections.Generic;

namespace TSK.Models.EF
{
    public partial class PmsisActividad
    {
        public int IdAct { get; set; }
        public int IdPms { get; set; }
        public int? Orden { get; set; }
        public bool? Habilitado { get; set; }
        public string Extracolumn1 { get; set; }
        public string Extracolumn2 { get; set; }
        public string Extracolumn3 { get; set; }

        public virtual Actividad IdActNavigation { get; set; }
        public virtual PmSistema IdPmsNavigation { get; set; }
    }
}
