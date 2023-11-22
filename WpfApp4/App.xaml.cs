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
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Создаем экземпляр ViewModel
            var viewModel = new LoginViewModel();

            // Создаем главное окно и устанавливаем ViewModel как контекст данных
            var mainWindow = new LoginWindow { DataContext = viewModel };
            mainWindow.Show();
        }

        public void ChangeTheme(string style)
        {
            string themeFilePath = @"C:\Users\admin\Documents\УП\Project\WpfApp4\Themes\" + style + ".xaml";

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

