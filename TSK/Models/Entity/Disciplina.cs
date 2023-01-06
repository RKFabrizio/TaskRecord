using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TSK.Models.Entity
{
    public partial class Disciplina
    {
        public Disciplina()
        {
            PmSistemas = new HashSet<PmSistema>();
            Usuarios = new HashSet<Usuario>();
        }

        public int IdDis { get; set; }

        [Required(ErrorMessage = "El Nivel es obligatorio")]
        public string IdNiv { get; set; }

        [Required(ErrorMessage = "El Nombre es obligatorio")]
        public string Nombre { get; set; }
        public bool? Habilitado { get; set; }
        public string Extracolumn1 { get; set; }
        public string Extracolumn2 { get; set; }
        public string Extracolumn3 { get; set; }

        public virtual Nivel IdNivNavigation { get; set; }
        public virtual ICollection<PmSistema> PmSistemas { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
