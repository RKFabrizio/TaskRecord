using System;
using System.Collections.Generic;

namespace TSK.Models.EF
{
    public partial class RepTecnico
    {
        public int? IdRep { get; set; }
        public int? IdUsr { get; set; }
        public int? IdTur { get; set; }

        public virtual Reporte IdRepNavigation { get; set; }
        public virtual Turno IdTurNavigation { get; set; }
        public virtual Usuario IdUsrNavigation { get; set; }
    }
}
