using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TSK.Models.Entity
{
    public partial class Tabs
    {
        public Tabs()
        {
            
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public string Page { get; set; }

        public bool? Habilitado { get; set; }


    }
}
