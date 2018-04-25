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
    public class ArticleRepository : IArticleRepository
    {
        public void AddArticle(Article article)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ArticleId",
                    dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@ArticleTitle", article.ArticleTitle);
                parameters.Add("@ArticleContent", article.ArticleContent);
                parameters.Add("@DateAdded", DateTime.Today);
                parameters.Add("@UserId", article.UserId);
                parameters.Add("@Approved", 0);

                cn.Execute("ArticleInsert", parameters, commandType: CommandType.StoredProcedure);

                article.ArticleId = parameters.Get<int>("@ArticleId");
            }
        }

        public IEnumerable<ArticleListItem> GetAll()
        {
            List<Article> articles = new List<Article>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                return cn.Query<ArticleListItem>("ArticleSelectAll", commandType: CommandType.StoredProcedure);
            }
        }

        public Article GetById(int ArticleId)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ArticleId", ArticleId);

                return cn.Query<Article>("ArticleSelectById", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public Article DeleteArticleById(int ArticleId)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ArticleId", ArticleId);

                return cn.Query<Article>("ArticleDelete", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public void UpdateArticle(Article article)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ArticleId", article.ArticleId);
                parameters.Add("@ArticleTitle", article.ArticleTitle);
                parameters.Add("@ArticleContent", article.ArticleContent);
                parameters.Add("@UserId", article.UserId);
                parameters.Add("@DateAdded", DateTime.Now);
                parameters.Add("@Approved", article.Approved);

                //cn.Query<Article>("ArticleUpdate", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
                cn.Execute("ArticleUpdate", parameters, commandType: CommandType.StoredProcedure);

                article.ArticleId = parameters.Get<int>("@ArticleId");
            }
        }
    }
}
