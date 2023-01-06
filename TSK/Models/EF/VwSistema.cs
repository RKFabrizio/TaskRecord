using System;
using System.Collections.Generic;

namespace TSK.Models.EF
{
    public partial class VwSistema
    {
        public int IdSis { get; set; }
        public string Nombre { get; set; }
        public int? IdSec { get; set; }
        public string Sector { get; set; }
        public string IdCod { get; set; }
        public string Condicion { get; set; }
    }
}
