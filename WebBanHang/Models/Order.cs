using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace WebBanHang.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập họ tên đầy đủ")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "Vui lòng cung cấp địa chỉ giao hàng")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại liên hệ")]
        public string Phone { get; set; }
        public double Total { set; get; }
        public string State { set; get; }
    }
 }
