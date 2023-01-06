using System;
using System.Collections.Generic;

namespace TSK.Models.EF
{
    public partial class VwPmSistema
    {
        public int IdPms { get; set; }
        public int IdSis { get; set; }
        public string Sistema { get; set; }
        public int? IdSec { get; set; }
        public string Sector { get; set; }
        public string IdCod { get; set; }
        public string Condicion { get; set; }
        public string IdPm { get; set; }
        public string Pm { get; set; }
        public int IdDis { get; set; }
        public string Disciplina { get; set; }
    }
}
