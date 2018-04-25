using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpacePirateInventory.Models.Queries;
using SpacePirateInventory.Models.Tables;

namespace SpacePirateInventory.Data.Interfaces
{
    public interface IPirateItemRepository
    {
        ItemLong GetById(int ItemId);
        void Insert(Item item);
        void Update(Item item);
        Item Delete(int ItemId);
        IEnumerable<Item> GetByCategoryId(Category categoryId);
        IEnumerable<ItemLong> GetAll();
        IEnumerable<ItemShort> GetAllFeaturedShortItems();
        IEnumerable<ItemShort> GetAllShortItems();
        IEnumerable<ItemShort> Search(ItemSearchParameters parameters);
    }
}
