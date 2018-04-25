using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpacePirateInventory.Models.Queries
{
    public class ItemShort
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemPictureURL { get; set; }
        public int DisplayValue { get; set; }
    }
}
