
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
            InitializeDatabase();
            DeleteAllBooks();
            string filePath = @"C:\Users\Selin\Desktop\dash\books.txt";
            ImportBooksFromFile(filePath);
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



public void InitializeDatabase()
    {
        string connectionString = "Data Source=books.db;Version=3;";
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            string createTableQuery = "CREATE TABLE IF NOT EXISTS Books (Id INTEGER PRIMARY KEY AUTOINCREMENT, Title TEXT, Author TEXT, PageNumber INTEGER)";
            using (var command = new SQLiteCommand(createTableQuery, connection))
            {
                command.ExecuteNonQuery();
            }
        }
            string currentDirectory = Directory.GetCurrentDirectory();
            MessageBox.Show("Current directory: " + currentDirectory);

        }
        public void DeleteAllBooks()
        {
            string connectionString = @"Data Source=books.db;Version=3;";

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Delete all records from the Books table
                string deleteQuery = "DELETE FROM Books";

                using (var command = new SQLiteCommand(deleteQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
                MessageBox.Show("All records deleted successfully.");
            }
        }

        public void AddBook(string title, string author, int pageNumber)
        {
            string connectionString = "Data Source=books.db;Version=3;";
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string insertQuery = "INSERT INTO Books (Title, Author, PageNumber) VALUES (@Title, @Author, @PageNumber)";
                using (var command = new SQLiteCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@Title", title);
                    command.Parameters.AddWithValue("@Author", author);
                    command.Parameters.AddWithValue("@PageNumber", pageNumber);
                    command.ExecuteNonQuery();
                }
            }
        }
        public void DisplayBookTitles()
        {
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
                            // Display the title. You can update a ListBox or TextBlock in your UI
                            MessageBox.Show(title); // For now, just show in a message box
                        }
                    }
                }
            }
        }
        private void AddBookButton_Click(object sender, RoutedEventArgs e)
        {
            // For demonstration, we hardcode a book, but you could get these values from TextBoxes.
            AddBook("Book Title", "Author Name", 250);
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





    }
}
