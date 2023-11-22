using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nimap_Product_Test.Models
{
    public class ProductDM
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int TotalRows { get; set; }

        public int PageNumber { get; set; }
        
    }
}