using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebBanHang.Helpers;
using WebBanHang.Models;

namespace WebBanHang.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;        
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;           
        }
        //hien thi danh sach tat ca san pham       
        public IActionResult Index()
        {
            var productlist = _db.Products.ToList();
            return View(productlist);           
           
        }     
    }
}
