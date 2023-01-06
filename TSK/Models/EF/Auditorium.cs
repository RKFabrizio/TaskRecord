using System;
using System.Collections.Generic;

namespace TSK.Models.EF
{
    public partial class Auditorium
    {
        public int IdAud { get; set; }
        public int IdUsr { get; set; }
        public int IdTab { get; set; }
        public DateTime? Dia { get; set; }
        public int IdOpe { get; set; }
        public bool? Habilitado { get; set; }
        public string Extracolumn1 { get; set; }
        public string Extracolumn2 { get; set; }
        public string Extracolumn3 { get; set; }

        public virtual Operacion IdOpeNavigation { get; set; }
        public virtual Usuario IdUsrNavigation { get; set; }
    }
}
