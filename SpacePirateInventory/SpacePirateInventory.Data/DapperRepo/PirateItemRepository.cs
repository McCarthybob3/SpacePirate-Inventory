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
    public class PirateItemRepository : IPirateItemRepository
    {
        public Item Delete(int ItemId)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ItemId", ItemId);

                return cn.Query<Item>("ItemDelete", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public IEnumerable<ItemLong> GetAll()
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                return cn.Query<ItemLong>("ItemSelectAll", commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<ItemShort> GetAllFeaturedShortItems()
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                return cn.Query<ItemShort>("ItemSelectFeautredShortAll", commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<ItemShort> GetAllShortItems()
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                return cn.Query<ItemShort>("ItemSelectShortAll", commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<Item> GetByCategoryId(Category categoryId)
        {
            throw new NotImplementedException();
        }

        public ItemLong GetById(int ItemId)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ItemId", ItemId);

                return cn.Query<ItemLong>("ItemSelectByIdLong", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public Item GetItemById(int ItemId)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ItemId", ItemId);

                return cn.Query<Item>("ItemSelectShortById", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public void Insert(Item item)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ItemId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@ItemName", item.ItemName);
                parameters.Add("@RealValue", item.RealValue);
                parameters.Add("@DisplayValue", item.DisplayValue);
                parameters.Add("@Description", item.Description);
                parameters.Add("@Favorite", item.Favorite);
                parameters.Add("@Featured", item.Featured);
                parameters.Add("@CurrencyName", item.CurrencyName);
                parameters.Add("@CategoryName", item.CategoryName);
                parameters.Add("@ItemPictureURL", item.ItemPictureURL);

                cn.Execute("ItemInsert", parameters, commandType: CommandType.StoredProcedure);

                item.ItemId = parameters.Get<int>("@ItemId");
            }
        }

        public IEnumerable<ItemShort> Search(ItemSearchParameters parameters)
        {
            List<ItemShort> itemInfo = new List<ItemShort>();
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var param = new DynamicParameters();
                param.Add("@MinValue", parameters.MinValue);
                param.Add("@MaxValue", parameters.MaxValue);
                param.Add("@ItemName", parameters.ItemName);
                param.Add("@CategoryName", parameters.CategoryName);

                return cn.Query<ItemShort>("ItemSelectForSearch", parameters, commandType: CommandType.StoredProcedure);
                //string query = "SELECT I.DateAdded,[Description],DisplayValue,Favorite,Featured,ItemName,ItemPictureURL,RealValue, Ca.CategoryName, " +
                //    "A.ArticleId FROM Item I inner join ItemCategory ICa on I.ItemId = ICa.ItemId inner join ItemArticle IA on I.ItemId = IA.ItemId " +
                //    "inner join Category Ca on ICa.CategoryId = CA.CategoryId inner join Article A on IA.ArticleId = A.ArticleId WHERE 1 = 1 ";
                //SqlCommand cmd = new SqlCommand();
                //cmd.Connection = cn;

                //if (parameters.MinValue.HasValue)
                //{
                //    query += "AND DisplayValue >= @MinValue ";
                //    cmd.Parameters.AddWithValue("@MinValue", parameters.MinValue.Value);
                //}
                //if (parameters.MaxValue.HasValue)
                //{
                //    query += "AND DisplayValue >= @MaxValue ";
                //    cmd.Parameters.AddWithValue("@MaxValue", parameters.MaxValue.Value);
                //}
                //if (!string.IsNullOrEmpty(parameters.ItemName))
                //{
                //    query += "AND ItemName LIKE @ItemName ";
                //    cmd.Parameters.AddWithValue("@ItemName", '%' + parameters.ItemName + '%');
                //}
                //if (!string.IsNullOrEmpty(parameters.CategoryName))
                //{
                //    query += "AND CategoryName LIKE @CategoryName ";
                //    cmd.Parameters.AddWithValue("@CategoryName", '%' + parameters.CategoryName + '%');
                //}

                //cmd.CommandText = query;
                //cn.Open();


            }
            //return itemInfo;
        }

        public void Update(Item item)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ItemId", item.ItemId);
                parameters.Add("@ItemName", item.ItemName);
                parameters.Add("@RealValue", item.RealValue);
                parameters.Add("@DisplayValue", item.DisplayValue);
                parameters.Add("@Description", item.Description);
                parameters.Add("@Favorite", item.Favorite);
                parameters.Add("@Featured", item.Featured);
                parameters.Add("@CurrencyId", item.CurrencyId);
                parameters.Add("@CategoryId", item.CategoryId);
                parameters.Add("@ItemPictureURL", item.ItemPictureURL);

                cn.Execute("ItemUpdate", parameters, commandType: CommandType.StoredProcedure);

                item.ItemId = parameters.Get<int>("@ItemId");
            }
        }
    }
}
