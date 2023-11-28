using System;
using System.Windows;
using System.IO;

namespace WpfApp4.View
{
    /// <summary>
    /// Логика взаимодействия для DataWindow.xaml
    /// </summary>
    public partial class DataWindow : Window
    {
        public DataWindow()
        {
            InitializeComponent();
            DataContext = new DataViewModel(fd);
        }
    }
}
