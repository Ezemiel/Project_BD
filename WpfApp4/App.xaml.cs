using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

namespace WpfApp4
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public void ChangeTheme(string style)
        {
            string themeFilePath = @"C:\Users\katrovskiiEM\Documents\Project\WpfApp4\Themes\" + style + ".xaml";

            if (File.Exists(themeFilePath))
            {
                var uri = new Uri(themeFilePath, UriKind.Absolute);
                ResourceDictionary resourceDict = new ResourceDictionary();
                resourceDict.Source = uri;

                Application.Current.Resources.Clear();
                Application.Current.Resources.MergedDictionaries.Add(resourceDict);
            }
            else
            {
                MessageBox.Show("Тема не найдена");
            }
        }
    }
}

