using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpacePirateInventory.Models.Queries;
using SpacePirateInventory.Models.Tables;

namespace SpacePirateInventory.Data.Interfaces
{
    public interface IArticleRepository
    {
        IEnumerable<ArticleListItem> GetAll();
        Article GetById(int ArticleId);
        void UpdateArticle(Article article);
        void AddArticle(Article article);
    }
}
