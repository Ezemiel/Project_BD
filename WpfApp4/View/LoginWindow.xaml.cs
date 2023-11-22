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
using System.Windows.Shapes;
using System.IO;

namespace WpfApp4
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            DataContext = new LoginViewModel();
            //List<string> styles = new List<string> { "Light", "Dark" };
            //styleBox.SelectionChanged += ThemeChange;
            //styleBox.ItemsSource = styles;
            //styleBox.SelectedItem = "Dark";
        }
        private void Window_click(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        //private void ThemeChange(object sender, SelectionChangedEventArgs e)
        //{
        //    string style = styleBox.SelectedItem as string;
        //    string themeFilePath = @"C:\Users\admin\Documents\УП\Project\WpfApp4\Themes\" + style + ".xaml";

        //    if (File.Exists(themeFilePath))
        //    {
        //        var uri = new Uri(themeFilePath, UriKind.Absolute);
        //        ResourceDictionary resourceDict = new ResourceDictionary();
        //        resourceDict.Source = uri;

        //        Application.Current.Resources.Clear();
        //        Application.Current.Resources.MergedDictionaries.Add(resourceDict);
        //    }
        //    else
        //    {
        //        MessageBox.Show("Тема не найдена");
        //    }
        //}
    }
}
