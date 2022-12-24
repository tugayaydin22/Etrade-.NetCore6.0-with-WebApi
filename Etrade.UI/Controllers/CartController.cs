using Etrade.DAL.Abstract;
using Etrade.Entities.Models.Entities;
using Etrade.Entities.Models.Helpers;
using Etrade.Entities.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Etrade.UI.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductDAL _productDAL;
        private readonly IOrderDAL _IOrderDAL;
        public CartController(IProductDAL productDAL, IOrderDAL ıOrderDAL)
        {
            _productDAL = productDAL;
            _IOrderDAL = ıOrderDAL;
        }

        public IActionResult Index()
        {
            var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            if (cart == null)
                return View();
            ViewBag.Total=cart.Sum(i=>i.Product.Price*i.Quantity).ToString("c");
            SessionHelper.Count=cart.Count();
            return View(cart);
        }
        public IActionResult Buy(int id)
        {
            if (SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart") == null)
            {
                var Cart = new List<CartItem>();
                Cart.Add(new CartItem { Product = _productDAL.Get(id), Quantity = 1 });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", Cart);
            }
            else
            {
                var Cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
                int index = isExits(Cart, id);
                if (index < 0)
                    Cart.Add(new CartItem { Product = _productDAL.Get(id), Quantity = 1 });
                else
                    Cart[index].Quantity++;

                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", Cart);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int id)
        {
            var Cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            int index = isExits(Cart, id);
            Cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", Cart);
            return RedirectToAction("Index");
        }

        private int isExits(List<CartItem> Cart, int id)
        {
            for (int i = 0; i < Cart.Count; i++)
            {
                if (Cart[i].Product.Id.Equals(id))
                {
                    return i;
                    break;
                }
            }
            return -1;
        }

        public IActionResult CheckOut()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public IActionResult CheckOut(ShippingDetails entity)
        {
            var Cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            if (Cart == null)
            {
                ModelState.AddModelError("UrunYokError", "Sepetinizde ürün bulunmamaktadır.");
            }

            if (ModelState.IsValid)
            {
                SaveOrder(Cart, entity);
                Cart.Clear();
                return View("Completed");
            }
            return View(entity);
        }

        private void SaveOrder(List<CartItem> cart, ShippingDetails entity)
        {
            var order = new Order();

            order.OrderNumber = "A" + (new Random()).Next(11111, 99999).ToString();
            order.Total = cart.Sum(i => i.Product.Price * i.Quantity);
            order.OrderDate = DateTime.Now;
            order.OrderState = EnumOrderState.Waiting;
            //order.Username = User.Identity.Name;
            order.Username = entity.UserName;

            order.AddressTitle = entity.AddressTitle;
            order.Address = entity.Address;
            order.City = entity.City;
            order.District = entity.District;
            order.Mahalle = entity.Mahalle;
            order.PostalCode = entity.PostalCode;

            order.Orderlines = new List<OrderLine>();

            foreach (var item in cart)
            {
                var orderline = new OrderLine();
                orderline.Quantity = item.Quantity;
                orderline.Price = item.Quantity * item.Product.Price;
                orderline.ProductId = item.Product.Id;

                order.Orderlines.Add(orderline);
            }
            _IOrderDAL.Add(order);
        }
    }
}
