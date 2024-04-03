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
    /// Interaction logic for EditCountry.xaml
    /// </summary>
    public partial class EditCountry : Window
    {
        private string connectionString;
        public EditCountry(string connectionString) 
        {
            InitializeComponent();
            this.connectionString = connectionString;
            LoadCountries();
        }

        private void LoadCountries()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var countries = connection.Query<Country>("SELECT CountryID, CountryName FROM Countries").ToList();
                CountriesComboBox.ItemsSource = countries;
            }
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedCountry = CountriesComboBox.SelectedItem as Country;
            if (selectedCountry == null)
            {
                MessageBox.Show("Пожалуйста, выберите страну.");
                return;
            }

            var newCountryName = NewCountryNameTextBox.Text.Trim();
            if (string.IsNullOrEmpty(newCountryName))
            {
                MessageBox.Show("Пожалуйста, введите новое название страны.");
                return;
            }

            using (var connection = new SqlConnection(connectionString))
            {
                var sql = "UPDATE Countries SET CountryName = @NewName WHERE CountryID = @ID";
                connection.Execute(sql, new { NewName = newCountryName, ID = selectedCountry.CountryID });
            }

            MessageBox.Show("Название страны успешно обновлено.");
            LoadCountries(); 
            NewCountryNameTextBox.Clear(); 
        }

    }
}
