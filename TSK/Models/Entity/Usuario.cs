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
            Reportes = new HashSet<Reporte>();
        }

        public int IdUsr { get; set; }

        [Required(ErrorMessage = "El Rol es obligatorio")]
        public int IdRol { get; set; }

        [Required(ErrorMessage = "El Nombre es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La disciplina es obligatoria")]
        public int? IdDis { get; set; }

        [Required(ErrorMessage = "El Usuario es obligatorio")]
        public string Usuario1 { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatorio")]
        public string Contrasena { get; set; }

        public bool? Lider { get; set; }
        public bool? Habilitado { get; set; }
        public string Extracolumn1 { get; set; }
        public string Extracolumn2 { get; set; }
        public string Extracolumn3 { get; set; }

        public virtual Disciplina IdDisNavigation { get; set; }
        public virtual Rol IdRolNavigation { get; set; }
        public virtual ICollection<Auditorium> Auditoria { get; set; }
        public virtual ICollection<Reporte> Reportes { get; set; }
    }
}
