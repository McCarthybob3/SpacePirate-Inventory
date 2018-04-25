using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpacePirateInventory.Models.Tables;

namespace SpacePirateInventory.Data.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();
        void DeleteCategory(int CategoryId);
        void AddCategory(Category category);
        Category GetById(int CategoryId);
    }
}
