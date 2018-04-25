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
    public class AuthorRepository : IAuthorRepository
    {
        public IEnumerable<Author> GetAll()
        {
            List<Author> categories = new List<Author>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                return cn.Query<Author>("AuthorSelectAll", commandType: CommandType.StoredProcedure);
            }
        }

        //public Author GetById(int AuthorId)
        //{
        //    using (var cn = new SqlConnection(Settings.GetConnectionString()))
        //    {
        //        var parameters = new DynamicParameters();
        //        parameters.Add("@AuthorId", AuthorId);

        //        return cn.Query<Author>("CategorySelectById", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
        //    }
        //}
    }
}
