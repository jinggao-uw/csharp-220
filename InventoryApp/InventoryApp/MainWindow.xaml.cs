using System.Linq;
using System.Windows;
using System.Windows.Controls;
using InventoryApp.Models;

namespace InventoryApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadInventory();
        }

        // Private variable holding reference to the current selected inventory item
        private InventoryModel selectedInventory;

        // Initial load of the inventory
        private void LoadInventory()
        {
            // Get all the items from the database using inventory repository
            var inventory = App.InventoryRepository.GetAll();

            // Databind the inventory to the list control
            uxInventoryList.ItemsSource = inventory.Select(t => InventoryModel.ToModel(t)).ToList();
        }

        // Event handler of the 'new' menu item
        private void uxFileNew_Click(object sender, RoutedEventArgs e)
        {
            var window = new InventoryWindow();

            if (window.ShowDialog() == true)
            {
                // Get a reference to the ui inventory and construct a repository model
                var uiModel = window.Inventory;
                var repositoryModel = uiModel.ToRepositoryModel();

                App.InventoryRepository.Add(repositoryModel);

                // Reload inventory
                LoadInventory();
            }
        }

        // Event handler of the 'delete' menu item
        private void uxFileDelete_Click(object sender, RoutedEventArgs e)
        {
            App.InventoryRepository.Remove(selectedInventory.ItemId);
            selectedInventory = null;
            LoadInventory();
        }


        // Event handler of the 'modify' menu item
        private void uxFileChange_Click(object sender, RoutedEventArgs e)
        {
            var window = new InventoryWindow();
            window.Inventory = selectedInventory;

            if (window.ShowDialog() == true)
            {
                App.InventoryRepository.Update(window.Inventory.ToRepositoryModel());
                LoadInventory();
            }
        }

        // Evnet handlers for state changes
        private void uxFileChange_Loaded(object sender, RoutedEventArgs e)
        {
            uxFileChange.IsEnabled = (selectedInventory != null);
            uxContextFileChange.IsEnabled = uxFileChange.IsEnabled;
        }

        private void uxFileDelete_Loaded(object sender, RoutedEventArgs e)
        {
            uxFileDelete.IsEnabled = (selectedInventory != null);
        }

        private void uxInventoryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedInventory = (InventoryModel)uxInventoryList.SelectedValue;
        }
    }
}

