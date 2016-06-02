using AlterStudio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AlterStudio.Controllers
{
    public class CuratorController : Controller
    {
        private readonly LocalStudioEntities _db = new LocalStudioEntities();

        public ActionResult List()
        {
            return View(_db.Curators.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Curators curator, string city)
        {
            curator.CityId = Convert.ToInt32(city);
            if (!string.IsNullOrEmpty(curator.FirstName)
                && !string.IsNullOrEmpty(curator.LastName)
                && !string.IsNullOrEmpty(curator.Telephone)
                && !string.IsNullOrEmpty(curator.Email)
                && !string.IsNullOrEmpty(curator.Rate.ToString()))
            {
                _db.Curators.Add(curator);
                _db.SaveChanges();
                TempData["Success"] = @"Новый куратор '" + curator.FirstName + "' был(а) успешно добавлен(а)!";
                return RedirectToAction("List");
            }
            CuratorViewModel model = new CuratorViewModel()
            {
                Curator = curator,
                Cities = from i in _db.Cities
                         select new SelectListItem()
                         {
                             Text = i.Title,
                             Value = i.CityId.ToString()
                         }
            };
            return PartialView(model);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuratorViewModel model = new CuratorViewModel()
            {
                Curator = _db.Curators.Find(id),
                Cities = from i in _db.Cities
                         select new SelectListItem()
                         {
                             Text = i.Title,
                             Value = i.CityId.ToString()
                         }
            };

            if (model.Curator == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Curators curator, string city)
        {
            if (ModelState.IsValid)
            {
                var entry = _db.Curators.Find(curator.CuratorId);
                entry.FirstName = curator.FirstName;
                entry.LastName = curator.LastName;
                entry.Telephone = curator.Telephone;
                entry.Rate = curator.Rate;
                entry.Email = curator.Email;
                entry.CityId = Convert.ToInt32(city);
                _db.SaveChanges();
                return RedirectToAction("List");
            }
            CuratorViewModel model = new CuratorViewModel()
            {
                Curator = curator,
                Cities = from i in _db.Cities
                         select new SelectListItem()
                         {
                             Text = i.Title,
                             Value = i.CityId.ToString()
                         }
            };
            return View(model);
        }

        public PartialViewResult NewCurator()
        {
            var model = new CuratorViewModel()
            {
                Curator = null,
                Cities = from i in _db.Cities
                         select new SelectListItem()
                         {
                             Text = i.Title,
                             Value = i.CityId.ToString()
                         }
            };
            return PartialView("Create", model);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Curators curators = _db.Curators.Find(id);

            if (curators == null)
            {
                return HttpNotFound();
            }
            return View(curators);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Curators curators = _db.Curators.Include("Orders").FirstOrDefault(x=>x.CuratorId == id);
            if (curators.Orders.Count!=0) // куратор есть в заказах
            {
                ModelState.AddModelError("Existing", "Куратор используется в заказах! Измените заказы, перед тем как удалить данного куратора.");
                return View("Delete", curators);
            }
            _db.Curators.Remove(curators);
            _db.SaveChanges();
            return RedirectToAction("List");
        }
    }
}
