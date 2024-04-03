using Dapper;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DiscountedProducts
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string connectionString;
        public MainWindow()
        {
            InitializeComponent();

            var builder = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();
            connectionString = configuration.GetConnectionString("DefaultConnection");
            Loaded += Window_Loaded;

            ShowAllCustomers.Click += ShowAllCustomers_Click;
            ShowCustomerEmails.Click += ShowCustomerEmails_Click;
            ShowSections.Click += ShowSections_Click;
            ShowSpecialOffers.Click += ShowSpecialOffers_Click;
            ShowAllCities.Click += ShowAllCities_Click;
            ShowAllCountries.Click += ShowAllCountries_Click;

            InsertNewCustomers.Click += InsertNewCustomers_Click;
            InsertNewCountries.Click += InsertNewCountries_Click;
            InsertNewCities.Click += InsertNewCities_Click;
            InsertNewSections.Click += InsertNewSections_Click;
            InsertNewSpecialOffers.Click += InsertNewSpecialOffers_Click;

            UpdateCity.Click += UpdateCity_Click;
            UpdateCountry.Click += UpdateCountry_Click;
            UpdateCustomer.Click += UpdateCustomer_Click;
        }
        private void UpdateCity_Click(object sender, RoutedEventArgs e)
        {
            var win = new EditCity(connectionString);
            win.ShowDialog();
        }

        private void UpdateCountry_Click(object sender, RoutedEventArgs e)
        {
            var win = new EditCountry(connectionString);
            win.ShowDialog();
        }

        private void UpdateCustomer_Click(object sender, RoutedEventArgs e)
        {
            var win = new EditUser(connectionString);
            win.ShowDialog();
        }
        private void InsertNewCustomers_Click(object sender, RoutedEventArgs e)
        {
            var CustWin = new AddCustomersWin(connectionString);
            CustWin.ShowDialog();
        }

        private void InsertNewCountries_Click(object sender, RoutedEventArgs e)
        {
            var CountryWin = new AddCountries(connectionString);
            CountryWin.ShowDialog();
        }

        private void InsertNewCities_Click(object sender, RoutedEventArgs e)
        {
            var Win = new AddCityWin(connectionString);
            Win.ShowDialog();
        }

        private void InsertNewSections_Click(object sender, RoutedEventArgs e)
        {
            var Win = new AddProductInterestsWin(connectionString);
            Win.ShowDialog();
        }

        private void InsertNewSpecialOffers_Click(object sender, RoutedEventArgs e)
        {
            var Win = new AddSpecialOffersWin(connectionString);
            Win.ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCitiesMenu();
            LoadCountriesMenu();
            LoadCountriesForOffersMenu();
        }
        private void LoadCitiesMenu()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var cities = connection.Query<City>("SELECT CityID, CityName FROM Cities").ToList();
                CustomersByCity.Items.Clear();
                foreach (var city in cities)
                {
                    var cityItem = new MenuItem { Header = city.CityName, Tag = city.CityID };
                    cityItem.Click += CityItem_Click;
                    CustomersByCity.Items.Add(cityItem);
                }
            }
        }


        private void LoadCountriesMenu()
        {
            CustomersByCountry.Items.Clear(); 

            using (var connection = new SqlConnection(connectionString))
            {
                var countries = connection.Query<Country>("SELECT CountryID, CountryName FROM Countries").ToList();
                foreach (var country in countries)
                {
                    var countryItem = new MenuItem { Header = country.CountryName, Tag = country.CountryID };
                    countryItem.Click += CountryItem_Click;
                    CustomersByCountry.Items.Add(countryItem);
                }
            }
        }

        private void LoadCountriesForOffersMenu()
        {
            OffersByCountry.Items.Clear(); 

            using (var connection = new SqlConnection(connectionString))
            {
                var countries = connection.Query<Country>("SELECT CountryID, CountryName FROM Countries").ToList();
                foreach (var country in countries)
                {
                    var countryItem = new MenuItem { Header = country.CountryName, Tag = country.CountryID };
                    countryItem.Click += SpecialOffersByCountryItem_Click;
                    OffersByCountry.Items.Add(countryItem);
                }
            }
        }

        private void SpecialOffersByCountryItem_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            var countryId = (int)menuItem.Tag;

            using (var connection = new SqlConnection(connectionString))
            {
                var query = @"
            SELECT so.PromotionID, so.InterestID, so.StartDate, so.EndDate, so.PromotionDetails, co.CountryName
            FROM SpecialOffers so
            JOIN Countries co ON so.CountryID = co.CountryID
            WHERE so.CountryID = @CountryId";
                var specialOffers = connection.Query(query, new { CountryId = countryId }).Select(so => new
                {
                    so.PromotionID,
                    so.InterestID,
                    StartDate = so.StartDate.ToString("yyyy-MM-dd"),
                    EndDate = so.EndDate.ToString("yyyy-MM-dd"),
                    so.PromotionDetails,
                    so.CountryName
                }).ToList();

                dataGrid.ItemsSource = specialOffers;
            }
        }


        private void CityItem_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            var cityId = (int)menuItem.Tag;

            using (var connection = new SqlConnection(connectionString))
            {
                var query = @"
            SELECT up.CustomerID, up.FullName, up.BirthDate, up.Gender, up.Email, c.CityName
            FROM UserProfiles up
            JOIN Cities c ON up.CityID = c.CityID
            WHERE c.CityID = @CityId";
                var userProfiles = connection.Query(query, new { CityId = cityId }).ToList();

                dataGrid.ItemsSource = userProfiles.Select(up => new
                {
                    up.CustomerID,
                    up.FullName,
                    BirthDate = up.BirthDate.ToString("yyyy-MM-dd"), 
                    up.Gender,
                    up.Email,
                    up.CityName
                }).ToList();
            }
        }



        private void CountryItem_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            var countryId = (int)menuItem.Tag;

            using (var connection = new SqlConnection(connectionString))
            {
                var query = @"
            SELECT up.CustomerID, up.FullName, up.BirthDate, up.Gender, up.Email, c.CityName, co.CountryName
            FROM UserProfiles up
            JOIN Cities c ON up.CityID = c.CityID
            JOIN Countries co ON c.CountryID = co.CountryID
            WHERE co.CountryID = @CountryId";
                var userProfiles = connection.Query(query, new { CountryId = countryId }).ToList();

                dataGrid.ItemsSource = userProfiles.Select(up => new
                {
                    up.CustomerID,
                    up.FullName,
                    BirthDate = up.BirthDate.ToString("yyyy-MM-dd"), 
                    up.Gender,
                    up.Email,
                    up.CityName,
                    up.CountryName
                }).ToList();
            }
        }


        private void ShowAllCustomers_Click(object sender, RoutedEventArgs e)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var query = @"
            SELECT up.CustomerID, up.FullName, up.Email, c.CityName, co.CountryName
            FROM UserProfiles up
            JOIN Cities c ON up.CityID = c.CityID
            JOIN Countries co ON c.CountryID = co.CountryID";
                var userProfiles = connection.Query(query).Select(x => new {
                    CustomerID = x.CustomerID, 
                    FullName = x.FullName,
                    Email = x.Email,
                    CityName = x.CityName,
                    CountryName = x.CountryName
                }).ToList();
                dataGrid.ItemsSource = userProfiles;
            }
            dataGrid.Name = "CustomersDataGrid";
        }


        private void ShowCustomerEmails_Click(object sender, RoutedEventArgs e)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var customerEmails = connection.Query<string>("SELECT Email FROM UserProfiles").ToList();
                dataGrid.ItemsSource = customerEmails.Select(email => new { Email = email }).ToList();
            }
        }
        private void ShowSections_Click(object sender, RoutedEventArgs e)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var productInterests = connection.Query<ProductInterest>("SELECT * FROM ProductInterests").ToList();
                dataGrid.ItemsSource = productInterests;
            }
            dataGrid.Name = "SectionsDataGrid";
        }
        private void ShowSpecialOffers_Click(object sender, RoutedEventArgs e)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var query = @"
        SELECT so.PromotionDetails, pi.InterestName, co.CountryName, so.StartDate, so.EndDate
        FROM SpecialOffers so
        JOIN ProductInterests pi ON so.InterestID = pi.InterestID
        JOIN Countries co ON so.CountryID = co.CountryID;";

                var specialOffers = connection.Query(query).Select(so => new
                {
                    PromotionDetails = so.PromotionDetails,
                    InterestName = so.InterestName,
                    CountryName = so.CountryName,
                    StartDate = so.StartDate.ToString("yyyy-MM-dd"),
                    EndDate = so.EndDate.ToString("yyyy-MM-dd")
                }).Distinct().ToList();

                dataGrid.ItemsSource = specialOffers;
            }
            dataGrid.Name = "SpecialOffersDataGrid";
        }


        private void ShowAllCities_Click(object sender, RoutedEventArgs e)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var query = @"
            SELECT c.CityID, c.CityName, cn.CountryName 
            FROM Cities c
            JOIN Countries cn ON c.CountryID = cn.CountryID;";
                var cities = connection.Query(query).Select(x => new
                {
                    CityID = x.CityID,
                    CityName = x.CityName,
                    CountryName = x.CountryName
                }).ToList();
                dataGrid.ItemsSource = cities;
            }
            dataGrid.Name = "CitiesDataGrid";
        }


        private void ShowAllCountries_Click(object sender, RoutedEventArgs e)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var countries = connection.Query<Country>("SELECT * FROM Countries").ToList();
                dataGrid.ItemsSource = countries;
                dataGrid.Name = "CountriesDataGrid";
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Обработка изменения выбора, если необходимо.
        }

        private void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem == null) return;

            var result = MessageBox.Show("Вы уверены, что хотите удалить этот объект?", "Подтверждение удаления", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    var item = dataGrid.SelectedItem;
                    DeleteItemFromDatabase(item, dataGrid.Name);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении объекта: {ex.Message}");
                }
            }
        }

        private void DeleteItemFromDatabase(dynamic item, string dataGridName)
        {
            string tableName = "";
            string primaryKeyColumn = "";
            object primaryKeyValue = null;

            switch (dataGridName)
            {
                case "CustomersDataGrid":
                    tableName = "UserProfiles";
                    primaryKeyColumn = "CustomerID";
                    primaryKeyValue = item.CustomerID;
                    break;
                case "CountriesDataGrid":
                    tableName = "Countries";
                    primaryKeyColumn = "CountryID";
                    primaryKeyValue = item.CountryID;
                    break;
                case "CitiesDataGrid":
                    tableName = "Cities";
                    primaryKeyColumn = "CityID";
                    primaryKeyValue = item.CityID;
                    break;
                case "SectionsDataGrid":
                    tableName = "ProductInterests";
                    primaryKeyColumn = "InterestID";
                    primaryKeyValue = item.InterestID;
                    break;
                case "SpecialOffersDataGrid":
                    tableName = "SpecialOffers";
                    primaryKeyColumn = "PromotionID";
                    primaryKeyValue = item.PromotionID;
                    break;
            }

            if (!string.IsNullOrEmpty(tableName) && primaryKeyValue != null)
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    var deleteSql = $"DELETE FROM {tableName} WHERE {primaryKeyColumn} = @PrimaryKeyValue";
                    connection.Execute(deleteSql, new { PrimaryKeyValue = primaryKeyValue });
                    MessageBox.Show("Запись успешно удалена.");
                    RefreshDataGrid(dataGridName);
                }
            }
            else
            {
                MessageBox.Show("Не удалось определить таблицу или ключ для удаления.");
            }
        }

        private void RefreshDataGrid(string dataGridName)
        {
            switch (dataGridName)
            {
                case "CustomersDataGrid":
                    ShowAllCustomers_Click(null, null); 
                    break;
                case "CountriesDataGrid":
                    ShowAllCountries_Click(null, null);
                    break;
                case "CitiesDataGrid":
                    ShowAllCities_Click(null, null);
                    break;
                case "SectionsDataGrid":
                    ShowSections_Click(null, null);
                    break;
                case "SpecialOffersDataGrid":
                    ShowSpecialOffers_Click(null, null);
                    break;
            }
        }



    }
}