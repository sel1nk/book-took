using System;
using System.Collections.Generic;
using System.IO;
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

namespace dash
{
    /// <summary>
    /// Interaction logic for Categories.xaml
    /// </summary>
    public partial class Categories : Window
    {
        public Categories()
        {
            InitializeComponent();
            ShowPopularBooks();
        }
        private void GoToDashboard_Click(object sender, RoutedEventArgs e)
        {
            var dashboardWindow = new Dashboard();
            dashboardWindow.Show();
            this.Close();
        }
        private void GoToHome_Click(object sender, RoutedEventArgs e)
        {
            var homeWindow = new MainWindow();
            homeWindow.Show();
            this.Close();
        }
       
        
        private void ShowTitlesButton_Click(object sender, RoutedEventArgs e)
        {
            bookListBox.Items.Clear(); // Clear the list before showing new titles
            string connectionString = "Data Source=books.db;Version=3;";
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string selectQuery = "SELECT Title FROM Books";
                using (var command = new SQLiteCommand(selectQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string title = reader.GetString(0);
                            bookListBox.Items.Add(title); // Add to the ListBox
                        }
                    }
                }
            }
        }
       
       
        private void TrendingButton_Click(object sender, RoutedEventArgs e)
        {
            bookListBox.Items.Clear();
            
            // Now load and display the trending books
            string connectionString = "Data Source=books.db;Version=3;";
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string selectQuery = "SELECT Title FROM TrendingBooks";
                using (var command = new SQLiteCommand(selectQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string title = reader.GetString(0);
                            bookListBox.Items.Add(title);
                        }
                    }
                }
            }
        }

        private void PopularButton_Click(object sender, RoutedEventArgs e)
        {
            ShowPopularBooks();
        }
       
        private void ShowPopularBooks()
        {
            bookListBox.Items.Clear();
            string connectionString = "Data Source = books.db;Version=3";

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string selectQuery = "SELECT Title FROM AllTimesPopular";
                using (var command = new SQLiteCommand(selectQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string title = reader.GetString(0);
                            bookListBox.Items.Add(title);
                        }
                    }
                }
            }
        }
    }
}
