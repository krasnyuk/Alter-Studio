using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlterStudio.Models;
namespace AlterStudio.Controllers
{
    public class PositionsController : Controller
    {
        private RemoteStudioEntities _db = new RemoteStudioEntities();

        public ActionResult List()
        {
            return View(_db.Positions.ToList());
        }
        public PartialViewResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Positions position)
        {
            if (ModelState.IsValid)
            {
                _db.Positions.Add(position);
                _db.SaveChanges();
                TempData["Success"] = @"Новая должность  '" + position.Title + "' была успешно добавлена!";
                return RedirectToAction("List");
            }

            return PartialView(position);
        }

        public PartialViewResult NewPosition()
        {
            return PartialView("Create");
        }
    }
}