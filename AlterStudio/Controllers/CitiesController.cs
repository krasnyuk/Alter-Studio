using AlterStudio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AlterStudio.Controllers
{
    public class CitiesController : Controller
    {
        private RemoteStudioEntities _db = new RemoteStudioEntities();

        public ActionResult Index()
        {
            return View(_db.Cities.ToList());
        }
        public PartialViewResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cities cities)
        {
            if (ModelState.IsValid)
            {
                _db.Cities.Add(cities);
                _db.SaveChanges();
                TempData["Success"] = @"Новый город '" + cities.Title + "' был успешно добавлена!";
                return RedirectToAction("Index");
            }

            return PartialView(cities);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cities city = _db.Cities.Find(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CityId,Title,Country")] Cities city)
        {
            if (ModelState.IsValid)
            {
                var entry = _db.Cities.Find(city.CityId);
                entry.Title = city.Title;
                entry.Country = city.Country;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(city);
        }

        public PartialViewResult NewCity()
        {
            return PartialView("Create");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cities city = _db.Cities.Find(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cities city = _db.Cities.Find(id);
            _db.Cities.Remove(city);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}