using System.Collections.Generic;
using System.Linq;
using InventoryDB;


namespace InventoryRepository
{
    public class InventoryModel
    {
        public int ItemId { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantities { get; set; }
        public decimal UnitCost { get; set; }
        public decimal Value { get; set; }
        public string Notes { get; set; }
    }

    public class InventoryRepository
    {
        public InventoryModel Add(InventoryModel inventoryModel)
        {
            var inventoryDb = ToDbModel(inventoryModel);

            DatabaseManager.Instance.Inventories.Add(inventoryDb);
            DatabaseManager.Instance.SaveChanges();

            inventoryModel = new InventoryModel
            {
                ItemId = inventoryDb.InventoryItemId,
                Description = inventoryDb.InventoryDescription,
                UnitPrice = inventoryDb.InventoryUnitPrice,
                Quantities = inventoryDb.InventoryQuantities,
                UnitCost = inventoryDb.InventoryUnitCost,
                Value = inventoryDb.InventoryValue,
                Notes = inventoryDb.InventoryNotes,
            };
            return inventoryModel;
        }

        public List<InventoryModel> GetAll()
        {
            // Use .Select() to map the database inventories to InventoryModel
            var items = DatabaseManager.Instance.Inventories
              .Select(t => new InventoryModel
              {
                  ItemId = t.InventoryItemId,
                  Description = t.InventoryDescription,
                  UnitPrice = t.InventoryUnitPrice,
                  Quantities = t.InventoryQuantities,
                  UnitCost = t.InventoryUnitCost,
                  Value = t.InventoryValue,
                  Notes = t.InventoryNotes,
              }).ToList();

            return items;
        }

        public bool Update(InventoryModel inventoryModel)
        {
            var original = DatabaseManager.Instance.Inventories.Find(inventoryModel.ItemId);

            if (original != null)
            {
                DatabaseManager.Instance.Entry(original).CurrentValues.SetValues(ToDbModel(inventoryModel));
                DatabaseManager.Instance.SaveChanges();
            }

            return false;
        }

        public bool Remove(int inventoryItemId)
        {
            var items = DatabaseManager.Instance.Inventories
                                .Where(t => t.InventoryItemId == inventoryItemId);

            if (items.Count() == 0)
            {
                return false;
            }

            DatabaseManager.Instance.Inventories.Remove(items.First());
            DatabaseManager.Instance.SaveChanges();

            return true;
        }

        private Inventory ToDbModel(InventoryModel inventoryModel)
        {
            var contactDb = new Inventory
            {
                InventoryItemId = inventoryModel.ItemId,
                InventoryDescription = inventoryModel.Description,
                InventoryUnitPrice = inventoryModel.UnitPrice,
                InventoryQuantities = inventoryModel.Quantities,
                InventoryUnitCost = inventoryModel.UnitCost,
                InventoryValue = inventoryModel.Value,
                InventoryNotes = inventoryModel.Notes,
            };

            return contactDb;
        }
    }
}
