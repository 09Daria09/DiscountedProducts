using System;
using System.Data.SqlClient;
using Dapper;
using System.Windows;

namespace DiscountedProducts
{
    public partial class AddProductInterestsWin : Window
    {
        private string connectionString;

        public AddProductInterestsWin(string connectionString)
        {
            this.connectionString = connectionString;
            InitializeComponent();
        }

        private void AddInterest_Click(object sender, RoutedEventArgs e)
        {
            var interestName = InterestNameBox.Text.Trim();

            if (string.IsNullOrEmpty(interestName))
            {
                MessageBox.Show("Пожалуйста, введите название интереса.");
                return;
            }

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    var sql = "INSERT INTO ProductInterests (InterestName) VALUES (@InterestName)";
                    var affectedRows = connection.Execute(sql, new { InterestName = interestName });

                    if (affectedRows > 0)
                    {
                        MessageBox.Show("Интерес успешно добавлен.");
                        InterestNameBox.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Не удалось добавить интерес.");
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
