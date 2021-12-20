using System;
using System.Collections.Generic;
using System.IO;
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

namespace MultiPageApplication
{
    /// <summary>
    /// Логика взаимодействия для AutoPage.xaml
    /// </summary>
    public partial class AutoPage : Page
    {       
        public AutoPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.mainFrame.Navigate(new FirstPage());
        }

        private void BtnAuto_Click(object sender, RoutedEventArgs e)
        {
            int passwordCode = PBAutoPassword.Password.GetHashCode();
            Users User = BaseClass.Base.Users.FirstOrDefault(z => z.Login == TBAutoLogin.Text && z.Password == passwordCode);
            if(User == null)
            {
                MessageBox.Show("Вы не зарегистрированы");
            }
            else
            {
                switch (User.ID_Role)
                {
                    case 1:
                        MessageBox.Show("Здравствуйте, Администратор " + User.Name);
                        FrameClass.mainFrame.Navigate(new AdminPage());
                            break;
                    case 2:
                        MessageBox.Show("Здравствуйте, Пользователь " + User.Name);
                        FrameClass.mainFrame.Navigate(new SecretPage(User));
                        break;
                }
            }
 
        }
    }
}
