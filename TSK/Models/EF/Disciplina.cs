using System;
using System.Collections.Generic;

namespace TSK.Models.EF
{
    public partial class Disciplina
    {
        public Disciplina()
        {
            PmSistemas = new HashSet<PmSistema>();
            Usuarios = new HashSet<Usuario>();
        }

        public int IdDis { get; set; }
        public string IdNiv { get; set; }
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
