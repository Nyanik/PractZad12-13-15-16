using Microsoft.Win32;
using MultiPageApplication.Pages;
using System;
using System.Collections.Generic;
using System.Drawing;
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
    /// Логика взаимодействия для SecretPage.xaml
    /// </summary>
    public partial class SecretPage : Page
    {
        private Users _user;
        private string _path;
        private UserPhoto UP;
        //private UsersPhoto UP;  // пустой объект для добавления изображения в таблицу UsersPhoto

        public SecretPage(Users User)
        {
            InitializeComponent();
            _user = User;
            TBUserName.Text = _user.Name;
            TBUserSurname.Text = _user.Surname;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.mainFrame.Navigate(new FirstPage());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            UpdateLP upLS = new UpdateLP(_user);
            upLS.ShowDialog();
            FrameClass.mainFrame.Navigate(new AutoPage());
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            UpdateWindow upWin = new UpdateWindow(_user);  // создаем окно для редактирования данных
            upWin.ShowDialog();  // открываем окно
            // далее пишется код, который будет выполнятся после закрытия окна
            FrameClass.mainFrame.Navigate(new SecretPage(_user));  // перезагружаем страницу
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                UserPhoto U = BaseClass.Base.UserPhoto.FirstOrDefault(x => x.IDUser == _user.ID_User);
                if (U == null)
                {
                    UP = new UserPhoto();
                    UP.IDUser = _user.ID_User;
                    OpenFileDialog OFD = new OpenFileDialog();
                    OFD.ShowDialog();
                    _path = OFD.FileName;
                    System.Drawing.Image SDI = System.Drawing.Image.FromFile(_path);
                    ImageConverter IC = new ImageConverter();
                    byte[] PhotoArray = (byte[])IC.ConvertTo(SDI, typeof(byte[]));
                    UP.PhotoBinary = PhotoArray;
                    BaseClass.Base.UserPhoto.Add(UP);
                    BaseClass.Base.SaveChanges();
                    MessageBox.Show("Картинка добавлена", "Добавление");
                }
                else
                {
                    OpenFileDialog OFD = new OpenFileDialog();
                    OFD.ShowDialog();
                    _path = OFD.FileName;
                    System.Drawing.Image SDI = System.Drawing.Image.FromFile(_path);
                    ImageConverter IC = new ImageConverter();
                    byte[] PhotoArray = (byte[])IC.ConvertTo(SDI, typeof(byte[]));
                    U.PhotoBinary = PhotoArray;
                    BaseClass.Base.SaveChanges();
                    MessageBox.Show("Картинка изменена", "Редактирование");
                }

                FrameClass.mainFrame.Navigate(new SecretPage(_user));


            }
            catch
            {
                MessageBox.Show("Картинка не выбрана", "Ошибка");
            }
        }
    }
}
