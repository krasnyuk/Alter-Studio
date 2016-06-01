using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlterStudio.Models
{
	public class OrderViewModel
	{
        public Orders Order { get; set; }
        public IEnumerable<SelectListItem> Clients { get; set; }
        public IEnumerable<SelectListItem> Curators { get; set; }

    }
}