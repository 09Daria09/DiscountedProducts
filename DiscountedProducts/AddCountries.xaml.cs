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
    /// Interaction logic for AddCountries.xaml
    /// </summary>
    public partial class AddCountries : Window
    {
        private string connectionString;
        public AddCountries(string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;
        }
        private void AddCountry_Click(object sender, RoutedEventArgs e)
        {
            var countryName = CountryNameBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(countryName))
            {
                MessageBox.Show("Пожалуйста, введите название страны.");
                return;
            }

            try
            {
                AddCountryToDatabase(countryName);
                MessageBox.Show("Страна успешно добавлена.");
                CountryNameBox.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении страны: {ex.Message}");
            }
        }

        private int AddCountryToDatabase(string countryName)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var sql = "INSERT INTO Countries (CountryName) VALUES (@CountryName); SELECT SCOPE_IDENTITY();";
                var countryId = connection.ExecuteScalar<int>(sql, new { CountryName = countryName });
                return countryId;
            }
        }

    }
}
