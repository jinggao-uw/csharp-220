using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryApp.Models
{
    // UI model
    public class InventoryModel: INotifyPropertyChanged
    {
        // Properties of the inventory UI model
        public int ItemId { get; set; }
        public string Description { get; set; }

        private decimal _unitPrice;
        public decimal UnitPrice
        {
            get
            {
                return _unitPrice;
            }
            set
            {
                if (_unitPrice != value)
                {
                    _unitPrice = value;

                    _value = _unitPrice * _quantities;
                    OnPropertyChanged("Value");
                }
            }
        }

        private int _quantities;
        public int Quantities
        {
            get
            {
                return _quantities;
            }
            set
            {
                if (_quantities != value)
                {
                    _quantities = value;

                    _value = _unitPrice * _quantities;
                    OnPropertyChanged("Value");
                }
            }
        }

        public decimal UnitCost { get; set; }

        private decimal _value;
        public decimal Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        public string Notes { get; set; }

        // INotifyPropertyChanged interface implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        // Create a reporotify model from current ui model
        public InventoryRepository.InventoryModel ToRepositoryModel()
        {
            var repositoryModel = new InventoryRepository.InventoryModel
            {
                ItemId = ItemId,
                Description = Description,
                UnitPrice = UnitPrice,
                Quantities = Quantities,
                UnitCost = UnitCost,
                Value = Value,
                Notes = Notes,
            };

            return repositoryModel;
        }

        // Convert a repository model to ui model
        public static InventoryModel ToModel(InventoryRepository.InventoryModel respositoryModel)
        {
            var inventoryModel = new InventoryModel
            {
                ItemId = respositoryModel.ItemId,
                Description = respositoryModel.Description,
                UnitPrice = respositoryModel.UnitPrice,
                Quantities = respositoryModel.Quantities,
                UnitCost = respositoryModel.UnitCost,
                Value = respositoryModel.Value,
                Notes = respositoryModel.Notes,
            };

            return inventoryModel;
        }
    }
}
