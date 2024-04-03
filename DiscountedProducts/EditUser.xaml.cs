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
    /// Interaction logic for EditUser.xaml
    /// </summary>
    public partial class EditUser : Window
    {
        private string connectionString;
        public EditUser(string connectionString) 
        {
            InitializeComponent();
            this.connectionString = connectionString;
            LoadUsers();
            LoadCities();
            SetupGenderComboBox();
        }

        private void LoadUsers()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var users = connection.Query<UserProfile>("SELECT CustomerID, FullName, BirthDate, Gender, Email, CityID FROM UserProfiles").ToList();
                UserComboBox.ItemsSource = users;
            }
        }

        private void LoadCities()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var cities = connection.Query<City>("SELECT CityID, CityName FROM Cities").ToList();
                CityComboBox.ItemsSource = cities;
            }
        }

        private void SetupGenderComboBox()
        {
            GenderComboBox.Items.Add(new ComboBoxItem { Content = "Male", Tag = "M" });
            GenderComboBox.Items.Add(new ComboBoxItem { Content = "Female", Tag = "F" });

            GenderComboBox.SelectedValuePath = "Tag";
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = UserComboBox.SelectedItem as UserProfile;
            if (selectedUser == null) return;

            var selectedGenderItem = GenderComboBox.SelectedItem as ComboBoxItem;
            var gender = selectedGenderItem != null ? selectedGenderItem.Tag.ToString() : string.Empty;

            var newUserProfile = new UserProfile
            {
                CustomerID = selectedUser.CustomerID,
                FullName = FullNameTextBox.Text,
                BirthDate = BirthDatePicker.SelectedDate ?? default(DateTime),
                Gender = gender,
                Email = EmailTextBox.Text,
                CityID = (int)CityComboBox.SelectedValue
            };

            UpdateUserProfile(newUserProfile);
        }

        private void UpdateUserProfile(UserProfile userProfile)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var sql = "UPDATE UserProfiles SET FullName = @FullName, BirthDate = @BirthDate, Gender = @Gender, Email = @Email, CityID = @CityID WHERE CustomerID = @CustomerID";
                connection.Execute(sql, userProfile);
            }

            MessageBox.Show("Информация о пользователе успешно обновлена.");
            LoadUsers(); 
        }
    }

}
