using System;
using System.ComponentModel.DataAnnotations;

namespace WebBanHang.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Chưa Nhập Tên Chủng Loại"), StringLength(50)]
        public String Name { get; set; }
        [Range(1, 100, ErrorMessage = "Trong khoảng 1 - 100")]
        public int DisplayOrder { get; set; }
    }
}
