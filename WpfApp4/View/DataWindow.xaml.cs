using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
using WpfApp4.ViewModel;
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
            ThemeService.ThemeChanged += OnThemeChanged;
        }
        private void OnThemeChanged(object sender, string style)
        {
            // Обновить тему в данном окне
            ChangeTheme(style);
        }
        private void ChangeTheme(string style)
        {
            string themeFilePath = @"C:\Users\admin\Documents\УП\Project\WpfApp4\Themes\" + style + ".xaml";

            if (File.Exists(themeFilePath))
            {
                var uri = new Uri(themeFilePath, UriKind.Absolute);
                ResourceDictionary resourceDict = new ResourceDictionary();
                resourceDict.Source = uri;

                Resources.Clear();
                Resources.MergedDictionaries.Add(resourceDict);
            }
            else
            {
                MessageBox.Show("Тема не найдена");
            }
        }
    }
}
