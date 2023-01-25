using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TSK.Models.Entity
{
    public partial class Usuario
    {
        public Usuario()
        {
            Auditoria = new HashSet<Auditorium>();
            GrupoUsuarios = new HashSet<GrupoUsuario>();
        }

        public int IdUsr { get; set; }

        [Required(ErrorMessage = "La Posicion es obligatoria")]
        public int IdPos { get; set; }

        [Required(ErrorMessage = "El Nombre es obligatorio")]
        public string Nombre { get; set; }


        [Required(ErrorMessage = "El Usuario es obligatorio")]
        public string Usuario1 { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatorio")]
        public string Contrasena { get; set; }

        public bool? Habilitado { get; set; }
        public string Extracolumn1 { get; set; }
        public string Extracolumn2 { get; set; }
        public string Extracolumn3 { get; set; }

        public virtual Posicion IdPosNavigation { get; set; }
        public virtual GrupoUsuario IdGrusNavigation { get; set; }
        public virtual ICollection<Auditorium> Auditoria { get; set; }
        public virtual ICollection<GrupoUsuario> GrupoUsuarios { get; set; }
    }
}
