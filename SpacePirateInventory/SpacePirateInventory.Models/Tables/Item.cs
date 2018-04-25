using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpacePirateInventory.Models.Tables
{
    public class Item
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemPictureURL { get; set; }
        public int RealValue { get; set; }
        public int DisplayValue { get; set; }
        public string Description { get; set; }
        public bool Favorite { get; set; }
        public bool Featured { get; set; }
        public DateTime DateAdded { get; set; }
        public int CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
