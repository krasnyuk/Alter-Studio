using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AlterStudio.Models;
using Rotativa;

namespace AlterStudio.Controllers
{
    [Authorize]
    public class ServicesController : Controller
    {
        private readonly LocalStudioEntities _db = new LocalStudioEntities();

        public ActionResult Index()
        { 
            return View(_db.Services.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Services services)
        {
            if (ModelState.IsValid)
            {
                _db.Services.Add(services);
                _db.SaveChanges();
                TempData["Success"] = @"Новая услуга  '" + services.Title.ToUpper() + "' была успешно добавлена!";
                return RedirectToAction("Index");
            }
            return View(services);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Services services = _db.Services.Find(id);
            if (services == null)
            {
                return HttpNotFound();
            }
            return View(services);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ServiceId,Title,Description,Cost,Note")] Services services)
        {
            if (ModelState.IsValid)
            {
                var entry = _db.Services.Find(services.ServiceId);
                entry.Cost = services.Cost;
                entry.Description = services.Description;
                entry.Note = services.Note;
                entry.Title = services.Title;
               // db.Entry(services).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(services);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Services services = _db.Services.Find(id);
            if (services == null)
            {
                return HttpNotFound();
            }
            return View(services);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Services services = _db.Services
                .Include("OrderDetails")
                .FirstOrDefault(x=>x.ServiceId == id);
            if (services.OrderDetails.Count!=0)
            {
                ModelState.AddModelError("Existing", "Услуга используется в таблицах! Измените данные, перед тем как удалить.");
                return View("Delete", services);
            }
            _db.Services.Remove(services);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public PartialViewResult NewService()
        {
            return PartialView("_CreatePartial");
        }
      
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
        [HttpPost]
        public ActionResult Print(string pattern = "фото")
        {
            return 
                new ViewAsPdf("ListPdf", _db.Services
                .ToList()
                .FindAll(x=>x.Title.Contains(pattern.ToLower())));
        }
    }
}
