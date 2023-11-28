using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp4.ViewModel;

namespace WpfApp4.View
{
    public class DataViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<UserModel> _users;
        private DataGrid _dataGrid; // Добавляем свойство DataGrid

        public ObservableCollection<UserModel> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        public ICommand LoadDataCommand { get; private set; }
        public ICommand EditCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand AddCommand { get; private set; }
        public ICommand SettingsCommand { get; private set; }

        public DataViewModel(DataGrid dataGrid) // Принимаем DataGrid в конструкторе
        {
            _dataGrid = dataGrid; // Присваиваем DataGrid

            Users = new ObservableCollection<UserModel>();
            LoadDataCommand = new RelayCommand(LoadData);
            _dataGrid = dataGrid;
            Users = new ObservableCollection<UserModel>();
            LoadDataCommand = new RelayCommand(LoadData);
            EditCommand = new RelayCommand(Edit);
            DeleteCommand = new RelayCommand(Delete);
            AddCommand = new RelayCommand(Add);
            SettingsCommand = new RelayCommand(Settings);

            LoadData(null);
        }

        private void LoadData(object parameter)
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-D3VB68L;Initial Catalog=Users;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT UserID, Username, Password, RegistrationDate FROM Users1"; // Измените запрос на выборку нужных полей
                    SqlCommand command = new SqlCommand(query, connection);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    Users.Clear();

                    foreach (DataRow row in dataTable.Rows)
                    {
                        Users.Add(new UserModel
                        {
                            UserID = Convert.ToInt32(row["UserID"]),
                            Username = row["Username"].ToString(),
                            Password = row["Password"].ToString(),
                            RegistrationDate = Convert.ToDateTime(row["RegistrationDate"])
                            // Другие свойства UserModel, если они есть
                        });
                    }

                    // Привязываем данные к DataGrid
                    _dataGrid.ItemsSource = Users;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных из БД: {ex.Message}");
            }
        }
        private void Edit(object parameter)
        {
            EditWindow editWindow = new EditWindow();
            editWindow.Show();
            Application.Current.Windows[0].Close();
        }

        private void Delete(object parameter)
        {
            // Логика открытия окна удаления
            DeleteWindow deleteWindow = new DeleteWindow();
            deleteWindow.Show();
            Application.Current.Windows[0].Close();
        }
        private void Add(object parameter)
        {
            AddWindow register = new AddWindow();
            register.Show();
            Application.Current.Windows[0].Close();
        }

        private void Settings(object parameter)
        {
            SettingsWindow settings = new SettingsWindow();
            settings.Show();
            Application.Current.Windows[0].Close();
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void ThemeChange(string style)
        {
            ThemeService.ChangeTheme(style);
        }
    }

}
