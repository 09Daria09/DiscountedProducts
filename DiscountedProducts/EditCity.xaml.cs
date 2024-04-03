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
    /// Interaction logic for EditCity.xaml
    /// </summary>
    public partial class EditCity : Window
    {
        private string connectionString;
        public EditCity(string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            LoadCities();
        }

        private void LoadCities()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var cities = connection.Query<City>("SELECT CityID, CityName FROM Cities").ToList();
                CitiesComboBox.ItemsSource = cities;
                CitiesComboBox.DisplayMemberPath = "CityName";
                CitiesComboBox.SelectedValuePath = "CityID";
            }
        }

        private void SaveCityChangesButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedCity = CitiesComboBox.SelectedItem as City;
            if (selectedCity == null)
            {
                MessageBox.Show("Пожалуйста, выберите город.");
                return;
            }

            var newCityName = NewCityNameTextBox.Text.Trim();
            if (string.IsNullOrEmpty(newCityName))
            {
                MessageBox.Show("Пожалуйста, введите новое название города.");
                return;
            }

            using (var connection = new SqlConnection(connectionString))
            {
                var sql = "UPDATE Cities SET CityName = @NewName WHERE CityID = @ID";
                connection.Execute(sql, new { NewName = newCityName, ID = selectedCity.CityID });
            }

            MessageBox.Show("Название города успешно обновлено.");
            LoadCities(); 
            NewCityNameTextBox.Clear();
        }
    }

}
