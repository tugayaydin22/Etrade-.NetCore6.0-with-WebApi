using Etrade.DAL.Abstract;
using Etrade.Entities.Models.Entities;
using Etrade.Entities.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Etrade.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        private readonly IOrderDAL _IOrderDAL;

        public OrderController(IOrderDAL ıOrderDAL)
        {
            _IOrderDAL = ıOrderDAL;
        }

        public IActionResult Index()
        {
            return View(_IOrderDAL.GetAll());
        }

        public IActionResult Details(int id)
        {
            return View(_IOrderDAL.Get(id));
        }

        public IActionResult Edit(int id)
        {
            var order = _IOrderDAL.Get(id);
            var model = new OrderStateViewModel()
            {
                OrderId = order.Id,
                OrderNumber = order.OrderNumber,
                IsCompleted = false
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(OrderStateViewModel model)
        {
            var order = _IOrderDAL.Get(model.OrderId);
            if (model.IsCompleted)
            {
                order.OrderState = EnumOrderState.Completed;
                _IOrderDAL.Update(order);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var order = _IOrderDAL.Get(id);
            if (order != null)
            {
                _IOrderDAL.Delete(order);
            }
            return RedirectToAction("Index");
        }
    }
}
