using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TSK.Models.Entity
{
    public partial class Complemento
    {
        public int IdCom { get; set; }

        [Required(ErrorMessage = "El Repuesto es obligatorio")]
        public int? IdRep { get; set; }

        [Required(ErrorMessage = "El Equipo es obligatorio")]
        public int? IdEqu { get; set; }

        [Required(ErrorMessage = "La herramienta es obligatoria")]
        public int? IdHerr { get; set; }

        [Required(ErrorMessage = "El PM Sistema es obligatorio")]
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
