using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using System.Data.SqlClient;
using System;
using WpfApp4.View;
using System.Windows.Media;
using System.Collections.ObjectModel;
using System.IO;

namespace WpfApp4
{

    public class LoginViewModel : INotifyPropertyChanged
    {
        private UserModel _user; // Добавление объекта UserModel
        private RegisterViewModel _registerViewModel;
        private string _selectedTheme;

        public string SelectedTheme
        {
            get { return _selectedTheme; }
            set
            {
                if (_selectedTheme != value)
                {
                    _selectedTheme = value;
                    OnPropertyChanged(nameof(SelectedTheme));
                    ChangeTheme(_selectedTheme);
                }
            }
        }

        public UserModel User // Доступ к объекту UserModel
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }


        public ICommand RegisterCommand { get; private set; }
        public ICommand LoginCommand { get; private set; }
        public ICommand ClickCommand { get; private set; }
        public ICommand ChangeThemeCommand { get; private set; }
        public ObservableCollection<string> Styles { get; private set; }

        public LoginViewModel()
        {
            _registerViewModel = new RegisterViewModel();
            User = new UserModel();
            RegisterCommand = new RelayCommand(Register, CanRegister);
            LoginCommand = new RelayCommand(Login, CanLogin);
            ClickCommand = new RelayCommand(Switch);
            ChangeThemeCommand = new RelayCommand(obj => ChangeTheme(obj as string));
            Styles = new ObservableCollection<string> { "Light", "Dark" };
        }
        private void Register(object parameter)
        {
            // Используем _registerViewModel для выполнения регистрации
            _registerViewModel.RegisterCommand.Execute(null);

            // После регистрации, проверяем, была ли регистрация успешной
            if (_registerViewModel.IsRegistrationSuccessful)
            {
                // Регистрация была успешной, создаем новое окно логина
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.DataContext = new LoginViewModel(); // или установите существующий экземпляр LoginViewModel
                loginWindow.Show();
            }
        }

        private void Login(object parameter)
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-D3VB68L;Initial Catalog=Users;Integrated Security=True";
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        string loginQuary = "SELECT COUNT(*) FROM Users1 WHERE Username = @Username AND Password = @Password";
                        SqlCommand loginCommand = new SqlCommand(loginQuary, connection);
                        loginCommand.Parameters.AddWithValue("@Username", User.Username);
                        loginCommand.Parameters.AddWithValue("@Password", User.Password);

                        int userCount = (int)loginCommand.ExecuteScalar();

                        if (userCount > 0)
                        {
                            MessageBox.Show("Вход успешно выполнен!");
                            DataWindow DataWindow = new DataWindow();
                            DataWindow.Show();
                            Application.Current.Windows[0].Close();
                        }
                        else
                        {
                            MessageBox.Show("Ошибка входа. Проверьте логин и пароль.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при выполнении входа: {ex.Message}");
            }

        }

        private void Switch(object parameter)
        {
            // Закрываем текущее окно логина


            // Открываем окно регистрации
            _registerViewModel.OpenRegisterWindow();
            CloseLoginWindow();
        }

        private bool CanRegister(object parameter)
        {
            return !string.IsNullOrWhiteSpace(User.Username) && !string.IsNullOrWhiteSpace(User.Password);
        }

        private bool CanLogin(object parameter)
        {
            return !string.IsNullOrWhiteSpace(User.Username) && !string.IsNullOrWhiteSpace(User.Password);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void CloseLoginWindow()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();  // Используйте window.Close() вместо window.Hide()
                    break;
                }
            }
        }
        private void ChangeTheme(object parameter)
        {
            string style = parameter as string;
            ((App)Application.Current).ChangeTheme(style);
        }

    }
}


