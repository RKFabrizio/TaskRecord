using System;
using System.Collections.Generic;

namespace TSK.Models.EF
{
    public partial class Complemento
    {
        public int IdCom { get; set; }
        public int? IdRep { get; set; }
        public int? IdEqu { get; set; }
        public int? IdHerr { get; set; }
        public int IdPms { get; set; }
        public bool? Habilitado { get; set; }
        public byte[] Extracolumn1 { get; set; }
        public byte[] Extracolumn2 { get; set; }
        public string Extracolumn3 { get; set; }

        public virtual Equipo IdEquNavigation { get; set; }
        public virtual Herramientum IdHerrNavigation { get; set; }
        public virtual PmSistema IdPmsNavigation { get; set; }
        public virtual Repuesto IdRepNavigation { get; set; }
    }
}
