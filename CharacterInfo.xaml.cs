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
            ShowPopularTitles();
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
            characterBox.Items.Clear();
            characterInfoBox.Items.Clear();
            //characterBox.Items.Add(characterName);
            //characterImageBox.Items.Clear();
            string connectionString = "Data Source=books.db;Version=3;";
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string selectCharacterBoxQuery = "SELECT Name, Title, Author, Description, Powers, Strength FROM PopularCharacters WHERE Name = @name";
                
                using (var command = new SQLiteCommand(selectCharacterBoxQuery, connection))
                {
                    command.Parameters.AddWithValue("@name", characterName);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string name = reader["Name"].ToString();
                            string title = reader["Title"].ToString();
                            string author = reader["Author"].ToString();
                            string desc = reader["Description"].ToString();
                            string powers = reader["Powers"].ToString();
                            string strength = reader["Strength"].ToString();

                            string chBox = $"{name}\n{title}\nBy {author}";
                            string chInfoBox = $"{desc}\nPowers: {powers}\nStrength Level: {strength}";

                            characterBox.Items.Add(chBox);
                            characterInfoBox.Items.Add(chInfoBox);
                        }
                    }
                }
                
            }
        }
    }
}