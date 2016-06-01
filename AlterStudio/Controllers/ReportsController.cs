using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlterStudio.Models;
using Rotativa;

namespace AlterStudio.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        readonly LocalStudioEntities _db = new LocalStudioEntities();
        public ActionResult Main()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PrintServices(string pattern = "фото")
        {
            return new ViewAsPdf("ListPdf", _db.Services
                .ToList()
                .FindAll(x => x.Title.Contains(pattern.ToLower())));
        }

        public ActionResult PrintPositions()
        {
            return new ViewAsPdf("PositionsPdf", _db.Positions.ToList());
        }
    }
}