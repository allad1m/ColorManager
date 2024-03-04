using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorManager.DataBase
{
    public class Users
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), DefaultValue(0)]
        public int ID { get; set; }

        public string? Login { get; set; }

        public string? Password { get; set; }

        public string? Email { get; set; }

        public string? Name { get; set; }

        public string? JobTitle { get; set; }

        public string? PhoneNumber { get; set; }

        [Required, DefaultValue(0)]
        public int CommercialOnce { get; set; }

        [Required, DefaultValue(0)]
        public int MakingPaintsOnce { get; set; }

        [Required, DefaultValue(0)]
        public int MakingPaintsCount { get; set; }

        [Required, DefaultValue(0)]
        public int RequestRecipesOnce { get; set; }

        [Required, DefaultValue(0)]
        public int CheckAnalogueOnce { get; set; }

        public string? IP { get; set; }
    }
}
