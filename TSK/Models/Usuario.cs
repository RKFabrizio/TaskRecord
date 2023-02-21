using System.ComponentModel.DataAnnotations;

namespace TSK.Models
{
    public class Usuario
    {
        public int idUsuario { get; set; }
        
        [Required(ErrorMessage = "El campo Posicion es obligatorio")]
        public int idPos { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        public string Nombre { get; set; }

        public string UserName { get; set; }

        [Required(ErrorMessage = "El campo Clave es obligatorio")]
        public string Clave { get; set; }


        public string[] Posiciones { get; set; }

        public bool Habilitado { get; set; }

    }
}
