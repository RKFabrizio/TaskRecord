using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TSK.Models.Entity
{
    public partial class Posicion
    {
        public Posicion()
        {
            Usuarios = new HashSet<Usuario>();
        }

        public int IdPos { get; set; }

        [Required(ErrorMessage = "El Cargo es obligatorio")]
        public string Cargo { get; set; }
        public bool? Habilitado { get; set; }
        public string Extracolumn1 { get; set; }
        public string Extracolumn2 { get; set; }
        public string Extracolumn3 { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
