using System.ComponentModel.DataAnnotations;

namespace TSK.Models
{
    public class Usuario
    {
        public int idUsuario { get; set; }
        
        [Required(ErrorMessage = "El campo idRol es obligatorio")]
        public int idRol { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        public string Nombre { get; set; }

        public int idDisciplina { get; set; }

        [Required(ErrorMessage = "El campo Usuario es obligatorio")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "El campo Clave es obligatorio")]
        public string Clave { get; set; }

        public string[] Roles { get; set; }


    }
}
