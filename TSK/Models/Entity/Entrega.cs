using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TSK.Models.Entity
{
    public partial class Entrega
    {
        public Entrega()
        {
            RepEntregas = new HashSet<RepEntrega>();
        }
        public int IdEnt { get; set; }

        [Required(ErrorMessage = "El Nombre es obligatorio")]
        public string Nombre { get; set; }
        public bool? Habilitado { get; set; }
        public string Extracolumn1 { get; set; }
        public string Extracolumn2 { get; set; }
        public string Extracolumn3 { get; set; }

        public virtual ICollection<RepEntrega> RepEntregas { get; set; }
    }
}
