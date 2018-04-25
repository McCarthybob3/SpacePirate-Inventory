using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using SpacePirateInventory.Data.Interfaces;
using SpacePirateInventory.Models.Tables;
using static Dapper.SqlMapper;

namespace SpacePirateInventory.Data.Dapper
{
    public class CategoryRepository : ICategoryRepository
    {
        public void AddCategory(Category category)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@CategoryId",
                    dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@CategoryName", category.CategoryName);

                cn.Execute("CategoryInsert", parameters, commandType: CommandType.StoredProcedure);

                category.CategoryId = parameters.Get<int>("@CategoryId");
            }
        }

        public void DeleteCategory(int CategoryId)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@CategoryId", CategoryId);

                cn.Execute("CategoryDelete", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<Category> GetAll()
        {
            List<Category> categories = new List<Category>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                return cn.Query<Category>("CategorySelectAll", commandType: CommandType.StoredProcedure);
            }
            //  ConfigurationManager.ConnectionStrings["InventorySystem"].ConnectionString)             
        }

        public Category GetById(int CategoryId)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@CategoryId", CategoryId);

                return cn.Query<Category>("CategorySelectById", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }
    }
}
