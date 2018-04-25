using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using SpacePirateInventory.Data.Interfaces;
using SpacePirateInventory.Models.Queries;
using SpacePirateInventory.Models.Tables;

namespace SpacePirateInventory.Data.DapperRepo
{
    public class UserRepository : IUserRepository
    {
        public IEnumerable<UserListItem> GetAll()
        {
            List<UserListItem> articles = new List<UserListItem>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                return cn.Query<UserListItem>("UserListSelect", commandType: CommandType.StoredProcedure);
            }
        }

        
    }
}
