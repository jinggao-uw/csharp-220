using System.Windows;
using InventoryApp.Models;

namespace InventoryApp
{
    // The window for adding and modifying new inventory entry
    public partial class InventoryWindow : Window
    {
        public InventoryWindow()
        {
            InitializeComponent();
        }

        public InventoryModel Inventory { get; set; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Inventory != null)
            {
                uxSubmit.Content = "Update";
            }
            else
            {
                Inventory = new InventoryModel();
            }

            // Setup  databinding, one time one direction.
            uxGrid.DataContext = Inventory;
        }

        private void uxSubmit_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void uxCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
