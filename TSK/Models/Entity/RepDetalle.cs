using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TSK.Models.Entity
{
    public partial class RepDetalle
    {
        public int IdRepsis { get; set; }
        public int IdAct { get; set; }
        
        public string Titulo { get; set; }
        public string Especificacion { get; set; }

        public double? Valor { get; set; }

        public int IdEst { get; set; }

        public string Referencia { get; set; }
        public string ReferenciaURL { get; set; }
        public string Observacion { get; set; }
        public int? Orden { get; set; }
        public bool? Habilitado { get; set; }
        public string Extracolumn1 { get; set; }
        public string Extracolumn2 { get; set; }
        public string Extracolumn3 { get; set; }

        public virtual RepSistema IdRepsisNavigation { get; set; }

        public virtual Estado IdEstadoNavigation { get; set; }
    }
}
