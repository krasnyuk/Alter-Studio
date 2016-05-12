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
        public ActionResult Graphics()
        {
            return View();
        }
    }
}