using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpacePirateInventory.Models.Queries
{
    public class ArticleListItem
    {
        public int ArticleId { get; set; }
        public string ArticleTitle { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool? Approved { get; set; }
    }
}
