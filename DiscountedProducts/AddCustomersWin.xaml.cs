using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace DiscountedProducts
{
    /// <summary>
    /// Interaction logic for AddCustomersWin.xaml
    /// </summary>
    public partial class AddCustomersWin : Window
    {
        private string connectionString;

        public AddCustomersWin(string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            LoadCitiesIntoComboBox();
        }
        private IEnumerable<City> LoadCities()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query<City>("SELECT CityID, CityName FROM Cities").ToList();
            }
        }

        private void LoadCitiesIntoComboBox()
        {
            var cities = LoadCities();
            CityComboBox.ItemsSource = cities;
            CityComboBox.DisplayMemberPath = "CityName"; 
            CityComboBox.SelectedValuePath = "CityID"; 
        }

        private void AddCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FullNameBox.Text))
            {
                MessageBox.Show("Пожалуйста, введите полное имя.");
                return;
            }
            if (!BirthDatePicker.SelectedDate.HasValue)
            {
                MessageBox.Show("Пожалуйста, выберите дату рождения.");
                return;
            }
            if (GenderBox.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, выберите пол.");
                return;
            }
            if (string.IsNullOrWhiteSpace(EmailBox.Text))
            {
                MessageBox.Show("Пожалуйста, введите адрес электронной почты.");
                return;
            }
            if (CityComboBox.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, выберите город.");
                return;
            }

            try
            {
                var newCustomer = new UserProfile
                {
                    FullName = FullNameBox.Text,
                    BirthDate = BirthDatePicker.SelectedDate.Value,
                    Gender = ((ComboBoxItem)GenderBox.SelectedItem)?.Content.ToString(),
                    Email = EmailBox.Text,
                    CityID = Convert.ToInt32(CityComboBox.SelectedValue)
                };

                AddCustomerToDatabase(newCustomer);
                MessageBox.Show("Покупатель успешно добавлен!");

                FullNameBox.Text = "";
                BirthDatePicker.SelectedDate = null;
                GenderBox.SelectedItem = null;
                EmailBox.Text = "";
                CityComboBox.SelectedItem = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось добавить покупателя: {ex.Message}");
            }
        }


        private void AddCustomerToDatabase(UserProfile customer)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var sql = "INSERT INTO UserProfiles (FullName, BirthDate, Gender, Email, CityID) VALUES (@FullName, @BirthDate, @Gender, @Email, @CityID)";
                connection.Execute(sql, new
                {
                    FullName = customer.FullName,
                    BirthDate = customer.BirthDate,
                    Gender = customer.Gender,
                    Email = customer.Email,
                    CityID = customer.CityID
                });
            }
        }

    }
}
