using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AlterStudio.Models;

namespace AlterStudio.Controllers
{
    public class ClientController : Controller
    {
        private readonly LocalStudioEntities _db = new LocalStudioEntities();

        public ActionResult List()
        {
            return View(_db.Clients.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Clients client, string city)
        {
            client.CityId = Convert.ToInt32(city);
            if (!string.IsNullOrEmpty(client.FirstName)
                && !string.IsNullOrEmpty(client.LastName)
                && !string.IsNullOrEmpty(client.Telephone))
            {
                _db.Clients.Add(client);
                _db.SaveChanges();
                TempData["Success"] = @"Новый клиент '" + client.FirstName + "' был(а) успешно добавлен(а)!";
                return RedirectToAction("List");
            }
            ClientViewModel model = new ClientViewModel()
            {
                Client = client,
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
            ClientViewModel model = new ClientViewModel()
            {
                Client = _db.Clients.Find(id),
                Cities = from i in _db.Cities
                         select new SelectListItem()
                         {
                             Text = i.Title,
                             Value = i.CityId.ToString()
                         }
            };
            
            if (model.Client == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Clients client, string city)
        {
            if (ModelState.IsValid)
            {
                var entry = _db.Clients.Find(client.ClientId);
                entry.FirstName = client.FirstName;
                entry.LastName = client.LastName;
                entry.Note = client.Note;
                entry.CityId = Convert.ToInt32(city);
                entry.Telephone = client.Telephone;
                _db.SaveChanges();
                return RedirectToAction("List");
            }
            ClientViewModel model = new ClientViewModel()
            {
                Client = client,
                Cities = from i in _db.Cities
                         select new SelectListItem()
                         {
                             Text = i.Title,
                             Value = i.CityId.ToString()
                         }
            };
            return View(model);
        }

        public PartialViewResult NewClient()
        {
            ClientViewModel model = new ClientViewModel()
            {
                Client = null,
                Cities = from i in _db.Cities
                         select new SelectListItem()
                         {
                             Text = i.Title,
                             Value = i.CityId.ToString()
                         }
            };
            return PartialView("Create",model);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clients clients = _db.Clients.Find(id);

            if (clients == null)
            {
                return HttpNotFound();
            }
            return View(clients);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Clients clients = _db.Clients.Find(id);
            _db.Clients.Remove(clients);
            _db.SaveChanges();
            return RedirectToAction("List");
        }
    }
}