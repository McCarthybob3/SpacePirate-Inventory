using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpacePirateInventory.Models.Tables;

namespace SpacePirateInventory.Data.Interfaces
{
    public interface IAuthorRepository
    {
        IEnumerable<Author> GetAll();
        //Author GetById(int AuthorId);
    }
}
