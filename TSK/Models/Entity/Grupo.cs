using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TSK.Models.Entity
{
    public class Grupo
    {
        public Grupo(){
            GrupoUsuarios = new HashSet<GrupoUsuario>();
        }
        public int IdGru { get; set; }
        public int NGru { get; set; }
        public virtual ICollection<GrupoUsuario> GrupoUsuarios { get; set; }
    }

}
