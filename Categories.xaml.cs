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
            //initialTrendingTable();
            //DeleteTrendingBooks();
            ImportTrendingBooks(@"C:\Users\Selin\Desktop\dash\trending.txt");

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
        public void ImportBooksFromFile(string filePath)
        {
            string connectionString = @"Data Source=books.db;Version=3;";

            try
            {
                // Open SQLite connection
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Create table if it doesn't exist
                    string createTableQuery = @"CREATE TABLE IF NOT EXISTS Books (
                                            Id INTEGER PRIMARY KEY AUTOINCREMENT, 
                                            Title TEXT, 
                                            Author TEXT, 
                                            PageNumber INTEGER)";
                    using (var command = new SQLiteCommand(createTableQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    // Read the file line by line
                    string[] lines = File.ReadAllLines(filePath);

                    // Loop through each line and insert into the database
                    foreach (string line in lines)
                    {
                        // Split the line into title, author, and page number
                        string[] parts = line.Split('|');
                        if (parts.Length == 3)
                        {
                            string title = parts[0].Trim();
                            string author = parts[1].Trim();
                            int pageNumber = 0;
                            if (int.TryParse(parts[2].Trim(), out pageNumber))
                            {
                                // Insert the book into the database
                                string insertQuery = @"INSERT INTO Books (Title, Author, PageNumber) 
                                               VALUES (@Title, @Author, @PageNumber)";
                                using (var insertCommand = new SQLiteCommand(insertQuery, connection))
                                {
                                    insertCommand.Parameters.AddWithValue("@Title", title);
                                    insertCommand.Parameters.AddWithValue("@Author", author);
                                    insertCommand.Parameters.AddWithValue("@PageNumber", pageNumber);
                                    insertCommand.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Invalid page number for book: {title}");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Invalid format in line: {line}");
                        }
                    }
                    MessageBox.Show("Books successfully imported.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
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
        public void ImportTrendingBooks(string filePath)
        {
            string connectionString = @"Data Source=books.db;Version=3;";

            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Create table if not exists
                    string createTrendingTableQuery = @"CREATE TABLE IF NOT EXISTS TrendingBooks (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Title TEXT,
                        Author TEXT,
                        PageNumber INTEGER
                        )";
                    using (var trendingCommand = new SQLiteCommand(createTrendingTableQuery, connection))
                    {
                        trendingCommand.ExecuteNonQuery();
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

                            string insertQuery = @"INSERT INTO TrendingBooks (Title, Author, PageNumber)
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

                    MessageBox.Show("Trending books imported successfully.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error importing trending books: {ex.Message}");
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

    }
}
