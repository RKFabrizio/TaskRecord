using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TSK.Models.Entity
{
    public partial class Nivel
    {
        public Nivel()
        {
            Disciplinas = new HashSet<Disciplina>();
        }

        [Required(ErrorMessage = "El ID es obligatorio")]
        public string IdNiv { get; set; }

        [Required(ErrorMessage = "El Nombre es obligatorio")]
        public string Nombre { get; set; }
        public bool? Habilitado { get; set; }
        public string Extracolumn1 { get; set; }
        public string Extracolumn2 { get; set; }
        public string Extracolumn3 { get; set; }

        public virtual ICollection<Disciplina> Disciplinas { get; set; }
    }
}
