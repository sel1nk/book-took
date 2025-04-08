
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SQLite;
using System.IO;

namespace dash
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DeleteAllBooks("Books");
            DeleteAllBooks("TrendingBooks");
            DeleteAllBooks("AllTimesPopular");
            DeleteAllBooks("PopularCharacters");
            ImportDatasets(@"C:\Users\Selin\Desktop\dash\popular.txt", "AllTimesPopular", true);
            ImportDatasets(@"C:\Users\Selin\Desktop\dash\popularCharacters.txt", "PopularCharacters", false);
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


        public void ImportDatasets(string filePath, string tableName, bool isBook)
        {
            string connectionString = @"Data Source=books.db;Version=3;";

            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    if (isBook)
                    {
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
                    }
                    else
                    {
                        string createTableQuery = $@"CREATE TABLE IF NOT EXISTS {tableName} (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT,
                        Title TEXT,
                        Author TEXT
                        )";
                        using (var tableCommand = new SQLiteCommand(createTableQuery, connection))
                        {
                            tableCommand.ExecuteNonQuery();
                        }
                    }
                        // Read and insert data
                        string[] lines = File.ReadAllLines(filePath);
                    foreach (string line in lines)
                    {
                        string[] parts = line.Split('|');
                        if (parts.Length == 3) { 
                            if (isBook)
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
                            } else
                            {
                                string name = parts[0].Trim();
                                string title = parts[1].Trim();
                                string author = parts[2].Trim();

                                string insertQuery = $@"INSERT INTO {tableName} (Name, Title, Author)
                                           VALUES (@Name, @Title, @Author)";
                                using (var command = new SQLiteCommand(insertQuery, connection))
                                {
                                    command.Parameters.AddWithValue("@Name", name);
                                    command.Parameters.AddWithValue("@Title", title);
                                    command.Parameters.AddWithValue("@Author", author);
                                    command.ExecuteNonQuery();
                                }
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


        
        public void DeleteAllBooks(string tableName)
        {
            string connectionString = @"Data Source=books.db;Version=3;";

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Delete all records from the Books table
                string deleteQuery = $"DELETE FROM {tableName}";

                using (var command = new SQLiteCommand(deleteQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
                MessageBox.Show("All records deleted successfully.");
            }
        }

       
        private void ShowPopularTitles()
        {
            bookListBox.Items.Clear(); // Clear the list before showing new titles
            charactersListBox.Items.Clear();
            string connectionString = "Data Source=books.db;Version=3;";
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string selectPopularTitlesQuery = "SELECT Title FROM AllTimesPopular";
                string selectPopularCharactersQuery = "SELECT Name FROM PopularCharacters";
                using (var command = new SQLiteCommand(selectPopularTitlesQuery, connection))
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
                using (var command = new SQLiteCommand(selectPopularCharactersQuery, connection))
                {
                    using (var reader2 = command.ExecuteReader())
                    {
                        while (reader2.Read())
                        {
                            string name = reader2.GetString(0);
                            charactersListBox.Items.Add(name); // Add to the ListBox
                        }
                    }
                }
            }
        }

      





    }
}
