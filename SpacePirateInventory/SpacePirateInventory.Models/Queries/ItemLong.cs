using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpacePirateInventory.Models.Tables;

namespace SpacePirateInventory.Models.Queries
{
    public class ItemLong
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemPictureURL { get; set; }
        public int RealValue { get; set; }
        public int DisplayValue { get; set; }
        public string Description { get; set; }
        public string CurrencyName { get; set; }
        public bool Favorite { get; set; }
        public string CategoryName { get; set; }
        public IEnumerable<Category> Category { get; set; }
        public IEnumerable<Article> Article { get; set; }


    }
}
