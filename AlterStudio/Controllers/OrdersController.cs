using AlterStudio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AlterStudio.Controllers
{
    public class OrdersController : Controller
    {
        private readonly LocalStudioEntities _db = new LocalStudioEntities();
        #region Orders
        public ActionResult List()
        {
            return View(_db.Orders.ToList());
        }
        public ActionResult CreateOrder()
        {
            OrderViewModel model = new OrderViewModel
            {
                Order = null,
                Clients = from c in _db.Clients
                          select new SelectListItem
                          {
                              Text = c.FirstName + " " + c.LastName,
                              Value = c.ClientId.ToString()
                          },
                Curators = from c in _db.Curators
                           select new SelectListItem
                           {
                               Text = c.FirstName + " " + c.LastName,
                               Value = c.CuratorId.ToString()
                           }

            };
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateConfirmed(Orders order, string client, string curator)
        {
            order.ClientId = int.Parse(client);
            order.CuratorId = int.Parse(curator);
            if (ModelState.IsValid)
            {
                _db.Orders.Add(order);
                _db.SaveChanges();
                TempData["Success"] = @"Новый заказ №'" + order.OrderId + "' был успешно добавлен!";
                return RedirectToAction("List");
            }
            OrderViewModel model = new OrderViewModel
            {
                Order = order,
                Clients = from c in _db.Clients
                          select new SelectListItem
                          {
                              Text = c.FirstName + " " + c.LastName,
                              Value = c.ClientId.ToString()
                          },
                Curators = from c in _db.Curators
                           select new SelectListItem
                           {
                               Text = c.FirstName + " " + c.LastName,
                               Value = c.CuratorId.ToString()
                           },
            };
            return View("CreateOrder", model);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            OrderViewModel model = new OrderViewModel
            {
                Order = _db.Orders.Find(id),
                Clients = from c in _db.Clients
                          select new SelectListItem
                          {
                              Text = c.FirstName + " " + c.LastName,
                              Value = c.ClientId.ToString()
                          },
                Curators = from c in _db.Curators
                           select new SelectListItem
                           {
                               Text = c.FirstName + " " + c.LastName,
                               Value = c.CuratorId.ToString()
                           }

            };
            if (model.Order == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Orders order, string client, string curator)
        {
            if (ModelState.IsValid)
            {
                Orders entry = _db.Orders.Find(order.OrderId);           
                entry.ClientId = Convert.ToInt32(client);
                entry.CuratorId = Convert.ToInt32(curator);
                entry.ShootingDate = order.ShootingDate;
                entry.PerformTo = order.PerformTo;
                entry.Description = order.Description;

                _db.SaveChanges();
                return RedirectToAction("List");
            }
            OrderViewModel model = new OrderViewModel
            {
                Order = order,
                Clients = from c in _db.Clients
                          select new SelectListItem
                          {
                              Text = c.FirstName + " " + c.LastName,
                              Value = c.ClientId.ToString()
                          },
                Curators = from c in _db.Curators
                           select new SelectListItem
                           {
                               Text = c.FirstName + " " + c.LastName,
                               Value = c.CuratorId.ToString()
                           }

            };
            return View(model);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders order = _db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Orders order = _db.Orders
                .Include("OrderDetails")
                .FirstOrDefault(x => x.OrderId == id);
            _db.Orders.Remove(order);
            _db.SaveChanges();
            return RedirectToAction("List");
        }
        
        #endregion

        public ActionResult Details(int id)
        {
            IEnumerable<OrderDetails> orderDetails = _db.OrderDetails.Where(x => x.OrderId == id);
            if (orderDetails == null)
                return HttpNotFound();

            ViewBag.OrderId = id;
            return View(orderDetails);
        }
        public ActionResult AddServiceToOrder(int orderId)
        {
            OrderDetailsViewModel model = new OrderDetailsViewModel
            {
                OrderDetail = null,
                OrderId = orderId,
                Employees = from e in _db.Employees
                            select new SelectListItem
                            {
                                Value = e.EmployeeId.ToString(),
                                Text = e.LastName + " " + e.FirstName
                            },
                Services = from e in _db.Services
                           select new SelectListItem
                           {
                               Value = e.ServiceId.ToString(),
                               Text = e.Title
                           },
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult AddServiceToOrder(int orderId, OrderDetailsViewModel details, string service, string employee)
        {
            OrderDetails orderDetail = new OrderDetails
            {
                OrderId = orderId,
                ServiceId = int.Parse(service),
                Amount = details.OrderDetail.Amount,
                EmployeeId = int.Parse(employee),
            };

            var  checkForDuplicate = _db.OrderDetails.Find( orderDetail.ServiceId, orderDetail.OrderId);
            if ( checkForDuplicate != null) 
                ModelState.AddModelError("DuplicatePkError","Такая услуга и исполнитель уже существуют в данном заказе. ");

            if (ModelState.IsValid)
            {
                _db.OrderDetails.Add(orderDetail);
                _db.SaveChanges();
                return RedirectToAction("Details", new { id = orderId});
            }
            OrderDetailsViewModel model = new OrderDetailsViewModel
            {
                OrderDetail = orderDetail,
                OrderId = orderId,
                Employees = from e in _db.Employees
                            select new SelectListItem
                            {
                                Value = e.EmployeeId.ToString(),
                                Text = e.FirstName + " " + e.LastName
                            },
                Services = from e in _db.Services
                           select new SelectListItem
                           {
                               Value = e.ServiceId.ToString(),
                               Text = e.Title
                           },
            };
            return View(model);
        }
        public ActionResult EditDetails(int orderId, int serviceId)
        {
            
            OrderDetailsViewModel model = new OrderDetailsViewModel
            {
                OrderDetail = _db.OrderDetails.Find(serviceId, orderId),
                OrderId = orderId,
                Employees = from e in _db.Employees
                            select new SelectListItem
                            {
                                Value = e.EmployeeId.ToString(),
                                Text = e.LastName + " " + e.FirstName
                            },
                Services = from e in _db.Services
                           select new SelectListItem
                           {
                               Value = e.ServiceId.ToString(),
                               Text = e.Title
                           },
            };

            if (model.OrderDetail == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult EditDetails(int orderId, OrderDetailsViewModel details,  string employee)
        {

            OrderDetails orderDetail = new OrderDetails
            {
                OrderId = orderId,
                ServiceId = details.OrderDetail.ServiceId,
                Amount = details.OrderDetail.Amount,
                EmployeeId = int.Parse(employee),
            };

            if (ModelState.IsValid)
            {
                var entry = _db.OrderDetails.Find(orderDetail.ServiceId, orderDetail.OrderId);
                entry.Amount = details.OrderDetail.Amount;
                entry.EmployeeId = int.Parse(employee);
                _db.SaveChanges();
                return RedirectToAction("Details", new { id = orderId });
            }
            OrderDetailsViewModel model = new OrderDetailsViewModel
            {
                OrderDetail = orderDetail,
                OrderId = orderId,
                Employees = from e in _db.Employees
                            select new SelectListItem
                            {
                                Value = e.EmployeeId.ToString(),
                                Text = e.FirstName + " " + e.LastName
                            },
            };
            return View(model);
        }
        public ActionResult DeleteDetails(int orderId, int serviceId)
        {

            OrderDetails order = _db.OrderDetails.Find(serviceId, orderId);
            _db.OrderDetails.Remove(order);
            _db.SaveChanges();
            return RedirectToAction("Details", new { id = orderId });
        }
    }
}