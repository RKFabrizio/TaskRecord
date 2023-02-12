using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TSK.Models.Entity
{
    public partial class Actividad
    {
        public Actividad()
        {
            PmsisActividads = new HashSet<PmsisActividad>();

        }

        public int IdAct { get; set; }
        public string? IdCon { get; set; }
        public string? IdClm { get; set; }
        public int? IdDis { get; set; }

        [Required(ErrorMessage = "El Título es obligatorio")]
        public string? Titulo { get; set; }
        public string? IdUm { get; set; }
        public int? IdFrc { get; set; }
        public int? IdRan { get; set; }
        public string Especificacion { get; set; }
        public double? Inicio { get; set; }
        public double? Fin { get; set; }
        public string? Referencia { get; set; }
        public string? ReferenciaUrl { get; set; }
        public bool? Habilitado { get; set; }
        public string? Descripcion { get; set; }
        public decimal? Duracion { get; set; }
        public string? SubActividad { get; set; }



        public virtual Disciplina IdDisNavigation { get; set; }
        public virtual ClaseMantencion IdClmNavigation { get; set; }
        public virtual Consecuencium IdConNavigation { get; set; }
        public virtual Frecuencium IdFrcNavigation { get; set; }
        public virtual Rango IdRanNavigation { get; set; }
        public virtual UnidadMedidum IdUmNavigation { get; set; }
        public virtual ICollection<PmsisActividad> PmsisActividads { get; set; }

    }
}
