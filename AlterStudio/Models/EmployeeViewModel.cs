using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlterStudio.Models
{
    public class EmployeeViewModel
    {
        public Employees Employee { get; set; }
        public IEnumerable<SelectListItem> Cities { get; set; }
        public IEnumerable<SelectListItem> Positions { get; set; }

    }
}