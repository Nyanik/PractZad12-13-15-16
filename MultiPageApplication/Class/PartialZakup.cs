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
                switch (ID_Postavshik)
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
    }
}
