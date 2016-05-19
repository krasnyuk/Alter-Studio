using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlterStudio.Models;

namespace AlterStudio.Controllers
{
    [Authorize]
    public class AnalysisController : Controller
    {
        RemoteStudioEntities _db = new RemoteStudioEntities();
        public JsonResult GetServicesCostsJson()
        {
            var data = from d in _db.Services
                       select new
                       {
                           d.Title,
                           d.Cost
                       };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Graphics()
        {
            return View();
        }
    }
}