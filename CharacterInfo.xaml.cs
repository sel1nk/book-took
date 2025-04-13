using System;
using System.Collections.Generic;
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
using System.Data.SQLite;
using System.IO;
using System.Xml.Linq;

namespace dash
{
    /// <summary>
    /// Interaction logic for CharacterInfo.xaml
    /// </summary>
    public partial class CharacterInfo : Window
    {
        private string characterName;
        public CharacterInfo(string chName)
        {
            InitializeComponent();
            characterName = chName;
        }

        private void GoToDashboard_Click(object sender, RoutedEventArgs e)
        {
            var dashboardWindow = new Dashboard();
            dashboardWindow.Show();

            // Close the current window (MainWindow)
            this.Close();
        }
        private void GoToCategories_Click(object sender, RoutedEventArgs e)
        {
            var categoriesWindow = new Categories();
            categoriesWindow.Show();

            // Close the current window (MainWindow)
            this.Close();
        }
        private void GoToHome_Click(object sender, RoutedEventArgs e)
        {
            var homeWindow = new MainWindow();
            homeWindow.Show();

            // Close the current window (MainWindow)
            this.Close();
        }

        private void ShowPopularTitles()
        {
            //characterInfoBox.Items.Clear(); // Clear the list before showing new titles
            //characterBox.Items.Clear();
            //characterImageBox.Items.Clear();
            //string connectionString = "Data Source=books.db;Version=3;";
            //using (var connection = new SQLiteConnection(connectionString))
            //{
            //    connection.Open();
            //    string selectCharacterNameQuery = "SELECT Name FROM AllTimesPopular";
            //    string selectCharacterBookQuery = "SELECT Book FROM AllTimesPopular";
            //    string selectAuthorQuery = "SELECT Author FROM AllTimesPopular";
            //    string selectDescriptionQuery = "SELECT Description FROM AllTimesPopular";
            //    string selectPowersQuery = "SELECT Name Powers AllTimesPopular";
            //    string selectStrengthQuery = "SELECT Strength FROM AllTimesPopular";
            //    using (var command = new SQLiteCommand(selectCharacterNameQuery, connection))
            //    {
            //        using (var reader = command.ExecuteReader())
            //        {
            //            while (reader.Read())
            //            {
            //                string title = reader.GetString(0);
            //                characterInfoBox.Items.Add(title); // Add to the ListBox
            //            }
            //        }
            //    }
            //    using (var command = new SQLiteCommand(selectCharacterBookQuery, connection))
            //    {
            //        using (var reader2 = command.ExecuteReader())
            //        {
            //            while (reader2.Read())
            //            {
            //                string name = reader2.GetString(0);
            //                characterInfoBox.Items.Add(name); // Add to the ListBox
            //            }
            //        }
            //    }
            //}
        }
    }
}
