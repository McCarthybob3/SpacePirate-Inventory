using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpacePirateInventory.Models.Queries
{
    public class InventoryListItem
    {
        public int ItemId { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string AcutalValue { get; set; }
        public string DisplayValue { get; set; }
        public int CurrencyName { get; set; }

    }
}
