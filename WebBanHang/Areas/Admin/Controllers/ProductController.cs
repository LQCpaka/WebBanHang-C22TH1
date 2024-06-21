using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Models;

namespace WebBanHang.Controllers
{
    [Area("Admin")]
    
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHostingEnvironment _hosting;
        public ProductController(ApplicationDbContext db, IHostingEnvironment hosting)
        {
            _db = db;
            _hosting = hosting;
        }
        //Hiển thị danh sách sản phẩm cần quản lý
        public IActionResult Index(int ?page, string textsearch="")
        {
            var pageIndex = (int)(page != null ? page : 1);
            var pageSize = 10;

            var dsSanPham = _db.Products.Include(x => x.Category).Where(p=>p.Name.ToLower().Contains(textsearch.ToLower())).ToList();
            //thống kê số trang
            var pageSum = dsSanPham.Count() / pageSize + (dsSanPham.Count() % pageSize > 0 ? 1 : 0);
            //truyen du lieu cho View
            ViewBag.PageSum = pageSum;
            ViewBag.PageIndex = pageIndex;
            return View(dsSanPham.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList());
           // return View("ShowAll");
        }

        //Hiển thị giao diện thêm sản phẩm
        public IActionResult Add()
        {
            ViewBag.CategoryList = _db.Categories.Select(c=> new SelectListItem { Value = c.Id.ToString(), Text=c.Name});
            return View();
        }
        //xử lý thêm mới sản phẩm
        [HttpPost]
        public IActionResult Add(Product product, IFormFile ImageUrl)
        {
            if (ModelState.IsValid) //kiem tra hop le
            {
                if (ImageUrl != null)
                {
                    //xu ly upload và lưu ảnh đại diện
                    product.ImageUrl = SaveImage(ImageUrl);
                }
                //thêm product vào table Product
                _db.Products.Add(product);
                _db.SaveChanges();
                TempData["success"] = "Product inserted success";
                return RedirectToAction("Index");
            }
            ViewBag.CategoryList = _db.Categories.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });
            return View();
        }

        private string SaveImage(IFormFile image)
        {
            //đặt lại tên file cần lưu
            var filename = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            //lay duong dan luu tru wwwroot tren server
            var path = Path.Combine(_hosting.WebRootPath, @"images/products");
            var saveFile = Path.Combine(path, filename);
            using (var filestream = new FileStream(saveFile, FileMode.Create))
            {
                image.CopyTo(filestream);
            }
            return @"images/products/" + filename;
        }

        //Hiển thị form cập nhật sản phẩm
        public IActionResult Update(int id)
        {
            var product = _db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            //truyền danh sách thể loại cho View để sinh ra điều khiển DropDownList
            ViewBag.CategoryList = _db.Categories.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });
            return View(product);
        }
        //Xử lý cập nhật sản phẩm
        [HttpPost]
        public IActionResult Update(Product product, IFormFile ImageUrl)
        {
            if (ModelState.IsValid) //kiem tra hop le
            {
                var existingProduct = _db.Products.Find(product.Id);
                if (ImageUrl != null)
                {
                    //xu ly upload và lưu ảnh đại diện mới
                    product.ImageUrl = SaveImage(ImageUrl);
                    //xóa ảnh cũ (nếu có)
                    if (!string.IsNullOrEmpty(existingProduct.ImageUrl))
                    {
                        var oldFilePath = Path.Combine(_hosting.WebRootPath,
                        existingProduct.ImageUrl);
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }
                }
                else
                {
                    product.ImageUrl = existingProduct.ImageUrl;
                }
                //cập nhật product vào table Product
                existingProduct.Name = product.Name;
                existingProduct.Description = product.Description;
                existingProduct.Price = product.Price;
                existingProduct.CategoryId = product.CategoryId;
                existingProduct.ImageUrl = product.ImageUrl;
                _db.SaveChanges();
                TempData["success"] = "Product updated success";
                return RedirectToAction("Index");
            }
            ViewBag.CategoryList = _db.Categories.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });
            return View(product);
        }

        //Hiển thị form xác nhận xóa sản phẩm
        public IActionResult Delete(int id)
        {
            var product = _db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        //Xử lý xóa sản phẩm
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            // xoá hình cũ của sản phẩm
            if (!String.IsNullOrEmpty(product.ImageUrl))
            {
                var oldFilePath = Path.Combine(_hosting.WebRootPath, product.ImageUrl);
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
            }
            // xoa san pham khoi CSDL
            _db.Products.Remove(product);
            _db.SaveChanges();
            TempData["success"] = "Product deleted success";
            //chuyen den action index
            return RedirectToAction("Index");
        }

        #region CAll_API
        //---call API---------------------
        // lấy tất cả sản phẩm
        [HttpGet]
        public IActionResult GetAll()
        {
            var dsSanPham = _db.Products.Include(x => x.Category).ToList();
            return Json(dsSanPham);
        }
        #endregion

    }
}
