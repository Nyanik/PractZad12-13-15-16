using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MultiPageApplication
{
    public partial class Zakup
    {
        public SolidColorBrush KomplectColor
        {
            get
            {
                switch (ID_Komplect)
                {
                    case 1:
                        return Brushes.LightBlue;
                    case 2:
                        return Brushes.LightPink;
                    default:
                        return Brushes.White;
                }
            }
        }
        public string Kolvo
        {
            get
            {
                switch (Kol_komp_zakup)
                {
                    default:
                        return Kol_komp_zakup + " ед.";
                }
            }
        }
    }
}
