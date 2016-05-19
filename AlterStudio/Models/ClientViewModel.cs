using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlterStudio.Models
{
    public class ClientViewModel
    {
        public Clients Client { get; set; }
        public  IEnumerable<SelectListItem> Cities { get; set; } 
    }
}