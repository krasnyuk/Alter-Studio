using AlterStudio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AlterStudio.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly LocalStudioEntities _db = new LocalStudioEntities();

        public ActionResult List()
        {
            return View(_db.Employees.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employees employee, string city, string position)
        {
            employee.CityId = int.Parse(city);
            employee.PositionId = int.Parse(position);
            if (ModelState.IsValid)
            {
                _db.Employees.Add(employee);
                _db.SaveChanges();
                TempData["Success"] = @"Новый сотрудник '" + employee.FullName + "' был(а) успешно добавлен(а)!";
                return RedirectToAction("List");
            }
            EmployeeViewModel model = new EmployeeViewModel()
            {
                Employee = employee,
                Cities = from i in _db.Cities
                         select new SelectListItem()
                         {
                             Text = i.Title,
                             Value = i.CityId.ToString()
                         },
                Positions = from i in _db.Positions
                            select new SelectListItem()
                            {
                                Text = i.Title,
                                Value = i.PositionId.ToString()
                            }
            };
            return PartialView(model);
        }

        public ActionResult Edit(int? employeeId)
        {
            if (employeeId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeViewModel model = new EmployeeViewModel()
            {
                Employee = _db.Employees.Find(employeeId),
                Cities = from i in _db.Cities
                         select new SelectListItem()
                         {
                             Text = i.Title,
                             Value = i.CityId.ToString()
                         },
                Positions = from i in _db.Positions
                            select new SelectListItem()
                            {
                                Text = i.Title,
                                Value = i.PositionId.ToString()
                            }
            };
            if (model.Employee == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employees employee, string city, string position)
        {
            if (ModelState.IsValid)
            {
                var entry = _db.Employees.Find(employee.EmployeeId);
                entry.FirstName = employee.FirstName;
                entry.LastName = employee.LastName;
                entry.Telephone = employee.Telephone;
                entry.Site = employee.Site;
                entry.Email = employee.Email;
                entry.CityId = int.Parse(city);
                entry.PositionId = int.Parse(position);
                _db.SaveChanges();
                return RedirectToAction("List");
            }
            EmployeeViewModel model = new EmployeeViewModel()
            {
                Employee = employee,
                Cities = from i in _db.Cities
                         select new SelectListItem()
                         {
                             Text = i.Title,
                             Value = i.CityId.ToString()
                         },
                Positions = from i in _db.Positions
                            select new SelectListItem()
                            {
                                Text = i.Title,
                                Value = i.PositionId.ToString()
                            }
            };
            return View(model);
        }

        public PartialViewResult NewEmployee()
        {
            EmployeeViewModel model = new EmployeeViewModel()
            {
                Employee = null,
                Cities = from i in _db.Cities
                         select new SelectListItem()
                         {
                             Text = i.Title,
                             Value = i.CityId.ToString()
                         },
                Positions = from i in _db.Positions
                            select new SelectListItem()
                            {
                                Text = i.Title,
                                Value = i.PositionId.ToString()
                            }
            };
            return PartialView("Create", model);
        }

        public ActionResult Delete(int? employeeId)
        {
            if (employeeId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employees employee = _db.Employees.Find(employeeId);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int employeeId)
        {
            Employees employee = _db.Employees
                .Include("OrderDetails")
                .FirstOrDefault(x=>x.EmployeeId == employeeId);
            if (employee.OrderDetails.Count != 0)
            {
                ModelState.AddModelError("Existing", "Сотрудник используется в таблицах! Измените данные, перед тем как удалить данного сотрудника.");
                return View("Delete", employee);
            }
            _db.Employees.Remove(employee);
            _db.SaveChanges();
            return RedirectToAction("List");
        }
    }
}