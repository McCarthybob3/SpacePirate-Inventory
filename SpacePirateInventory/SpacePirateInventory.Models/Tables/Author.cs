using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpacePirateInventory.Models.Tables
{
    public class Author
    {
        public int AuthorId { get; set; }
        public int AspNetUserId { get; set; }
        public string AuthorName { get; set; }
    }
}
