using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpacePirateInventory.Models.Tables;

namespace SpacePirateInventory.Models
{
    public class ArticleEditViewModel
    {
        public IEnumerable<SelectListItem> Categories { get; set; }
        public IEnumerable<SelectListItem> User { get; set; }
        public Article Article { get; set; }
    }
}