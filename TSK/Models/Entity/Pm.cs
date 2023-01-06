using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TSK.Models.Entity
{
    public partial class Pm
    {
        public Pm()
        {
            PmSistemas = new HashSet<PmSistema>();
            Reportes = new HashSet<Reporte>();
        }

        public int IdPm { get; set; }

        [Required(ErrorMessage = "El nombre es obligatoria")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria")]
        public string Descripcion { get; set; }
        public bool? Habilitado { get; set; }
        public string Extracolumn1 { get; set; }
        public string Extracolumn2 { get; set; }
        public string Extracolumn3 { get; set; }

        public string IdPmCopy { get; set; }

        public int IdFlt { get; set; }

        public virtual ICollection<PmSistema> PmSistemas { get; set; }
        public virtual ICollection<Reporte> Reportes { get; set; }

        public virtual Flotum IdFltNavigation { get; set; }

       
    }
}
