using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TSK.Models.Entity
{
    public partial class GrupoUsuario
    {

        public int IdGrus { get; set; }

        public int IdRep { get; set; }
        public int IdUsr { get; set; }
        public int Grupo { get; set; }
        public bool Lider { get; set; }

        public virtual Usuario IdUsrNavigation { get; set; }
        public virtual Reporte IdRepNavigation { get; set; }
    }
}
