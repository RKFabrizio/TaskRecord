using System;
using System.Collections.Generic;

namespace TSK.Models.EF
{
    public partial class Tab
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Page { get; set; }
        public bool? Habilitado { get; set; }
    }
}
