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
            ImportDatasets(@"C:\Users\Selin\Desktop\dash\trending.txt", "TrendingBooks");
            //ImportDatasets(@"C:\Users\Selin\Desktop\dash\popular.txt", "AllTimesPopular");
            ImportDatasets(@"C:\Users\Selin\Desktop\dash\books.txt", "Books");
        }
        private void GoToDashboard_Click(object sender, RoutedEventArgs e)
        {
            var dashboardWindow = new Dashboard();
            dashboardWindow.Show();

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
            bookListBox.Items.Clear();
            string connectionString = "Data Source = books.db;Version=3";
            
            using ( var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string selectQuery = "SELECT Title FROM AllTimesPopular";
                using (var command = new SQLiteCommand(selectQuery, connection)) { 
                    using(var reader = command.ExecuteReader())
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
        public void ImportDatasets(string filePath, string tableName)
        {
            string connectionString = @"Data Source=books.db;Version=3;";

            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Create table if not exists
                    string createTableQuery = $@"CREATE TABLE IF NOT EXISTS {tableName} (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Title TEXT,
                        Author TEXT,
                        PageNumber INTEGER
                        )";
                    using (var tableCommand = new SQLiteCommand(createTableQuery, connection))
                    {
                        tableCommand.ExecuteNonQuery();
                    }

                    // Read and insert data
                    string[] lines = File.ReadAllLines(filePath);
                    foreach (string line in lines)
                    {
                        string[] parts = line.Split('|');
                        if (parts.Length == 3)
                        {
                            string title = parts[0].Trim();
                            string author = parts[1].Trim();
                            int pageNumber = int.TryParse(parts[2].Trim(), out int pn) ? pn : 0;

                            string insertQuery = $@"INSERT INTO {tableName} (Title, Author, PageNumber)
                                           VALUES (@Title, @Author, @PageNumber)";
                            using (var command = new SQLiteCommand(insertQuery, connection))
                            {
                                command.Parameters.AddWithValue("@Title", title);
                                command.Parameters.AddWithValue("@Author", author);
                                command.Parameters.AddWithValue("@PageNumber", pageNumber);
                                command.ExecuteNonQuery();
                            }
                        }
                    }

                    MessageBox.Show($"{tableName} imported successfully.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error importing {tableName}: {ex.Message}");
            }
        }
    }
}
