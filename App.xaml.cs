using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SQLite;
using System.IO;

namespace dash
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            DeleteAllBooks("AllTimesPopular");
            DeleteAllBooks("PopularCharacters");
            DeleteAllBooks("Books");
            DeleteAllBooks("TrendingBooks");
            ImportDatasets(@"C:\Users\Selin\Desktop\dash\popular.txt", "AllTimesPopular", true);
            ImportDatasets(@"C:\Users\Selin\Desktop\dash\popularCharacters.txt", "PopularCharacters", false);
            ImportDatasets(@"C:\Users\Selin\Desktop\dash\books.txt", "Books", true);
            ImportDatasets(@"C:\Users\Selin\Desktop\dash\trending.txt", "TrendingBooks", true);
            
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
                //MessageBox.Show("All records deleted successfully.");
            }
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
                        //I dropped table after adding genre. 
                        //string drop = $"DROP TABLE {tableName}";
                        //using (var table = new SQLiteCommand(drop, connection))
                        //{
                        //    table.ExecuteNonQuery();
                        //}
                        string createTableQuery = $@"CREATE TABLE IF NOT EXISTS {tableName} (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Title TEXT,
                        Author TEXT,
                        PageNumber INTEGER,
                        Genre TEXT
                        )";
                        using (var tableCommand = new SQLiteCommand(createTableQuery, connection))
                        {
                            tableCommand.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        //string drop = $"DROP TABLE {tableName}";
                        //using (var table = new SQLiteCommand(drop, connection))
                        //{
                        //    table.ExecuteNonQuery();
                        //}
                        string createTableQuery = $@"CREATE TABLE IF NOT EXISTS {tableName} (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT,
                        Title TEXT,
                        Author TEXT,
                        Description TEXT,
                        Powers, TEXT,
                        Strength TEXT
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
                        //if (parts.Length == 3)
                        //{
                            if (isBook)
                            {
                                string title = parts[0].Trim();
                                string author = parts[1].Trim();
                                int pageNumber = int.TryParse(parts[2].Trim(), out int pn) ? pn : 0;
                                string genre = parts[3].Trim();

                                string insertQuery = $@"INSERT INTO {tableName} (Title, Author, PageNumber, Genre)
                                           VALUES (@Title, @Author, @PageNumber, @Genre)";
                                using (var command = new SQLiteCommand(insertQuery, connection))
                                {
                                    command.Parameters.AddWithValue("@Title", title);
                                    command.Parameters.AddWithValue("@Author", author);
                                    command.Parameters.AddWithValue("@PageNumber", pageNumber);
                                    command.Parameters.AddWithValue("@Genre", genre);
                                    command.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                string name = parts[0].Trim();
                                string title = parts[1].Trim();
                                string author = parts[2].Trim();
                                string description = parts[3].Trim();
                                string powers = parts[4].Trim();
                                string strength = parts[5].Trim();

                            string insertQuery = $@"INSERT INTO {tableName} (Name, Title, Author, Description, Powers, Strength)
                                           VALUES (@Name, @Title, @Author, @Description, @Powers, @Strength)";
                                using (var command = new SQLiteCommand(insertQuery, connection))
                                {
                                    command.Parameters.AddWithValue("@Name", name);
                                    command.Parameters.AddWithValue("@Title", title);
                                    command.Parameters.AddWithValue("@Author", author);
                                    command.Parameters.AddWithValue("@Description", description);
                                    command.Parameters.AddWithValue("@Powers", powers);
                                    command.Parameters.AddWithValue("@Strength", strength);
                                    command.ExecuteNonQuery();
                                }
                            }
                        //}

                    }

                    //MessageBox.Show($"{tableName} imported successfully.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error importing {tableName}: {ex.Message}");
            }
        }

    }
}
