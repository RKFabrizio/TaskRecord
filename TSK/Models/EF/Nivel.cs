using System;
using System.Collections.Generic;

namespace TSK.Models.EF
{
    public partial class Nivel
    {
        public Nivel()
        {
            Disciplinas = new HashSet<Disciplina>();
        }

        public string IdNiv { get; set; }
        public string Nombre { get; set; }
        public bool? Habilitado { get; set; }
        public string Extracolumn1 { get; set; }
        public string Extracolumn2 { get; set; }
        public string Extracolumn3 { get; set; }

        public virtual ICollection<Disciplina> Disciplinas { get; set; }
    }
}
