using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebBanHang.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required, StringLength(200)]
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
        //khai báo mối kết hợp 1-n
        [ForeignKey("CategoryId")]
        public virtual Category Category { set; get; } //khai báo mối kết hợp 1 - nhiều
        public string ImageUrl { get; set; }
    }
}
