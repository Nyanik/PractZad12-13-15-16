using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MultiPageApplication.Pages
{
    /// <summary>
    /// Логика взаимодействия для UpdatePage.xaml
    /// </summary>
    public partial class UpdatePage : Page
    {
        string path;  
        Zakup ZAK = new Zakup();  
        bool flag;  
        List<ZakupKomplect> ZakKomList = BaseClass.Base.ZakupKomplect.ToList();
          
        public UpdatePage()
        {
            InitializeComponent();
            flag = true;  
            LBKomplect.ItemsSource = BaseClass.Base.Komplect.ToList();  
            LBKomplect.SelectedValuePath = "ID_Komplect";
            LBKomplect.DisplayMemberPath = "Kompl";
            LBPostavshik.ItemsSource = BaseClass.Base.Postavshik.ToList();
            LBPostavshik.SelectedValuePath = "ID_Postavshik";
            LBPostavshik.DisplayMemberPath = "NamePostavhik";
        }
       
        public UpdatePage(Zakup ZakUpdate, Users users)
        {
            InitializeComponent();
            ZAK = ZakUpdate;
            int idkompl = 0;

            switch (LBKomplect.SelectedIndex)
            {
                case 0:
                    idkompl = 1;
                    break;
                case 1:
                    idkompl = 2;
                    break;
            }
            int idpostav = 0;
            switch (LBPostavshik.SelectedIndex)
            {
                case 0:
                    idpostav = 1;
                    break;
                case 1:
                    idpostav = 2;
                    break;
            }
            idkompl = ZakUpdate.ID_Komplect;
            idpostav = ZakUpdate.ID_Postavshik;

            DPDate.SelectedDate = ZakUpdate.Data_zakup;

            LBKomplect.ItemsSource = BaseClass.Base.Komplect.ToList();
            LBKomplect.SelectedValuePath = "ID_Komplect";
            LBKomplect.DisplayMemberPath = "Kompl";
            LBPostavshik.ItemsSource = BaseClass.Base.Postavshik.ToList();
            LBPostavshik.SelectedValuePath = "ID_Postavshik";
            LBPostavshik.DisplayMemberPath = "NamePostavhik";

            List<ZakupKomplect> D = ZakKomList.Where(x => x.ID_Komplect == ZakUpdate.ID_Komplect).ToList();

            foreach (Komplect f in LBKomplect.Items)
            {
                ZakupKomplect obj = D.FirstOrDefault(x => x.ID_Zakup_Komplect == f.ID_Komplect);
                if (obj != null)
                {
                    obj.Kolvo = Convert.ToInt32(TBKolvo.Text);
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int idkompl = 0;

                switch (LBKomplect.SelectedIndex)
                {
                    case 0:
                        idkompl = 1;
                        break;
                    case 1:
                        idkompl = 2;
                        break;
                }
                int idpostav=0;
                switch (LBPostavshik.SelectedIndex)
                {
                    case 0:
                        idpostav = 1;
                        break;
                    case 1:
                        idpostav = 2;
                        break;
                }
                ZAK.ID_Komplect = idkompl;
                ZAK.ID_Postavshik = idpostav;
                ZAK.Data_zakup = DPDate.DisplayDate.Date;
                if (flag == true)
                {
                    BaseClass.Base.Zakup.Add(ZAK);
                }
                BaseClass.Base.SaveChanges();
                List<ZakupKomplect> LTC = ZakKomList.Where(x => x.ID_Zakup == ZAK.ID_Zakup).ToList();  
                if (LTC.Count != 0)  
                {
                    foreach (ZakupKomplect tc in LTC)
                    {
                        BaseClass.Base.ZakupKomplect.Remove(tc);
                    }
                }
                foreach (Komplect t in LBKomplect.SelectedItems)  
                {
                    ZakupKomplect TC = new ZakupKomplect();
                    TC.ID_Zakup = ZAK.ID_Zakup;  
                    TC.ID_Komplect = t.ID_Komplect;
                    TC.Kolvo = Convert.ToInt32(TBKolvo.Text);                    
                    BaseClass.Base.ZakupKomplect.Add(TC); 
                }               
                BaseClass.Base.SaveChanges();               
                MessageBox.Show("Данные записаны");
                FrameClass.mainFrame.Navigate(new ZakupList());
            }
            catch
            {
                MessageBox.Show("Данные не записаны");
            }
        }
    }
}
