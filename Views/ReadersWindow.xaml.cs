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
using System.Windows.Shapes;

namespace Library.Views
{
    /// <summary>
    /// Логика взаимодействия для ReadersWindow.xaml
    /// </summary>
    public partial class ReadersWindow : Window
    {
        public ReadersWindow()
        {
            InitializeComponent();
            DataContext = App.ReadersViewModel;
            Closed += (s, e) => App.Repository.SaveChanges();

        }
    }
}
