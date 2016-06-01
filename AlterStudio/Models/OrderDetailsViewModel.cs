using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlterStudio.Models
{
    public class OrderDetailsViewModel
    {
        public IEnumerable<SelectListItem> Employees { get; set; }
        public IEnumerable<SelectListItem> Services  { get; set; }
        public OrderDetails OrderDetail { get; set; }
        public int OrderId { get; set; }
        
    }
}