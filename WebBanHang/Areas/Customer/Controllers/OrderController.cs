using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Helpers;
using WebBanHang.Models;

namespace WebBanHang.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _db;
        public OrderController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            //lấy cart từ Session
            Cart cart = HttpContext.Session.GetJson<Cart>("CART");
            if (cart == null)
            {
                cart = new Cart();
            }
            //gửi cart qua View thông qua ViewBag
            ViewBag.CART = cart;
            return View();
        }

        public IActionResult ProcessOrder(Order order)
        {
            Cart cart = HttpContext.Session.GetJson<Cart>("CART");

            if (ModelState.IsValid)
            {
                //B1. Luu tru don hang------
                //b.1.1. Them Order vào CSDL
                   //khoi tao don hang 
                  order.OrderDate = DateTime.Now;
                  order.Total = cart.Total;
                  order.State = "Pending";
                  //them Order vao CSDL 
                  _db.Orders.Add(order);
                  _db.SaveChanges();
                //b.1.2. Them OrderDetail vào CSDL
                   foreach (var item in cart.Items) //duyet cac item trong Cart
                   {
                        //tao doi tuong OrderDetail
                        var orderDetail = new OrderDetail { OrderId=order.Id,ProductId = item.Product.Id, Quantity=item.Quantity};
                        //them OrderDetail vao CSDL
                        _db.OrderDetails.Add(orderDetail);
                        _db.SaveChanges();
                    }

                //b2. xoa gio hang
                HttpContext.Session.Remove("CART");
                //b3.tra ve view hien thi ket qua
                return View("Result");
            } 
         
            //gửi cart qua View thông qua ViewBag
            ViewBag.CART = cart;
            return View("Index", order);           
        }
    }
}
