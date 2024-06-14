using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Models;
using WebBanHang.Helpers;

namespace WebBanHang.Areas.Customer.Controllers
{
	public class CartController : Controller
	{
		private readonly ApplicationDbContext _db;
		public CartController(ApplicationDbContext db)
		{
			_db = db;
		}

		//hiển thị giỏ hàng
		public IActionResult Index()
		{
			Cart cart = HttpContext.Session.GetJson<Cart>("CART");
			if (cart == null)
			{
				cart = new Cart();
			}
			return View(cart);
		}

		//xử lý thêm sản phẩm vào giỏ
		public IActionResult AddToCart(int productId)
		{
			var product = _db.Products.FirstOrDefault(x => x.Id == productId);
			if (product != null)
			{
				Cart cart = HttpContext.Session.GetJson<Cart>("CART");
				if (cart == null)
				{
					cart = new Cart();
				}
				cart.Add(product, 1);
				HttpContext.Session.SetJson("CART", cart);
				// return Json(new { msg="success", qty = cart.Quantity});
				TempData["success"] = "Product added to cart";
				return RedirectToAction("Index", "Home");
			}
			return Json(new { msg = "error" });
		}

		public IActionResult Update(int productId, int qty)
		{
			var product = _db.Products.FirstOrDefault(x => x.Id == productId);
			if (product != null)
			{
				//lấy cart từ Session
				Cart cart = HttpContext.Session.GetJson<Cart>("CART");
				if (cart != null)
				{
					cart.Update(productId, qty); //cập nhật lại số lượng
					HttpContext.Session.SetJson("CART", cart); //lưu cart vào Session
					return RedirectToAction("Index");
				}
			}
			return Json(new { msg = "error" });
		}

		public IActionResult Remove(int productId)
		{
			var product = _db.Products.FirstOrDefault(x => x.Id == productId);
			if (product != null)
			{
				//lấy cart từ Session
				Cart cart = HttpContext.Session.GetJson<Cart>("CART");
				if (cart != null)
				{
					cart.Remove(productId); //xoá sản phẩm khỏi giỏ
					HttpContext.Session.SetJson("CART", cart); //lưu cart vào Session
					return RedirectToAction("Index");
				}
			}

			return NotFound();
		}

		#region API
		public IActionResult AddToCartAPI(int productId)
		{
			var product = _db.Products.FirstOrDefault(x => x.Id == productId);
			if (product != null)
			{
				Cart cart = HttpContext.Session.GetJson<Cart>("CART");
				if (cart == null)
				{
					cart = new Cart();
				}
				cart.Add(product, 1);
				HttpContext.Session.SetJson("CART", cart);
				return Json(new { msg = "Product added to cart", qty = cart.Quantity });
			}
			return Json(new { msg = "error" });
		}
		
		public IActionResult GetQuantityOfCart()
		{
			Cart cart = HttpContext.Session.GetJson<Cart>("CART");
			if (cart != null)
			{
				return Json(new { qty = cart.Quantity });
			}
			return Json(new { qty = 0 });
		}

		#endregion

	}
}
