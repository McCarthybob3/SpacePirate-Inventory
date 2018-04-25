using SpacePirateInventory.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpacePirateInventory.Models.Queries
{
    public class ArticleLong
    {
        public int ArticleId { get; set; }
        public string ArticleTitle { get; set; }
        public string ArticleContent { get; set; }
        public DateTime DateAdded { get; set; }
        public string UserId { get; set; }
        public IEnumerable<Item> Item { get; set; }
    }
}
