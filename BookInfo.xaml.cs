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

namespace dash
{
    /// <summary>
    /// Interaction logic for BookInfo.xaml
    /// </summary>
    public partial class BookInfo : Window
    {
        private string book;
        public BookInfo(string bookTitle)
        {
            InitializeComponent();
            book = bookTitle;
            ShowBookInfo();
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

        private void ShowBookInfo()
        {
            //characterInfoBox.Items.Clear(); // Clear the list before showing new titles
            bookInfoBox.Items.Clear();
            bookImageBox.Items.Clear();
            //characterBox.Items.Add(characterName);
            //characterImageBox.Items.Clear();
            string connectionString = "Data Source=books.db;Version=3;";
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string selectCharacterBoxQuery = "SELECT Title, Author, PageNumber, Genre, Summary FROM AllTimesPopular WHERE Title = @title";

                using (var command = new SQLiteCommand(selectCharacterBoxQuery, connection))
                {
                    command.Parameters.AddWithValue("@title", book);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string name = reader["Title"].ToString();
                            string author = reader["Author"].ToString();
                            string pagenmb = reader["PageNumber"].ToString();
                            string genre = reader["Genre"].ToString();
                            string sum = reader["Summary"].ToString();
                            string bookDetails = $"{name}\n{author}\nPage number: {pagenmb}\nGenre: {genre}\n{sum}";
                            bookInfoBox.Items.Add(bookDetails);
                            //bookInfoBox.Items.Add(name);
                            //bookInfoBox.Items.Add(author);
                            //bookInfoBox.Items.Add(pagenmb);
                            //bookInfoBox.Items.Add(genre);

                            //characterBox.Items.Add($"Name: {name}\n"); // Add to the ListBox
                            //characterBox.Items.Add("\n"); //ISE YARAMIYOR!!!
                            //characterBox.Items.Add($"Title: {title}\n");
                            //characterBox.Items.Add("\n");
                            //characterBox.Items.Add($"Author: {author}\n");
                            //characterBox.Items.Add("\n");

                            //string desc = reader["Description"].ToString();
                            //string powers = reader["Powers"].ToString();
                            //string strength = reader["Strength"].ToString();
                            //characterInfoBox.Items.Add(desc + "\n");
                            //characterInfoBox.Items.Add(powers + "\n");
                            //characterInfoBox.Items.Add(" ");
                            //characterInfoBox.Items.Add(strength + "\n");
                        }
                    }
                }

            }
        }
    }
}
