using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ColorManager.Data.Models
{
    public class PalletersModel
    {
        public string ProductGroup;
        public string ColorFan;
        public string Color;
        public string LongName;
        public static SolidColorBrush? ColorValue;
        public static bool Availability;


        public PalletersModel(string productGroup, string colorFan, string color, string hex, bool availability)
        {
            ProductGroup = productGroup;
            ColorFan = colorFan;
            Color = color;
            LongName = $"{productGroup}, {colorFan}, {color}";
            Availability = availability;
            ColorValue = new BrushConverter().ConvertFrom(hex) as SolidColorBrush;
        }
       
    }
}
