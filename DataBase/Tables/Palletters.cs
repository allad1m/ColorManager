using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace ColorManager.DataBase.Tables
{
    public class Palletters
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), DefaultValue(0)]
        public int ID { get; set; }

        public string? ProductGroup { get; set; }

        public string? ColorFan { get; set; }

        public string? Color { get; set; }

        public string? ColorValue { get; set; }

        [NotNull, DefaultValue(false)]
        public bool InStock { get; set; }


    }
}
