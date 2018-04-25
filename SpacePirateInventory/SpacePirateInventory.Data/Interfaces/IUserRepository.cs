using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpacePirateInventory.Models.Queries;

namespace SpacePirateInventory.Data.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<UserListItem> GetAll();
    }
}
