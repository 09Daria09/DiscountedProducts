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
            SELECT up.FullName, up.Email, c.CityName, co.CountryName
            FROM UserProfiles up
            JOIN Cities c ON up.CityID = c.CityID
            JOIN Countries co ON c.CountryID = co.CountryID";
                var userProfiles = connection.Query(query).Select(x => new {
                    FullName = x.FullName,
                    Email = x.Email,
                    CityName = x.CityName,
                    CountryName = x.CountryName
                }).ToList();
                dataGrid.ItemsSource = userProfiles;
            }
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
        }


        private void ShowAllCities_Click(object sender, RoutedEventArgs e)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var query = @"
            SELECT c.CityName, cn.CountryName 
            FROM Cities c
            JOIN Countries cn ON c.CountryID = cn.CountryID;";
                var cities = connection.Query(query).Select(x => new
                {
                    CityName = x.CityName,
                    CountryName = x.CountryName
                }).ToList();
                dataGrid.ItemsSource = cities;
            }
        }


        private void ShowAllCountries_Click(object sender, RoutedEventArgs e)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var countries = connection.Query<Country>("SELECT * FROM Countries").ToList();
                dataGrid.ItemsSource = countries;
            }
        }

    }
}