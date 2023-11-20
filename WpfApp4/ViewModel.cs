using System;
using System.ComponentModel;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace WpfApp4
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _username;
        private string _password;

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public ICommand RegisterCommand { get; private set; }

        public LoginViewModel()
        {
            RegisterCommand = new RelayCommand(Register, CanRegister);
        }

        private void Register(object parameter)
        {
            string connectionString = "Data Source=DESKTOP-D3VB68L;Initial Catalog=Users;Integrated Security=True";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Проверяем, не существует ли уже пользователь с таким именем
                    string checkUserQuery = $"SELECT COUNT(*) FROM Users1 WHERE Username = @Username";
                    SqlCommand checkUserCommand = new SqlCommand(checkUserQuery, connection);
                    checkUserCommand.Parameters.AddWithValue("@Username", Username);
                    int existingUserCount = (int)checkUserCommand.ExecuteScalar();

                    if (existingUserCount > 0)
                    {
                        MessageBox.Show("Пользователь с таким именем уже существует!");
                        return;
                    }

                    // Используем параметризованный SQL запрос для вставки нового пользователя
                    string insertQuery = "INSERT INTO Users1 (Username, Password, RegistrationDate) VALUES (@Username, @Password, @RegistrationDate)";
                    SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                    insertCommand.Parameters.AddWithValue("@Username", Username);
                    insertCommand.Parameters.AddWithValue("@Password", Password);
                    insertCommand.Parameters.AddWithValue("@RegistrationDate", DateTime.Now);

                    insertCommand.ExecuteNonQuery();

                    MessageBox.Show("Готово! Пользователь зарегистрирован.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}");
            }
        }







        private bool CanRegister(object parameter)
        {
            return !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
