using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace homework3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public CollectionView MainView { get; set; }
        public ListSortDirection NameDirection { get; set; }
        public ListSortDirection PasswordDirection { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            var users = new List<Models.User>();

            users.Add(new Models.User { Name = "Dave", Password = "1DavePwd" });
            users.Add(new Models.User { Name = "Steve", Password = "2StevePwd" });
            users.Add(new Models.User { Name = "Lisa", Password = "3LisaPwd" });

            uxList.ItemsSource = users;

            // Setup the initial sorting descriptions
            MainView = (CollectionView)CollectionViewSource.GetDefaultView(uxList.ItemsSource);
            NameDirection = ListSortDirection.Ascending;
            PasswordDirection = ListSortDirection.Descending;

            MainView.SortDescriptions.Add(new SortDescription("Name", NameDirection));
        }

        public void SortByName()
        {
            MainView.SortDescriptions.Clear();

            if (NameDirection == ListSortDirection.Ascending)
            {
                NameDirection = ListSortDirection.Descending;
            }
            else
            {
                NameDirection = ListSortDirection.Ascending;
            }
            
            MainView.SortDescriptions.Add(new SortDescription("Name", NameDirection));
        }

        public void SortByPassword()
        {
            MainView.SortDescriptions.Clear();

            if (PasswordDirection == ListSortDirection.Ascending)
            {
                PasswordDirection = ListSortDirection.Descending;
            }
            else
            {
                PasswordDirection = ListSortDirection.Ascending;
            }

            MainView.SortDescriptions.Add(new SortDescription("Password", PasswordDirection));
        }

        private void OnListColumnHeader_Clicked(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader header = sender as GridViewColumnHeader;

            if (header.Tag.ToString() == "Name")
            {
                SortByName();
            }
            else if (header.Tag.ToString() == "Password")
            {
                SortByPassword();
            }

        }
    }
}
