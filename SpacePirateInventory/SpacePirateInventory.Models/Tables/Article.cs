using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpacePirateInventory.Models.Tables
{
    public class Article
    {
        public int ArticleId { get; set; }
        public string ArticleTitle { get; set; }
        public string ArticleContent { get; set; }
        public DateTime DateAdded { get; set; }
        public string UserId { get; set; }
        public bool Approved { get; set; }
    }
}
