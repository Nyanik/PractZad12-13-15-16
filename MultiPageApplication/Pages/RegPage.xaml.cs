using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace MultiPageApplication
{
    /// <summary>
    /// Логика взаимодействия для RegPage.xaml
    /// </summary>
    public partial class RegPage : Page
    {
        
        
        public RegPage()
        {
            InitializeComponent();
            CBGenderReg.ItemsSource = BaseClass.Base.GenderTable.ToList();
            CBGenderReg.SelectedValuePath = "ID_Gender";
            CBGenderReg.DisplayMemberPath = "Gender";
        }

        private void BtnReg_Click(object sender, RoutedEventArgs e)
        {            
            int c = 0;
            Regex r = new Regex("^.*(?=.*[0-9]{2}).*$");
            if (r.IsMatch(TBPasswordReg.Password.ToString()) == false)
            {
                MessageBox.Show("Слабый пароль! Менее 2-ух чисел.", "Регистрация");
            }
            else { c += 1; }
            r = new Regex("^.*(?=.*[A-Z]).*$");
            if (r.IsMatch(TBPasswordReg.Password.ToString()) == false)
            {
                MessageBox.Show("Слабый пароль! Менее 1-го заглавного символа латинского алфавита.", "Регистрация");
            }
            else { c += 1; }
            r = new Regex("^.*(?=.*[a-z]{3}).*$");
            if (r.IsMatch(TBPasswordReg.Password.ToString()) == false)
            {
                MessageBox.Show("Слабый пароль! Менее 3-ёх символов латинского алфавита", "Регистрация");
            }
            else { c += 1; }
            r = new Regex("^.*(?=.*[!@#$%^&*()?+=]).*$");
            if (r.IsMatch(TBPasswordReg.Password.ToString()) == false)
            {
                MessageBox.Show("Слабый пароль! Менее 1-го специального символа.", "Регистрация");
            }
            else { c += 1; }
            r = new Regex("^.*(?=.{8,}).*$");
            if (r.IsMatch(TBPasswordReg.Password.ToString()) == false)
            {
                MessageBox.Show("Слабый пароль! Менее 8-ми символов.", "Регистрация");
            }
            else { c += 1; }
            if (c == 5)
            {
                int pasRegCode = TBPasswordReg.Password.GetHashCode();
                Users UserReg = new Users() { Name = TBNameReg.Text, Surname = TBSurnameReg.Text, Login = TBLoginReg.Text, Password = pasRegCode, ID_Gender = CBGenderReg.SelectedIndex + 1, ID_Role = 2 };
                BaseClass.Base.Users.Add(UserReg);
                BaseClass.Base.SaveChanges();
                MessageBox.Show("Вы зарегистрированы");
                FrameClass.mainFrame.Navigate(new AutoPage());
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.mainFrame.Navigate(new FirstPage());
        }
    }
}
