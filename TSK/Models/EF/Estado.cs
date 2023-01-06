using System;
using System.Collections.Generic;

namespace TSK.Models.EF
{
    public partial class Estado
    {
        public Estado()
        {
            RepDetalles = new HashSet<RepDetalle>();
        }

        public int IdEst { get; set; }
        public string Valor { get; set; }
        public bool? Habilitado { get; set; }

        public virtual ICollection<RepDetalle> RepDetalles { get; set; }
    }
}
