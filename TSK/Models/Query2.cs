using System.ComponentModel.DataAnnotations;

namespace TSK.Models
{
    public class Query2
    {
        public int IdPms { get; set; }
        public int IdAct { get; set; }
        public int? Orden { get; set; }
        public string NombreActividad { get; set; }
    }

}
