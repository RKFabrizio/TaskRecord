using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TSK.Models.Entity
{
    public partial class RepLider
    {
        public int? IdRep { get; set; }
        public int? IdUser { get; set; }
        public int? IdTur { get; set; }

        public virtual Reporte IdRepNavigation { get; set; }
        public virtual Turno IdTurNavigation { get; set; }
        public virtual Usuario IdUserNavigation { get; set; }
    }
}