using System;
using System.Collections.Generic;

namespace TSK.Models.EF
{
    public partial class Usuario
    {
        public Usuario()
        {
            Auditoria = new HashSet<Auditorium>();
        }

        public int IdUsr { get; set; }
        public int IdRol { get; set; }
        public string Nombre { get; set; }
        public int? IdDis { get; set; }
        public string Usuario1 { get; set; }
        public string Contrasena { get; set; }
        public bool? Lider { get; set; }
        public bool? Habilitado { get; set; }
        public string Extracolumn1 { get; set; }
        public string Extracolumn2 { get; set; }
        public string Extracolumn3 { get; set; }

        public virtual Disciplina IdDisNavigation { get; set; }
        public virtual Rol IdRolNavigation { get; set; }
        public virtual ICollection<Auditorium> Auditoria { get; set; }
    }
}
