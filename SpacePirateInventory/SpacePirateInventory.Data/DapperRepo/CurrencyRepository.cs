using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using SpacePirateInventory.Data.Interfaces;
using SpacePirateInventory.Models.Tables;

namespace SpacePirateInventory.Data.DapperRepo
{
    public class CurrencyRepository : ICurrencyRepository
    {
        public IEnumerable<Currency> GetAll()
        {
            List<Currency> currencies = new List<Currency>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                return cn.Query<Currency>("CurrencySelectAll", commandType: CommandType.StoredProcedure);
            }
        }
    }
}
