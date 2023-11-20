using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace WpfApp4
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        private UserModel _user;
        private bool _isRegistrationSuccessful;

        public UserModel User
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }
        public bool IsRegistrationSuccessful
        {
            get { return _isRegistrationSuccessful; }
            set
            {
                _isRegistrationSuccessful = value;
                OnPropertyChanged(nameof(IsRegistrationSuccessful));
            }
        }

        public ICommand RegisterCommand { get; private set; }

        public RegisterViewModel()
        {
            User = new UserModel();
            RegisterCommand = new RelayCommand(Register, CanRegister);
        }

        private void Register(object parameter)
        {
            string connectionString = "Data Source=DESKTOP-D3VB68L;Initial Catalog=Users;Integrated Security=True";
            string username = User.Username;
            string password = User.Password;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string checkUserQuery = $"SELECT COUNT(*) FROM Users1 WHERE Username = @Username";
                    SqlCommand checkUserCommand = new SqlCommand(checkUserQuery, connection);
                    checkUserCommand.Parameters.AddWithValue("@Username", User.Username);
                    int existingUserCount = (int)checkUserCommand.ExecuteScalar();

                    if (existingUserCount > 0)
                    {
                        MessageBox.Show("Пользователь с таким именем уже существует!", "Ошибка", MessageBoxButton.OK);
                        return;
                    }

                    string insertQuery = "INSERT INTO Users1 (Username, Password, RegistrationDate) VALUES (@Username, @Password, @RegistrationDate)";
                    SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                    insertCommand.Parameters.AddWithValue("@Username", User.Username);
                    insertCommand.Parameters.AddWithValue("@Password", User.Password);
                    insertCommand.Parameters.AddWithValue("@RegistrationDate", DateTime.Now);

                    insertCommand.ExecuteNonQuery();

                    MessageBox.Show("Готово! Пользователь зарегистрирован.");
                    LoginWindow LoginWindow = new LoginWindow();
                    LoginWindow.Show();
                    Application.Current.Windows[0].Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}", "Ошибка", MessageBoxButton.OK);
            }
        }

        private bool CanRegister(object parameter)
        {
            return !string.IsNullOrWhiteSpace(User.Username) && !string.IsNullOrWhiteSpace(User.Password);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void OpenRegisterWindow()
        {
            try
            {
                RegisterWindow registerWindow = new RegisterWindow();
                registerWindow.Show();
            }
            catch (Exception ex)
            {
               MessageBox.Show($"Ошибка при открытии окна регистрации: {ex.Message}");
                // Или используйте другой механизм логирования
            }
        }
    }
}
