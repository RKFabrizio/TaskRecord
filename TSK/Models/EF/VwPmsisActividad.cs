using System;
using System.Collections.Generic;

namespace TSK.Models.EF
{
    public partial class VwPmsisActividad
    {
        public int IdPms { get; set; }
        public int IdAct { get; set; }
        public string Titulo { get; set; }
        public string IdCon { get; set; }
        public string IdClm { get; set; }
        public string IdUm { get; set; }
        public int? IdFrc { get; set; }
        public int? IdRan { get; set; }
        public string Especificacion { get; set; }
        public double? Inicio { get; set; }
        public double? Fin { get; set; }
        public string Referencia { get; set; }
        public string ReferenciaUrl { get; set; }
        public int? Orden { get; set; }
        public bool? Habilitado { get; set; }
    }
}
