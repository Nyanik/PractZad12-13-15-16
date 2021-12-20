using MultiPageApplication.Class;
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
    /// Логика взаимодействия для ZakupList.xaml
    /// </summary>
    public partial class ZakupList : Page
    {
        private Users _user;
        List<Zakup> ZakupStart = BaseClass.Base.Zakup.ToList();
        List<Zakup> FilZakupSort;
        PaginationClass PC = new PaginationClass();
        private Postavshik _post;     
        public ZakupList()
        {
            InitializeComponent();
            LVZakup.ItemsSource = ZakupStart;
            List<Komplect> PO = BaseClass.Base.Komplect.ToList();
            CBFilterKompl.Items.Add("Все записи");
            for (int i = 0; i < PO.Count; i++)
            {
                CBFilterKompl.Items.Add(PO[i].Kompl);
            }
            CBFilterKompl.SelectedIndex = 0;
            DataContext = PC;          
        }

        
        private void TextBlock_Loaded_1(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            int index = Convert.ToInt32(tb.Uid);
            List<Postavshik> TC = BaseClass.Base.Postavshik.Where(x => x.ID_Postavshik == index).ToList();
            string str = "";
            foreach (Postavshik item in TC)
            {
                str += item.NamePostavhik;
            }
            tb.Text = str.Substring(0);
        }

        private void TextBlock_Loaded_2(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            int index = Convert.ToInt32(tb.Uid);
            List<ZakupKomplect> TC = BaseClass.Base.ZakupKomplect.Where(x => x.ID_Zakup == index).ToList();
            int sum = 0;
            foreach (ZakupKomplect item in TC)
            {
                sum += item.Komplect.Cost * item.Kolvo;
            }
            tb.Text = sum + " рублей";
        }

        private void TextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            int index = Convert.ToInt32(tb.Uid);
            List<ZakupKomplect> TC = BaseClass.Base.ZakupKomplect.Where(x => x.ID_Zakup == index).ToList();
            string str = "";
            foreach (ZakupKomplect item in TC)
            {
                str += item.Kolvo;
            }
            tb.Text = "Количество:" + str.Substring(0);
        }

        private void TextBlock_Loaded_3(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            int index = Convert.ToInt32(tb.Uid);
            List<Komplect> TC = BaseClass.Base.Komplect.Where(x => x.ID_Komplect == index).ToList();
            string str = "";
            foreach (Komplect item in TC)
            {
                str += item.Kompl;
            }
            tb.Text = "Комплектующее:" + str.Substring(0);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.mainFrame.Navigate(new FirstPage());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FrameClass.mainFrame.Navigate(new FirstPage());
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Button B = (Button)sender; 
            int ind = Convert.ToInt32(B.Uid); 
            Zakup ZakDelete = BaseClass.Base.Zakup.FirstOrDefault(y => y.ID_Zakup == ind); 
            BaseClass.Base.Zakup.Remove(ZakDelete);  
            BaseClass.Base.SaveChanges();
            FrameClass.mainFrame.Navigate(new ZakupList()); 
            MessageBox.Show("Запись удалена");
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Button B = (Button)sender;
            int id = Convert.ToInt32(B.Uid);
            Zakup ZakupUpdate = BaseClass.Base.Zakup.FirstOrDefault(x => x.ID_Zakup == id);
            FrameClass.mainFrame.Navigate(new UpdatePage(ZakupUpdate, _user));
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            FrameClass.mainFrame.Navigate(new UpdatePage());
        }

        List<Zakup> ZakupFilter;
        
        private void Filters()
        {
            int index = CBFilterKompl.SelectedIndex;
            if (index != 0)
            {
                ZakupFilter = ZakupStart.Where(x => x.Postavshik.ID_Postavshik == index).ToList();
            }
            else
            {
                ZakupFilter = ZakupStart;
            }
           
            if (!string.IsNullOrWhiteSpace(TBFilterPostav.Text))
            {
                ZakupFilter = ZakupFilter.Where(x => x.Postavshik.NamePostavhik.ToLower().Contains(TBFilterPostav.Text.ToLower())).ToList();
            }
            LVZakup.ItemsSource = ZakupFilter;
        }

        private void CBFilterKompl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filters();
            txtPageCount_TextChanged(null, null);
        }

        private void TBFilterPostav_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filters();
            txtPageCount_TextChanged(null, null);
        }

        private void SortPoKompl_Checked(object sender, RoutedEventArgs e)
        {
            SortNum.IsChecked = false;
        }
        private void SortNum_Checked(object sender, RoutedEventArgs e)
        {
            SortPoKompl.IsChecked = false;
            
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if (SortPoKompl.IsChecked == true)
            {
                ZakupFilter.Sort((x, y) => x.Postavshik.NamePostavhik.CompareTo(y.Postavshik.NamePostavhik));
                LVZakup.Items.Refresh();
            }            
            if (SortNum.IsChecked == true)
            {
                ZakupFilter.Sort((x, y) => x.ID_Zakup.CompareTo(y.ID_Zakup));
                LVZakup.Items.Refresh();
            }
            txtPageCount_TextChanged(null, null);
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            if (SortPoKompl.IsChecked == true)
            {

                ZakupFilter.Sort((x, y) => x.Postavshik.NamePostavhik.CompareTo(y.Postavshik.NamePostavhik));
                ZakupFilter.Reverse();
                LVZakup.Items.Refresh();
            }
            if (SortNum.IsChecked == true)
            {
                ZakupFilter.Sort((x, y) => x.ID_Zakup.CompareTo(y.ID_Zakup));
                ZakupFilter.Reverse();
                LVZakup.Items.Refresh();
            }

            txtPageCount_TextChanged(null, null);
        }

        private void txtPageCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                PC.CountPage = Convert.ToInt32(txtPageCount.Text); // если в текстовом поле есnь значение, присваиваем его свойству объекта, которое хранит количество записей на странице
            }
            catch
            {
                PC.CountPage = ZakupFilter.Count;
            }
            PC.Countlist = ZakupFilter.Count;
            FilZakupSort = ZakupFilter.Skip(0).Take(PC.CountPage).ToList();
            LVZakup.ItemsSource = ZakupFilter.Skip(0).Take(PC.CountPage).ToList();
        }

        private void GoPage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            switch (tb.Uid)
            {
                case "prev":
                    PC.CurrentPage--;
                    break;
                case "next":
                    PC.CurrentPage++;
                    break;
                default:
                    PC.CurrentPage = Convert.ToInt32(tb.Text);
                    break;
            }
            FilZakupSort = ZakupFilter.Skip(PC.CurrentPage * PC.CountPage - PC.CountPage).Take(PC.CountPage).ToList();
            LVZakup.ItemsSource = ZakupFilter.Skip(PC.CurrentPage * PC.CountPage - PC.CountPage).Take(PC.CountPage).ToList();

        }
    }
}

