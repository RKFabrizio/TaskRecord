﻿using System;
using System.Collections.Generic;

namespace TSK.Models.EF
{
    public partial class Consecuencium
    {
        public Consecuencium()
        {
            Actividads = new HashSet<Actividad>();
        }

        public string IdCon { get; set; }
        public string Nombre { get; set; }
        public bool? Habilitado { get; set; }
        public string Extracolumn1 { get; set; }
        public string Extracolumn2 { get; set; }
        public string Extracolumn3 { get; set; }

        public virtual ICollection<Actividad> Actividads { get; set; }
    }
}
