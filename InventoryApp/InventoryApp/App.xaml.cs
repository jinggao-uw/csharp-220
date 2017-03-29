using System.Windows;

namespace InventoryApp
{
    /// <summary>
    /// The Application class for the InventoryApp
    /// </summary>
    public partial class App : Application
    {
        // Singleton instance of the InventoryRepository
        private static InventoryRepository.InventoryRepository inventoryRepository;

        static App()
        {
            inventoryRepository = new InventoryRepository.InventoryRepository();
        }

        // Readonly property to access the InventoryRepository instance
        public static InventoryRepository.InventoryRepository InventoryRepository
        {
            get
            {
                return inventoryRepository;
            }
        }
    }
}
