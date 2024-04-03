using System;
using System.Data.SqlClient;
using Dapper;
using System.Windows;

namespace DiscountedProducts
{
    public partial class AddSpecialOffersWin : Window
    {
        private string connectionString;

        public AddSpecialOffersWin(string connectionString)
        {
            this.connectionString = connectionString;
            InitializeComponent();
            LoadInterests();
            LoadCountries();
        }

        private void LoadInterests()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var interests = connection.Query("SELECT InterestID, InterestName FROM ProductInterests").ToList();
                InterestComboBox.ItemsSource = interests;
                InterestComboBox.DisplayMemberPath = "InterestName";
                InterestComboBox.SelectedValuePath = "InterestID";
            }
        }

        private void LoadCountries()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var countries = connection.Query("SELECT CountryID, CountryName FROM Countries").ToList();
                CountryComboBox.ItemsSource = countries;
                CountryComboBox.DisplayMemberPath = "CountryName";
                CountryComboBox.SelectedValuePath = "CountryID";
            }
        }

        private void AddPromotion_Click(object sender, RoutedEventArgs e)
        {
            var interestId = InterestComboBox.SelectedValue;
            var countryId = CountryComboBox.SelectedValue;
            var startDate = StartDatePicker.SelectedDate;
            var endDate = EndDatePicker.SelectedDate;
            var promotionDetails = PromotionDetailsBox.Text;

            if (interestId == null || countryId == null || startDate == null || endDate == null)
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            if (startDate > endDate)
            {
                MessageBox.Show("Дата начала должна быть раньше даты окончания.");
                return;
            }

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    var sql = @"INSERT INTO SpecialOffers (InterestID, CountryID, StartDate, EndDate, PromotionDetails)
                                VALUES (@InterestID, @CountryID, @StartDate, @EndDate, @PromotionDetails)";
                    var affectedRows = connection.Execute(sql, new
                    {
                        InterestID = interestId,
                        CountryID = countryId,
                        StartDate = startDate,
                        EndDate = endDate,
                        PromotionDetails = promotionDetails
                    });

                    if (affectedRows > 0)
                    {
                        MessageBox.Show("Специальное предложение успешно добавлено.");
                        InterestComboBox.SelectedIndex = -1;
                        CountryComboBox.SelectedIndex = -1;
                        StartDatePicker.SelectedDate = null;
                        EndDatePicker.SelectedDate = null;
                        PromotionDetailsBox.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Не удалось добавить специальное предложение.");
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
