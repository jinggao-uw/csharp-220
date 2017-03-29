using InventoryDB;


namespace InventoryRepository
{
    public class DatabaseManager
    {
        private static readonly InvenotryEntities entities;

        // Initialize and open the database connection
        static DatabaseManager()
        {
            entities = new InvenotryEntities();
            entities.Database.Connection.Open();
        }

        // Provide an accessor to the database
        public static InvenotryEntities Instance
        {
            get
            {
                return entities;
            }
        }
    }
}