
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

        private void GoToBookInfo(object sender, MouseButtonEventArgs e)
        {
            var border = sender as Border;
            var bookTitle = (border?.DataContext ?? "Unknown").ToString();
            //MessageBox.Show($"Clicked: {bookTitle}");
            var bookInfo = new BookInfo(bookTitle);
            bookInfo.Show();
            this.Close();
        }

        private void GoToCharacterInfo(object sender, MouseButtonEventArgs e)
        {
            var border = sender as Border;
            var chName = (border?.DataContext ?? "Unknown").ToString();
            //MessageBox.Show($"Clicked: {chName}");
            var characterinfo = new CharacterInfo(chName);
            Application.Current.MainWindow = characterinfo;
            characterinfo.Show();
            this.Close(); // fine as long as CharacterInfo becomes MainWindow
            //characterinfo.Show();
            //this.Close();
        }








    }
}
