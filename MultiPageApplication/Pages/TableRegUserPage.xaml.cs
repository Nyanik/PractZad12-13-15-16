﻿using System;
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

namespace MultiPageApplication
{
    /// <summary>
    /// Логика взаимодействия для TableRegUserPage.xaml
    /// </summary>
    public partial class TableRegUserPage : Page
    {
        public TableRegUserPage()
        {
            InitializeComponent();
            DGUsers.ItemsSource = BaseClass.Base.Users.ToList();
        }
    }
}
