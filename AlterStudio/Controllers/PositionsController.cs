using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AlterStudio.Models;
namespace AlterStudio.Controllers
{
    [Authorize]
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

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Positions positions = _db.Positions.Find(id);
            if (positions == null)
            {
                return HttpNotFound();
            }
            return View(positions);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PositionId,Title,Description,Note")] Positions positions)
        {
            if (ModelState.IsValid)
            {
                var entry = _db.Positions.Find(positions.PositionId);
                entry.Description = positions.Description;
                entry.Note = positions.Note;
                entry.Title = positions.Title;
                _db.SaveChanges();
                return RedirectToAction("List");
            }
            return View(positions);
        }

        public PartialViewResult NewPosition()
        {
            return PartialView("Create");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Positions positions = _db.Positions.Find(id);
            if (positions == null)
            {
                return HttpNotFound();
            }
            return View(positions);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Positions positions = _db.Positions.Find(id);
            _db.Positions.Remove(positions);
            _db.SaveChanges();
            return RedirectToAction("List");
        }
    }
}