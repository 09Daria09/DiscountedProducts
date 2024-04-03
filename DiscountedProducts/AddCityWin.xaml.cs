using System;
using System.Data.SqlClient;
using Dapper;
using System.Windows;

namespace DiscountedProducts
{
    public partial class AddCityWin : Window
    {
        private string connectionString;

        public AddCityWin(string connectionString)
        {
            this.connectionString = connectionString;
            InitializeComponent();
            LoadCountries();
        }

        private void LoadCountries()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var countries = connection.Query<Country>("SELECT CountryID, CountryName FROM Countries").ToList();
                CountryComboBox.ItemsSource = countries;
            }
        }

        private void AddCity_Click(object sender, RoutedEventArgs e)
        {
            var cityName = CityNameBox.Text;
            var selectedCountryId = CountryComboBox.SelectedValue;

            if (string.IsNullOrWhiteSpace(cityName))
            {
                MessageBox.Show("Пожалуйста, введите название города.");
                return;
            }

            if (selectedCountryId == null)
            {
                MessageBox.Show("Пожалуйста, выберите страну.");
                return;
            }

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    var sql = "INSERT INTO Cities (CityName, CountryID) VALUES (@CityName, @CountryID)";
                    var affectedRows = connection.Execute(sql, new { CityName = cityName, CountryID = selectedCountryId });

                    if (affectedRows > 0)
                    {
                        MessageBox.Show("Город успешно добавлен.");
                        CityNameBox.Clear();
                        CountryComboBox.SelectedIndex = -1;
                    }
                    else
                    {
                        MessageBox.Show("Не удалось добавить город.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }

    }
}
